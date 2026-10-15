using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CampusEmergency.Building
{
    /// <summary>
    /// 电梯前障碍组件：火灾警报期间阻止玩家使用电梯。
    /// 1. 实体碰撞体（空气墙）阻挡玩家通过手柄往前移动进入电梯；
    /// 2. 触发区检测玩家靠近并弹出警告窗口；
    /// 3. 仅遮挡电梯入口，不遮挡楼梯 —— 需在 Inspector 中把碰撞体尺寸/位置限制在电梯门前。
    /// 玩家靠近触发区时，会在视线前方弹出 "Please don't use the elevator during the fire alarm" 提示。
    /// </summary>
    [DisallowMultipleComponent]
    public class ElevatorBlocker : MonoBehaviour
    {
        [Header("空气墙（阻挡玩家进入电梯）")]
        [Tooltip("实体碰撞体，作为空气墙阻挡玩家移动。\n建议用 BoxCollider，务必关闭 Is Trigger，否则挡不住人。\n尺寸/位置只覆盖电梯门，不要延伸到楼梯口。")]
        [SerializeField] private BoxCollider airWallCollider;

        [Tooltip("空气墙尺寸（仅当未在 Inspector 指定 airWallCollider 时，脚本会自动生成该尺寸的 BoxCollider）。")]
        [SerializeField] private Vector3 airWallSize = new Vector3(2.2f, 2.4f, 0.3f);

        [Tooltip("空气墙相对本物体的中心偏移（米）。")]
        [SerializeField] private Vector3 airWallCenter = new Vector3(0f, 1.2f, 0f);

        [Header("警告触发区（玩家靠近时弹窗）")]
        [Tooltip("触发区碰撞体，玩家进入时弹出警告。务必开启 Is Trigger。\n若留空，脚本会在本物体上自动生成一个比空气墙略大的 BoxCollider 触发区。")]
        [SerializeField] private BoxCollider warningTrigger;

        [Tooltip("触发区尺寸（米），默认比空气墙大一圈，让玩家接近空气墙前就收到警告。")]
        [SerializeField] private Vector3 triggerSize = new Vector3(3.0f, 2.6f, 1.2f);

        [Tooltip("触发区相对本物体的中心偏移（米），默认略向玩家来的方向（+Z）偏移。")]
        [SerializeField] private Vector3 triggerCenter = new Vector3(0f, 1.3f, 0.6f);

        [Header("警告文字")]
        [TextArea(2, 5)]
        [SerializeField] private string warningMessage =
            "Please don't use the elevator during the fire alarm.\nPlease use the stairs to evacuate.";

        [Header("弹窗位置与显示")]
        [Tooltip("弹窗距离玩家相机的前方距离（米）。")]
        [SerializeField] private float popupDistance = 1.6f;

        [Tooltip("弹窗在视线中心的 Y 轴偏移（米），正值偏上。")]
        [SerializeField] private float popupHeightOffset = 0.25f;

        [Tooltip("弹窗自动隐藏延时（秒），设为 0 表示不自动隐藏。")]
        [SerializeField] private float autoHideDelay = 4.5f;

        [Tooltip("弹窗是否跟随玩家朝向（始终面向玩家）。")]
        [SerializeField] private bool followPlayerView = true;

        [Header("玩家识别")]
        [Tooltip("能触发警告的物体标签。")]
        [SerializeField] private string[] playerTags = { "Player", "MainCamera" };

        [Header("颜色主题")]
        [SerializeField] private Color panelColor = new Color(0.18f, 0.04f, 0.04f, 0.96f);
        [SerializeField] private Color accentColor = new Color(1f, 0.32f, 0.22f, 1f);
        [SerializeField] private Color textColor = new Color(1f, 0.95f, 0.92f, 1f);

        [Header("可视化（Gizmos 与运行时视觉墙）")]
        [Tooltip("运行时是否显示半透明空气墙视觉（不影响碰撞，仅视觉提示）。")]
        [SerializeField] private bool showRuntimeVisualWall = true;

        [Tooltip("运行时空气墙半透明度（0=完全透明, 1=不透明）。")]
        [Range(0f, 1f)]
        [SerializeField] private float runtimeWallOpacity = 0.22f;

        [Tooltip("Scene 视图中空气墙线框颜色。")]
        [SerializeField] private Color gizmoAirWallColor = new Color(1f, 0.2f, 0.2f, 0.9f);

        [Tooltip("Scene 视图中触发区线框颜色。")]
        [SerializeField] private Color gizmoTriggerColor = new Color(1f, 0.85f, 0.2f, 0.7f);

        private GameObject runtimeVisualWall;
        private GameObject popupCanvasObject;
        private TextMeshProUGUI messageText;
        private CanvasGroup popupCanvasGroup;
        private Coroutine hideCoroutine;
        private bool isShowing;
        private Transform playerCamera;

        private void Awake()
        {
            EnsureAirWallCollider();
            EnsureWarningTrigger();
            EnsureRuntimeVisualWall();
            BuildPopup();
        }

        private void LateUpdate()
        {
            if (!isShowing || !followPlayerView || popupCanvasObject == null) return;

            if (playerCamera == null)
            {
                FindPlayerCamera();
                if (playerCamera == null) return;
            }

            Vector3 forward = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up);
            if (forward.sqrMagnitude < 0.001f)
            {
                forward = playerCamera.forward;
            }
            forward.Normalize();

            Vector3 position = playerCamera.position + forward * popupDistance + Vector3.up * popupHeightOffset;
            Quaternion rotation = Quaternion.LookRotation(-forward, Vector3.up);
            popupCanvasObject.transform.SetPositionAndRotation(position, rotation);
        }

        private void OnDestroy()
        {
            if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        }

        /// <summary>玩家进入触发区时弹出警告。</summary>
        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayer(other)) return;
            ShowWarning();
        }

        /// <summary>玩家持续在触发区内，若弹窗被关闭则再次弹出。</summary>
        private void OnTriggerStay(Collider other)
        {
            if (!IsPlayer(other)) return;
            if (!isShowing) ShowWarning();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsPlayer(other)) return;
            // 离开触发区时不强制隐藏，让玩家读完提示；如需离开即隐藏可取消注释下一行。
            // HideWarning();
        }

        private bool IsPlayer(Collider other)
        {
            if (other == null) return false;

            foreach (string tag in playerTags)
            {
                if (other.CompareTag(tag)) return true;
            }

            return other.GetComponentInParent<CharacterController>() != null
                   || other.GetComponent<Camera>() != null;
        }

        /// <summary>确保空气墙碰撞体存在且为非 Trigger。</summary>
        private void EnsureAirWallCollider()
        {
            if (airWallCollider == null)
            {
                GameObject wall = new GameObject("AirWall_ElevatorBlocker");
                wall.transform.SetParent(transform, false);
                airWallCollider = wall.AddComponent<BoxCollider>();
                airWallCollider.size = airWallSize;
                airWallCollider.center = airWallCenter;
            }

            airWallCollider.isTrigger = false;

            if (airWallCollider.GetComponent<Rigidbody>() != null)
            {
                Debug.LogWarning("[ElevatorBlocker] 空气墙上不应该有 Rigidbody，静态碰撞体即可阻挡 CharacterController。", this);
            }
        }

        /// <summary>确保触发区存在且为 Trigger，挂在本脚本所在物体上以接收 OnTriggerEnter。</summary>
        private void EnsureWarningTrigger()
        {
            if (warningTrigger == null)
            {
                warningTrigger = GetComponent<BoxCollider>();
                if (warningTrigger == null)
                {
                    warningTrigger = gameObject.AddComponent<BoxCollider>();
                }
                warningTrigger.size = triggerSize;
                warningTrigger.center = triggerCenter;
            }

            warningTrigger.isTrigger = true;
        }

        /// <summary>构建运行时半透明空气墙视觉提示（与空气墙碰撞体同位置同尺寸）。
        /// 运行时（Play/构建）不创建视觉墙，玩家只感受到碰撞阻挡；
        /// 编辑器开发时通过 OnDrawGizmos 线框可视化障碍范围。</summary>
        private void EnsureRuntimeVisualWall()
        {
            if (Application.isPlaying) return;
            if (!showRuntimeVisualWall) return;
            if (airWallCollider == null) return;

            runtimeVisualWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            runtimeVisualWall.name = "AirWall_Visual";
            runtimeVisualWall.transform.SetParent(airWallCollider.transform, false);
            runtimeVisualWall.transform.localPosition = airWallCollider.center;
            runtimeVisualWall.transform.localScale = airWallCollider.size;

            // 移除默认的 BoxCollider，避免与空气墙碰撞体重复
            Collider visCollider = runtimeVisualWall.GetComponent<Collider>();
            if (visCollider != null) Destroy(visCollider);

            MeshRenderer renderer = runtimeVisualWall.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                if (mat == null)
                {
                    mat = new Material(Shader.Find("Standard"));
                }
                if (mat == null)
                {
                    mat = new Material(Shader.Find("Sprites/Default"));
                }

                Color wallColor = accentColor;
                wallColor.a = runtimeWallOpacity;
                mat.color = wallColor;

                if (mat.HasProperty("_SurfaceType")) mat.SetFloat("_SurfaceType", 1f);
                if (mat.HasProperty("_BlendMode")) mat.SetFloat("_BlendMode", 0f);
                if (mat.HasProperty("_SrcBlend")) mat.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
                if (mat.HasProperty("_DstBlend")) mat.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                if (mat.HasProperty("_ZWrite")) mat.SetFloat("_ZWrite", 0f);
                if (mat.HasProperty("_AlphaClip")) mat.SetFloat("_AlphaClip", 0f);
                if (mat.HasProperty("_Surface"))
                {
                    mat.SetFloat("_Surface", 1f);
                    mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    mat.EnableKeyword("_BLENDMODE_ALPHA");
                }
                mat.SetFloat("_Mode", 3f);
                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                renderer.material = mat;
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
            }
        }

        /// <summary>Scene 视图中始终显示触发区与空气墙线框（未选中也可见）。</summary>
        private void OnDrawGizmos()
        {
            DrawAirWallGizmo(false);
            DrawTriggerGizmo(false);
        }

        /// <summary>Scene 视图中选中该物体时高亮显示线框。</summary>
        private void OnDrawGizmosSelected()
        {
            DrawAirWallGizmo(true);
            DrawTriggerGizmo(true);
        }

        private void DrawAirWallGizmo(bool selected)
        {
            Vector3 center = airWallCollider != null ? airWallCollider.transform.TransformPoint(airWallCollider.center) : transform.TransformPoint(airWallCenter);
            Vector3 size = airWallCollider != null ? Vector3.Scale(airWallCollider.size, airWallCollider.transform.lossyScale) : airWallSize;

            Gizmos.color = gizmoAirWallColor;
            if (selected)
            {
                Gizmos.DrawCube(center, size);
            }
            Gizmos.DrawWireCube(center, size);
        }

        private void DrawTriggerGizmo(bool selected)
        {
            Vector3 center = warningTrigger != null ? warningTrigger.transform.TransformPoint(warningTrigger.center) : transform.TransformPoint(triggerCenter);
            Vector3 size = warningTrigger != null ? Vector3.Scale(warningTrigger.size, warningTrigger.transform.lossyScale) : triggerSize;

            Gizmos.color = gizmoTriggerColor;
            if (selected)
            {
                Gizmos.DrawCube(center, size);
            }
            Gizmos.DrawWireCube(center, size);
        }

        /// <summary>构建警告弹窗（World Space Canvas）。</summary>
        private void BuildPopup()
        {
            Font font = Font.CreateDynamicFontFromOSFont(
                new[] { "Arial", "Segoe UI", "Noto Sans" }, 64);
            if (font == null)
            {
                font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            }

            popupCanvasObject = new GameObject("ElevatorWarningPopup",
                typeof(RectTransform), typeof(Canvas), typeof(CanvasGroup));
            popupCanvasObject.transform.SetParent(transform, false);
            popupCanvasObject.SetActive(false);

            RectTransform rect = popupCanvasObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(1100f, 460f);

            Canvas canvas = popupCanvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 50;

            popupCanvasGroup = popupCanvasObject.GetComponent<CanvasGroup>();
            popupCanvasGroup.alpha = 0f;
            popupCanvasGroup.blocksRaycasts = true;
            popupCanvasGroup.interactable = true;

            popupCanvasObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            // 背景面板
            GameObject bg = new GameObject("PanelBackground",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            bg.transform.SetParent(popupCanvasObject.transform, false);
            RectTransform bgRect = bg.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            Image bgImage = bg.GetComponent<Image>();
            bgImage.color = panelColor;
            bgImage.raycastTarget = false;

            // 顶部强调条
            GameObject accent = new GameObject("AccentBar",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            accent.transform.SetParent(bg.transform, false);
            RectTransform accentRect = accent.GetComponent<RectTransform>();
            accentRect.anchorMin = new Vector2(0.02f, 0.82f);
            accentRect.anchorMax = new Vector2(0.98f, 0.86f);
            accentRect.offsetMin = Vector2.zero;
            accentRect.offsetMax = Vector2.zero;
            accent.GetComponent<Image>().color = accentColor;

            // 标题
            GameObject titleObj = new GameObject("TitleText",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            titleObj.transform.SetParent(bg.transform, false);
            RectTransform titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.05f, 0.62f);
            titleRect.anchorMax = new Vector2(0.95f, 0.80f);
            titleRect.offsetMin = Vector2.zero;
            titleRect.offsetMax = Vector2.zero;
            TextMeshProUGUI title = titleObj.GetComponent<TextMeshProUGUI>();
            title.text = "FIRE ALARM WARNING";
            title.fontSize = 42;
            title.color = accentColor;
            title.alignment = TextAlignmentOptions.Center;
            title.fontStyle = FontStyles.Bold;
            title.raycastTarget = false;

            // 正文
            GameObject msgObj = new GameObject("MessageText",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            msgObj.transform.SetParent(bg.transform, false);
            RectTransform msgRect = msgObj.GetComponent<RectTransform>();
            msgRect.anchorMin = new Vector2(0.06f, 0.12f);
            msgRect.anchorMax = new Vector2(0.94f, 0.58f);
            msgRect.offsetMin = Vector2.zero;
            msgRect.offsetMax = Vector2.zero;
            messageText = msgObj.GetComponent<TextMeshProUGUI>();
            messageText.text = warningMessage;
            messageText.fontSize = 32;
            messageText.color = textColor;
            messageText.alignment = TextAlignmentOptions.Center;
            messageText.enableWordWrapping = true;
            messageText.raycastTarget = false;

            // 关闭按钮
            GameObject closeBtn = new GameObject("CloseButton",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            closeBtn.transform.SetParent(bg.transform, false);
            RectTransform btnRect = closeBtn.GetComponent<RectTransform>();
            btnRect.anchorMin = new Vector2(0.36f, 0.02f);
            btnRect.anchorMax = new Vector2(0.64f, 0.11f);
            btnRect.offsetMin = Vector2.zero;
            btnRect.offsetMax = Vector2.zero;
            Image btnImage = closeBtn.GetComponent<Image>();
            btnImage.color = accentColor;
            Button btn = closeBtn.GetComponent<Button>();
            btn.targetGraphic = btnImage;
            btn.onClick.AddListener(HideWarning);

            GameObject btnLabel = new GameObject("Label",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            btnLabel.transform.SetParent(closeBtn.transform, false);
            RectTransform labelRect = btnLabel.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;
            TextMeshProUGUI label = btnLabel.GetComponent<TextMeshProUGUI>();
            label.text = "Got it";
            label.fontSize = 26;
            label.color = panelColor;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        /// <summary>显示警告弹窗。</summary>
        public void ShowWarning()
        {
            if (popupCanvasObject == null) return;

            FindPlayerCamera();
            if (playerCamera != null)
            {
                Vector3 forward = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up);
                if (forward.sqrMagnitude < 0.001f) forward = playerCamera.forward;
                forward.Normalize();
                Vector3 pos = playerCamera.position + forward * popupDistance + Vector3.up * popupHeightOffset;
                Quaternion rot = Quaternion.LookRotation(-forward, Vector3.up);
                popupCanvasObject.transform.SetPositionAndRotation(pos, rot);
            }

            popupCanvasObject.SetActive(true);
            isShowing = true;

            if (hideCoroutine != null) StopCoroutine(hideCoroutine);
            StartCoroutine(FadeCanvas(1f, 0.25f));

            if (autoHideDelay > 0f)
            {
                hideCoroutine = StartCoroutine(HideAfterDelay(autoHideDelay));
            }

            Debug.Log("[ElevatorBlocker] 玩家试图使用电梯，弹出火灾警报警告。");
        }

        /// <summary>隐藏警告弹窗。</summary>
        public void HideWarning()
        {
            if (!isShowing || popupCanvasObject == null) return;

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }

            StartCoroutine(FadeCanvas(0f, 0.25f, () =>
            {
                popupCanvasObject.SetActive(false);
                isShowing = false;
            }));
        }

        private IEnumerator HideAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            HideWarning();
        }

        private IEnumerator FadeCanvas(float targetAlpha, float duration, System.Action onComplete = null)
        {
            if (popupCanvasGroup == null) { onComplete?.Invoke(); yield break; }

            float startAlpha = popupCanvasGroup.alpha;
            if (duration <= 0f)
            {
                popupCanvasGroup.alpha = targetAlpha;
                popupCanvasGroup.interactable = targetAlpha > 0.01f;
                popupCanvasGroup.blocksRaycasts = targetAlpha > 0.01f;
                onComplete?.Invoke();
                yield break;
            }

            popupCanvasGroup.interactable = targetAlpha > startAlpha;
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float k = Mathf.Clamp01(t / duration);
                popupCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, k);
                yield return null;
            }
            popupCanvasGroup.alpha = targetAlpha;
            popupCanvasGroup.interactable = targetAlpha > 0.01f;
            popupCanvasGroup.blocksRaycasts = targetAlpha > 0.01f;
            onComplete?.Invoke();
        }

        private void FindPlayerCamera()
        {
            if (playerCamera != null) return;
            if (Camera.main != null)
            {
                playerCamera = Camera.main.transform;
                return;
            }
            Camera[] cams = FindObjectsOfType<Camera>();
            foreach (Camera c in cams)
            {
                if (c.enabled && c.gameObject.activeInHierarchy)
                {
                    playerCamera = c.transform;
                    break;
                }
            }
        }

        private void OnValidate()
        {
            if (airWallCollider != null)
            {
                airWallCollider.isTrigger = false;
            }
            if (warningTrigger != null)
            {
                warningTrigger.isTrigger = true;
            }
        }
    }
}
