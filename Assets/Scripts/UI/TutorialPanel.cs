using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CampusEmergency.UI
{
    /// <summary>
    /// 简易教程引导面板（零配置版）：
    /// 1. 启动时自动把 Canvas 设为 World Space 并出现在 HMD 相机正前方 1.2m
    /// 2. 自动查找子物体中的 TMP 文字和按钮，按名称或排序分配：
    ///    - Heading：名字含 "Heading" / "Title" / "标题" 的 TMP，否则就是第 1 个 TMP
    ///    - Body：名字含 "Body" / "Content" / "正文" 的 TMP，否则是第 2 个 TMP
    ///    - 左按钮（关闭）：名字含 "Close" / "Hide" / "Skip" / "关闭" / "跳过" 的 Button，否则第 1 个 Button
    ///    - 右按钮（翻页）：名字含 "Next" / "Arrow" / "下一页" / "箭头" 的 Button，否则第 2 个 Button
    /// 3. 同时自动补齐 VR UI 所需组件：TrackedDeviceGraphicRaycaster、Canvas 缩放等
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class TutorialPanel : MonoBehaviour
    {
        [Header("定位参数")]
        [Tooltip("距离 HMD 相机的前方距离（米）。")]
        [SerializeField] private float distanceFromCamera = 1.2f;

        [Tooltip("面板在相机前方的 Y 轴偏移（相对于视线中心，正值偏上，负值偏下）。Quest 3 推荐 0.3m 左右，处于舒适视线上方区域。")]
        [SerializeField] private float heightOffset = 0.3f;

        [Header("UI 引用（留空则启动时自动按名称/顺序查找）")]
        [SerializeField] private TextMeshProUGUI headingText;
        [SerializeField] private TextMeshProUGUI bodyText;
        [SerializeField] private Button leftCloseButton;
        [SerializeField] private Button rightNextButton;

        [Header("淡入淡出参数")]
        [Tooltip("面板整体显示/隐藏的过渡时长（秒）。")]
        [SerializeField] private float panelFadeDuration = 0.35f;

        [Tooltip("翻页时正文文字的淡入淡出时长（秒）。")]
        [SerializeField] private float pageFadeDuration = 0.25f;

        [Header("文字内容（留空则使用场景 UI 中已写好的文字）")]
        [Tooltip("标题：留空 = 使用 Heading_TMP 组件上已经填好的文字，不覆盖。")]
        [TextArea]
        [SerializeField] private string overrideHeading;

        [Tooltip("翻页正文：\n• 这里填内容 → 使用这里的文字，覆盖场景 UI；\n• 这里留空数组 → 自动查找子物体中名字含 Page1/Page2/Page3 的 Body_TMP 做每一页内容（推荐，直接在场景里写）；\n• 两者都没有 → 就用 Body_TMP 上已有的文字当作单页显示（不会翻页）。")]
        [TextArea]
        [SerializeField] private string[] overridePageContents;

        private string[] _runtimePages;
        private int _currentPage;
        private CanvasGroup _canvasGroup;       // 整体透明度控制 + 交互开关
        private Coroutine _panelFadeRoutine;    // 防止重复启动
        private Coroutine _bodyFadeRoutine;
        private bool _isHiding;

        private void Awake()
        {
            EnsureVrCanvasSetup();
            AutoResolveReferences();
            BindButtonEvents();
        }

        private void Start()
        {
            if (headingText != null && !string.IsNullOrWhiteSpace(overrideHeading))
            {
                headingText.text = overrideHeading;
            }

            BuildRuntimePages();
            _currentPage = 0;
            UpdateBodyText(immediate: true);
            PlaceInFrontOfCamera();

            ShowPanel();
        }

        private void OnDestroy()
        {
            UnbindButtonEvents();
        }

        // =========================================================
        // 自动补齐 VR Canvas 配置
        // =========================================================
        private void EnsureVrCanvasSetup()
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas == null) canvas = gameObject.AddComponent<Canvas>();

            if (canvas.renderMode != RenderMode.WorldSpace)
            {
                canvas.renderMode = RenderMode.WorldSpace;
            }

            // 给 Canvas 一个合理初始尺寸（Quest 3 常见 600x400，单位会随 scale 换算）
            RectTransform rt = canvas.GetComponent<RectTransform>();
            if (rt != null && rt.sizeDelta == Vector2.zero)
            {
                rt.sizeDelta = new Vector2(800f, 500f);
            }

            // 世界空间 Canvas 推荐 scale 0.001，这样 800x500 = 0.8m x 0.5m
            if (transform.localScale == Vector3.one)
            {
                transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            }

            // VR UI 必须：射线检测
            if (GetComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>() == null
                && GetComponent<GraphicRaycaster>() == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }

            // 如果场景没 EventSystem，补一个（UI 能接收点击的前提）
            if (FindFirstObjectByType<EventSystem>() == null)
            {
                GameObject es = new GameObject("EventSystem");
                es.AddComponent<EventSystem>();
            }

            // 注意：Rigidbody / 重力 / Collider 的配置全部由你在 Unity Inspector 里手动设置，
            // 本脚本不再触碰物理组件，避免与你自定义的设置冲突。

            // CanvasGroup：统一控制整体透明度 + 交互开关（淡入淡出核心）
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null) _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        // =========================================================
        // 自动查找子物体引用（按名称优先，其次按顺序）
        // =========================================================
        private void AutoResolveReferences()
        {
            TextMeshProUGUI[] allTMP = GetComponentsInChildren<TextMeshProUGUI>(true);
            Button[] allButtons = GetComponentsInChildren<Button>(true);

            // ---- Heading ----
            if (headingText == null)
            {
                headingText = FindByName(allTMP, "Heading", "Title", "标题");
                if (headingText == null && allTMP.Length >= 1) headingText = allTMP[0];
            }

            // ---- Body ----
            if (bodyText == null)
            {
                bodyText = FindByName(allTMP, "Body", "Content", "正文");
                if (bodyText == null && allTMP.Length >= 2) bodyText = allTMP[1];
            }

            // ---- 左按钮（关闭） ----
            if (leftCloseButton == null)
            {
                leftCloseButton = FindByName(allButtons, "Close", "Hide", "Skip", "关闭", "跳过", "取消");
                if (leftCloseButton == null && allButtons.Length >= 1) leftCloseButton = allButtons[0];
            }

            // ---- 右按钮（翻页） ----
            if (rightNextButton == null)
            {
                rightNextButton = FindByName(allButtons, "Next", "Arrow", "Continue", "下一页", "箭头", "继续");
                if (rightNextButton == null && allButtons.Length >= 2) rightNextButton = allButtons[1];
            }

            // 给按钮设置默认文字（按钮标签完全空时才补，你场景里已经写好的不会被覆盖）
            SetButtonLabelIfEmpty(leftCloseButton, "关闭");
            SetButtonLabelIfEmpty(rightNextButton, "→");
        }

        /// <summary>
        /// 收集运行时的翻页内容：优先级 ① Inspector 的 overridePageContents →
        /// ② 子物体里名字含 Page1/Page2 的 TMP 收集 → ③ 主 Body_TMP 初始文字（单页）。
        /// 推荐用方案 ②：在场景里建多个 Body_Page1、Body_Page2、Body_Page3 TMP，分别写好内容。
        /// </summary>
        private void BuildRuntimePages()
        {
            // ① Inspector 里填了就直接用
            if (overridePageContents != null && overridePageContents.Length > 0)
            {
                _runtimePages = overridePageContents;
                if (bodyText != null && _runtimePages.Length > 0)
                    bodyText.text = _runtimePages[0];
                return;
            }

            // ② 找场景里命名为 Page1 / Page2 / Page3 ... 的 TMP（放在 Canvas 任意层级下，名字包含关键词即可）
            TextMeshProUGUI[] allTMP = GetComponentsInChildren<TextMeshProUGUI>(true);
            System.Collections.Generic.List<string> collected = new System.Collections.Generic.List<string>();
            for (int i = 1; i <= 20; i++)
            {
                TextMeshProUGUI pageTMP = FindByName(allTMP, "Page" + i, "第" + i + "页");
                if (pageTMP == null) break;
                if (!string.IsNullOrEmpty(pageTMP.text))
                {
                    collected.Add(pageTMP.text);
                    pageTMP.gameObject.SetActive(false);
                }
            }
            if (collected.Count > 0)
            {
                _runtimePages = collected.ToArray();
                if (bodyText != null)
                    bodyText.text = _runtimePages[0];
                return;
            }

            // ③ 退而求其次：用 Body_TMP 场景里已经写好的那一页文字
            string fallback = bodyText != null ? bodyText.text : string.Empty;
            _runtimePages = string.IsNullOrEmpty(fallback) ? new string[0] : new[] { fallback };
        }

        private static T FindByName<T>(T[] array, params string[] keywords) where T : Component
        {
            if (array == null) return null;
            foreach (T item in array)
            {
                if (item == null) continue;
                string n = item.gameObject.name;
                foreach (string kw in keywords)
                {
                    if (n.IndexOf(kw, System.StringComparison.OrdinalIgnoreCase) >= 0)
                        return item;
                }
            }
            return null;
        }

        private static void SetButtonLabelIfEmpty(Button btn, string defaultLabel)
        {
            if (btn == null) return;
            TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>(true);
            if (label != null && string.IsNullOrWhiteSpace(label.text))
            {
                label.text = defaultLabel;
            }
        }

        // =========================================================
        // 按钮事件绑定 / 解绑
        // =========================================================
        private void BindButtonEvents()
        {
            if (leftCloseButton != null)
            {
                leftCloseButton.onClick.RemoveListener(ClosePanel);
                leftCloseButton.onClick.AddListener(ClosePanel);
            }
            else
            {
                Debug.LogWarning("[TutorialPanel] 没找到左按钮（关闭），请检查子物体中是否有 Button。");
            }

            if (rightNextButton != null)
            {
                rightNextButton.onClick.RemoveListener(NextPage);
                rightNextButton.onClick.AddListener(NextPage);
            }
            else
            {
                Debug.LogWarning("[TutorialPanel] 没找到右按钮（下一页），请检查子物体中是否有 Button。");
            }
        }

        private void UnbindButtonEvents()
        {
            if (leftCloseButton != null) leftCloseButton.onClick.RemoveListener(ClosePanel);
            if (rightNextButton != null) rightNextButton.onClick.RemoveListener(NextPage);
        }

        // =========================================================
        // 定位
        // =========================================================
        private void PlaceInFrontOfCamera()
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogWarning("[TutorialPanel] 未找到 MainCamera，跳过初始定位。");
                return;
            }

            Transform camT = cam.transform;
            Vector3 targetPos = camT.position
                              + camT.forward * distanceFromCamera
                              + Vector3.up * heightOffset;
            transform.position = targetPos;

            Vector3 lookDir = camT.position - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(-lookDir, Vector3.up);
            }
        }

        // =========================================================
        // 公共 API
        // =========================================================
        public void NextPage()
        {
            if (_runtimePages == null || _runtimePages.Length <= 1) return;
            _currentPage = (_currentPage + 1) % _runtimePages.Length;
            Debug.Log($"[TutorialPanel] 翻页 → {_currentPage + 1}/{_runtimePages.Length}");
            UpdateBodyText();
        }

        public void ClosePanel()
        {
            if (_isHiding) return;
            _isHiding = true;
            Debug.Log("[TutorialPanel] 面板渐出隐藏");
            if (_panelFadeRoutine != null) StopCoroutine(_panelFadeRoutine);
            _panelFadeRoutine = StartCoroutine(FadeCanvas(0f, panelFadeDuration, onComplete: () =>
            {
                gameObject.SetActive(false);
                _isHiding = false;
            }));
        }

        public void ShowPanel()
        {
            gameObject.SetActive(true);
            _isHiding = false;
            _currentPage = 0;
            if (_runtimePages == null) BuildRuntimePages();
            UpdateBodyText(immediate: true);

            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup != null) _canvasGroup.alpha = 0f;

            if (_panelFadeRoutine != null) StopCoroutine(_panelFadeRoutine);
            _panelFadeRoutine = StartCoroutine(FadeCanvas(1f, panelFadeDuration));
        }

        private void UpdateBodyText(bool immediate = false)
        {
            if (bodyText == null || _runtimePages == null || _runtimePages.Length <= _currentPage) return;
            string nextText = _runtimePages[_currentPage];

            if (immediate || pageFadeDuration <= 0f)
            {
                bodyText.text = nextText;
                var bc = bodyText.color; bc.a = 1f; bodyText.color = bc;
                return;
            }

            if (_bodyFadeRoutine != null) StopCoroutine(_bodyFadeRoutine);
            _bodyFadeRoutine = StartCoroutine(FadeBody(nextText));
        }

        // ========== 淡入淡出协程（通用插值） ==========
        private System.Collections.IEnumerator FadeCanvas(float targetAlpha, float duration, System.Action onComplete = null)
        {
            if (_canvasGroup == null) { onComplete?.Invoke(); yield break; }

            float startAlpha = _canvasGroup.alpha;
            if (duration <= 0f)
            {
                _canvasGroup.alpha = targetAlpha;
                _canvasGroup.interactable = targetAlpha > 0.01f;
                _canvasGroup.blocksRaycasts = targetAlpha > 0.01f;
                onComplete?.Invoke();
                yield break;
            }

            // 淡出中禁止点击，防止连点
            _canvasGroup.interactable = targetAlpha > startAlpha;

            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float k = Mathf.Clamp01(t / duration);
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, k);
                yield return null;
            }
            _canvasGroup.alpha = targetAlpha;
            _canvasGroup.interactable = targetAlpha > 0.01f;
            _canvasGroup.blocksRaycasts = targetAlpha > 0.01f;
            onComplete?.Invoke();
        }

        private System.Collections.IEnumerator FadeBody(string nextText)
        {
            float half = pageFadeDuration * 0.5f;

            // 1) 当前文字淡出到 0
            float t = 0f;
            Color c = bodyText.color;
            float startA = c.a;
            while (t < half)
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(startA, 0f, Mathf.Clamp01(t / half));
                bodyText.color = c;
                yield return null;
            }
            c.a = 0f; bodyText.color = c;

            // 2) 瞬间切换文字
            bodyText.text = nextText;

            // 3) 新文字淡入到 1
            t = 0f;
            while (t < half)
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(0f, 1f, Mathf.Clamp01(t / half));
                bodyText.color = c;
                yield return null;
            }
            c.a = 1f; bodyText.color = c;
        }
    }
}
