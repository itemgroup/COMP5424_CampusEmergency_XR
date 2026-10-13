using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CampusEmergency.Building
{
    public class NpcGuideDialogue : MonoBehaviour
    {
        [SerializeField] private GameObject speechBubbleRoot;
        [SerializeField] private TMP_Text tmpDialogueText;
        [SerializeField] private Text uiDialogueText;

        [Header("Bubble layout")]
        [SerializeField] private float bubbleHeight = 2.45f;
        [SerializeField] private float bubbleForwardOffset = 0.12f;
        [SerializeField] private float bubbleScale = 0.0013f;
        [SerializeField] private float panelWidth = 1050f;
        [SerializeField] private float panelHeight = 440f;
        [SerializeField] private int titleFontSize = 46;
        [SerializeField] private int messageFontSize = 44;

        [Header("Dialogue")]
        [TextArea(2, 5)]
        [SerializeField] private string dialogueMessage =
            "In case of fire, do NOT use the elevators! Please evacuate using the stairs and stay safe.";
        [SerializeField] private float displayDuration = 7f;
        [SerializeField] private bool triggerOnce = true;

        [Header("Optional animation and audio")]
        [SerializeField] private Animator npcAnimator;
        [SerializeField] private string animationTriggerName = "";
        [SerializeField] private AudioSource voiceAudioSource;

        [Header("Trigger area")]
        [SerializeField] private string[] playerTags = { "Player", "MainCamera" };

        private bool hasTriggered;
        private Coroutine hideCoroutine;

        private void OnValidate()
        {
            ApplyBubbleTransformAndStyles();
        }

        private void Awake()
        {
            EnsureSpeechBubbleExists();
            if (speechBubbleRoot != null)
                speechBubbleRoot.SetActive(false);
        }

        private void Start()
        {
            EnsureSpeechBubbleExists();
        }

        [ContextMenu("Build Or Fix Speech Bubble")]
        public void EnsureSpeechBubbleExists()
        {
            if (speechBubbleRoot != null)
            {
                Text existingText = speechBubbleRoot.GetComponentInChildren<Text>(true);
                TMP_Text existingTmpText = speechBubbleRoot.GetComponentInChildren<TMP_Text>(true);
                if (existingText != null || existingTmpText != null)
                {
                    ApplyBubbleTransformAndStyles();
                    return;
                }

                if (Application.isPlaying)
                    Destroy(speechBubbleRoot);
                else
                    DestroyImmediate(speechBubbleRoot);
                speechBubbleRoot = null;
            }

            GameObject bubble = new GameObject("DialogueBubble_WorldSpace", typeof(RectTransform), typeof(Canvas));
            bubble.transform.SetParent(transform, false);

            RectTransform canvasRect = bubble.GetComponent<RectTransform>();
            canvasRect.localPosition = new Vector3(0f, bubbleHeight, bubbleForwardOffset);
            canvasRect.localScale = Vector3.one * bubbleScale;
            canvasRect.sizeDelta = new Vector2(panelWidth, panelHeight);
            canvasRect.pivot = new Vector2(0.5f, 0.5f);

            Canvas canvas = bubble.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 25;
            bubble.AddComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>();
            bubble.AddComponent<LookAtPlayerYOnly>();

            GameObject background = new GameObject("PanelBackground",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            background.transform.SetParent(bubble.transform, false);
            RectTransform backgroundRect = background.GetComponent<RectTransform>();
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.offsetMin = Vector2.zero;
            backgroundRect.offsetMax = Vector2.zero;
            Image backgroundImage = background.GetComponent<Image>();
            backgroundImage.color = new Color(0.04f, 0.08f, 0.14f, 0.95f);
            backgroundImage.raycastTarget = false;

            GameObject accent = new GameObject("BorderAccent",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            accent.transform.SetParent(background.transform, false);
            RectTransform accentRect = accent.GetComponent<RectTransform>();
            accentRect.anchorMin = new Vector2(0.02f, 0.74f);
            accentRect.anchorMax = new Vector2(0.98f, 0.755f);
            accentRect.offsetMin = Vector2.zero;
            accentRect.offsetMax = Vector2.zero;
            Image accentImage = accent.GetComponent<Image>();
            accentImage.color = new Color(0.23f, 0.78f, 0.9f, 1f);
            accentImage.raycastTarget = false;

            Font font = Font.CreateDynamicFontFromOSFont(
                new[] { "Arial", "Segoe UI", "Noto Sans" }, 64);
            if (font == null)
                font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (font == null)
                font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            GameObject titleObject = CreateTextObject("TitleText", background.transform,
                new Vector2(0.04f, 0.76f), new Vector2(0.96f, 0.96f));
            Text title = titleObject.GetComponent<Text>();
            title.font = font;
            title.fontSize = titleFontSize;
            title.fontStyle = FontStyle.Bold;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(1f, 0.85f, 0.25f, 1f);
            title.text = "CAMPUS SAFETY ADVISORY";
            title.raycastTarget = false;

            GameObject messageObject = CreateTextObject("MessageText", background.transform,
                new Vector2(0.04f, 0.04f), new Vector2(0.96f, 0.72f));
            Text message = messageObject.GetComponent<Text>();
            message.font = font;
            message.fontSize = messageFontSize;
            message.alignment = TextAnchor.MiddleCenter;
            message.color = Color.white;
            message.horizontalOverflow = HorizontalWrapMode.Wrap;
            message.verticalOverflow = VerticalWrapMode.Truncate;
            message.resizeTextForBestFit = true;
            message.resizeTextMinSize = Mathf.Max(26, messageFontSize - 10);
            message.resizeTextMaxSize = messageFontSize;
            message.lineSpacing = 1.15f;
            message.text = dialogueMessage;
            message.raycastTarget = false;

            speechBubbleRoot = bubble;
            uiDialogueText = message;
            ApplyBubbleTransformAndStyles();
        }

        private static GameObject CreateTextObject(
            string objectName, Transform parent, Vector2 anchorMin, Vector2 anchorMax)
        {
            GameObject textObject = new GameObject(objectName,
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            textObject.transform.SetParent(parent, false);
            RectTransform rect = textObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return textObject;
        }

        [ContextMenu("Apply Bubble Style & Position")]
        public void ApplyBubbleTransformAndStyles()
        {
            if (speechBubbleRoot == null)
                return;

            Transform root = speechBubbleRoot.transform;
            root.localPosition = new Vector3(0f, bubbleHeight, bubbleForwardOffset);
            root.localScale = Vector3.one * bubbleScale;
            RectTransform canvasRect = speechBubbleRoot.GetComponent<RectTransform>();
            if (canvasRect != null)
                canvasRect.sizeDelta = new Vector2(panelWidth, panelHeight);

            if (speechBubbleRoot.GetComponent<LookAtPlayerYOnly>() == null)
                speechBubbleRoot.AddComponent<LookAtPlayerYOnly>();

            Transform titleTransform = speechBubbleRoot.transform.Find("PanelBackground/TitleText");
            if (titleTransform != null)
            {
                Text title = titleTransform.GetComponent<Text>();
                if (title != null)
                    title.fontSize = titleFontSize;
                TMP_Text tmpTitle = titleTransform.GetComponent<TMP_Text>();
                if (tmpTitle != null)
                    tmpTitle.fontSize = titleFontSize;
                KeepTextReadable(titleTransform);
            }

            Transform messageTransform = speechBubbleRoot.transform.Find("PanelBackground/MessageText");
            if (messageTransform != null)
            {
                Text message = messageTransform.GetComponent<Text>();
                if (message != null)
                {
                    message.fontSize = messageFontSize;
                    message.resizeTextMinSize = Mathf.Max(26, messageFontSize - 10);
                    message.resizeTextMaxSize = messageFontSize;
                    message.lineSpacing = 1.15f;
                    uiDialogueText = message;
                }

                TMP_Text tmpMessage = messageTransform.GetComponent<TMP_Text>();
                if (tmpMessage != null)
                {
                    tmpMessage.fontSize = messageFontSize;
                    tmpDialogueText = tmpMessage;
                }
                KeepTextReadable(messageTransform);
            }
        }

        private static void KeepTextReadable(Transform textTransform)
        {
            Vector3 scale = textTransform.localScale;
            textTransform.localScale = new Vector3(
                Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        }

        [ContextMenu("Test Trigger Dialogue")]
        public void TriggerDialogue()
        {
            if (triggerOnce && hasTriggered)
                return;

            hasTriggered = true;
            EnsureSpeechBubbleExists();
            ApplyBubbleTransformAndStyles();

            if (tmpDialogueText != null)
                tmpDialogueText.text = dialogueMessage;
            else if (uiDialogueText != null)
                uiDialogueText.text = dialogueMessage;

            if (speechBubbleRoot != null)
            {
                speechBubbleRoot.SetActive(true);
                if (displayDuration > 0f)
                {
                    if (hideCoroutine != null)
                        StopCoroutine(hideCoroutine);
                    hideCoroutine = StartCoroutine(HideBubbleRoutine(displayDuration));
                }
            }

            if (npcAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
                npcAnimator.SetTrigger(animationTriggerName);

            if (voiceAudioSource != null && voiceAudioSource.clip != null)
                voiceAudioSource.Play();
        }

        public void ShowPersistentDialogue(string heading, string message)
        {
            if (string.IsNullOrWhiteSpace(heading) || string.IsNullOrWhiteSpace(message))
            {
                Debug.LogError("NPC dialogue heading and message must both contain text.", this);
                return;
            }

            EnsureSpeechBubbleExists();
            ApplyBubbleTransformAndStyles();

            Transform titleTransform = speechBubbleRoot.transform.Find("PanelBackground/TitleText");
            if (titleTransform != null)
            {
                Text title = titleTransform.GetComponent<Text>();
                if (title != null)
                    title.text = heading;
                TMP_Text tmpTitle = titleTransform.GetComponent<TMP_Text>();
                if (tmpTitle != null)
                    tmpTitle.text = heading;
            }

            if (tmpDialogueText != null)
                tmpDialogueText.text = message;
            else if (uiDialogueText != null)
                uiDialogueText.text = message;
            else
            {
                Debug.LogError("NPC dialogue could not find its message text component.", this);
                return;
            }

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }
            speechBubbleRoot.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null)
                return;

            bool isPlayer = false;
            foreach (string playerTag in playerTags)
            {
                if (other.CompareTag(playerTag))
                {
                    isPlayer = true;
                    break;
                }
            }

            if (!isPlayer &&
                (other.GetComponentInParent<CharacterController>() != null ||
                 other.GetComponent<Camera>() != null))
            {
                isPlayer = true;
            }

            if (isPlayer)
                TriggerDialogue();
        }

        private IEnumerator HideBubbleRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (speechBubbleRoot != null)
                speechBubbleRoot.SetActive(false);
        }

        public void ResetDialogue()
        {
            hasTriggered = false;
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }
            if (speechBubbleRoot != null)
                speechBubbleRoot.SetActive(false);
        }
    }
}
