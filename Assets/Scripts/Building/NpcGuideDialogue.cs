using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CampusEmergency.Audio;

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
        private string persistentMessage;
        private float nextVoiceTime;
        private enum GuideCard { None, Introduction, Tasks, Completion, Review }
        private const string SafetyReview =
            "SAFETY REVIEW\n\n" +
            "Use the designated stairs during a fire alarm. Lifts may become unsafe or unavailable.\n\n" +
            "Check exit signs as you move. If a route is closed, stop and find a marked available route.\n\n" +
            "This 2F endpoint ends the prototype, not a real evacuation. In a real emergency, " +
            "follow official signs and on-site instructions to the designated assembly point.";
        private GuideCard guideCard;
        private Button continueButton;
        private RawImage evacuationImage;
        private Text teleportInstructions;
        private bool rerouteGuideActive;
        private bool hasRouteExerciseButton;
        private Button completionContinueButton;
        private Button completionBackButton;
        private UnityEngine.Events.UnityAction returnToStartAction;

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

            if (guideCard != GuideCard.None)
                ApplyGuideCardLayout();
        }

        public void ShowIntroduction(string message, UnityEngine.Events.UnityAction onContinue)
        {
            if (string.IsNullOrWhiteSpace(message) || onContinue == null)
            {
                Debug.LogError("NPC introduction requires welcome text and a Continue callback.", this);
                return;
            }
            ShowPersistentDialogue("WELCOME", message);
            guideCard = GuideCard.Introduction;
            ConfigureCardButton("Continue", () =>
            {
                continueButton.interactable = false;
                onContinue();
            });
            ApplyGuideCardLayout();
            CampusAudioDirector.Instance?.PlayPopup();
        }

        private void ConfigureCardButton(string labelText, UnityEngine.Events.UnityAction onClick)
        {
            Transform panel = speechBubbleRoot.transform.Find("PanelBackground");
            if (continueButton == null)
                continueButton = CreateCardButton("Continue", panel);
            continueButton.GetComponentInChildren<Text>().text = labelText;
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(onClick);
            continueButton.interactable = true;
            if (speechBubbleRoot.GetComponent<GraphicRaycaster>() == null)
                speechBubbleRoot.AddComponent<GraphicRaycaster>();
            if (speechBubbleRoot.GetComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>() == null)
                speechBubbleRoot.AddComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>();
            speechBubbleRoot.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        private static Button CreateCardButton(string objectName, Transform panel)
        {
            var buttonObject = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer),
                typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(panel, false);
            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.12f, 0.65f, 0.76f, 1f);
            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;
            GameObject labelObject = CreateTextObject("Label", buttonObject.transform, Vector2.zero, Vector2.one);
            Text label = labelObject.GetComponent<Text>();
            label.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            label.fontSize = 44;
            label.alignment = TextAnchor.MiddleCenter;
            label.color = Color.white;
            label.raycastTarget = false;
            return button;
        }

        public void ShowTaskGuide(string taskList, Texture2D diagram,
            UnityEngine.Events.UnityAction startRouteExercise = null, bool routeExerciseActive = false)
        {
            if (string.IsNullOrWhiteSpace(taskList) || diagram == null)
            {
                Debug.LogError("NPC task guide requires a task list and evacuation diagram Texture2D.", this);
                return;
            }
            ShowPersistentDialogue("EVACUATION TASK GUIDE", taskList);
            guideCard = GuideCard.Tasks;
            rerouteGuideActive = routeExerciseActive;
            hasRouteExerciseButton = startRouteExercise != null;
            if (hasRouteExerciseButton)
                ConfigureCardButton("Start reroute", startRouteExercise);
            Transform panel = speechBubbleRoot.transform.Find("PanelBackground");
            if (evacuationImage == null)
            {
                var frame = new GameObject("Evacuation Diagram Frame", typeof(RectTransform));
                frame.transform.SetParent(panel, false);
                SetAnchors(frame.transform, new Vector2(0.53f, 0.27f), new Vector2(0.97f, 0.80f));
                var imageObject = new GameObject("Evacuation Diagram", typeof(RectTransform), typeof(CanvasRenderer),
                    typeof(RawImage), typeof(AspectRatioFitter));
                imageObject.transform.SetParent(frame.transform, false);
                evacuationImage = imageObject.GetComponent<RawImage>();
                evacuationImage.raycastTarget = false;
                imageObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            }
            evacuationImage.texture = diagram;
            evacuationImage.GetComponent<AspectRatioFitter>().aspectRatio = (float)diagram.width / diagram.height;
            if (teleportInstructions == null)
            {
                GameObject note = CreateTextObject("Teleport Instructions", panel,
                    new Vector2(0.04f, 0.03f), new Vector2(0.96f, 0.23f));
                teleportInstructions = note.GetComponent<Text>();
                teleportInstructions.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                teleportInstructions.fontSize = 36;
                teleportInstructions.alignment = TextAnchor.MiddleLeft;
                teleportInstructions.color = new Color(1f, 0.85f, 0.25f, 1f);
                teleportInstructions.raycastTarget = false;
            }
            teleportInstructions.text = routeExerciseActive
                ? "Reroute exercise: check the environment signs and the current objective. " +
                    "The diagram is hidden to avoid revealing the alternative route.\n" +
                    "Confirm marked teleport points to activate tutorial effects."
                : hasRouteExerciseButton
                    ? "Use the marked teleport points and confirm to activate tutorial effects.\n" +
                        "Optional: select Start reroute to practise responding to a route closure."
                    : "Use the marked teleport points to trigger the corresponding tutorial effects. " +
                        "Aim at a teleport point and confirm the teleport; pointing alone does not activate it.\n" +
                        "Follow the red arrows on the evacuation diagram to the stairs.";
            ApplyGuideCardLayout();
        }

        private void ApplyGuideCardLayout()
        {
            bool tasks = guideCard == GuideCard.Tasks;
            bool completion = guideCard == GuideCard.Completion || guideCard == GuideCard.Review;
            Vector2 size = tasks ? new Vector2(1700f, hasRouteExerciseButton ? 1200f : 1050f) :
                guideCard == GuideCard.Review ? new Vector2(1250f, 1050f) : new Vector2(1250f, 650f);
            float heightOffset = completion ? 0.4f + (size.y - panelHeight) * bubbleScale * 0.5f :
                0.5f;
            speechBubbleRoot.transform.localPosition =
                new Vector3(0f, bubbleHeight + heightOffset, bubbleForwardOffset);
            speechBubbleRoot.GetComponent<RectTransform>().sizeDelta = size;
            Transform panel = speechBubbleRoot.transform.Find("PanelBackground");
            SetAnchors(panel.Find("TitleText"), new Vector2(0.04f, 0.85f), new Vector2(0.96f, 0.98f));
            SetAnchors(panel.Find("BorderAccent"), new Vector2(0.02f, 0.82f), new Vector2(0.98f, 0.83f));
            SetAnchors(panel.Find("MessageText"),
                tasks ? new Vector2(0.04f, hasRouteExerciseButton ? 0.32f : 0.27f) : new Vector2(0.05f, 0.32f),
                tasks ? new Vector2(rerouteGuideActive ? 0.96f : 0.49f, 0.80f) : new Vector2(0.95f, 0.80f));
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(!tasks || hasRouteExerciseButton);
                SetAnchors(continueButton.transform,
                    new Vector2(tasks && hasRouteExerciseButton ? 0.74f : 0.30f, tasks ? 0.02f : 0.06f),
                    new Vector2(tasks && hasRouteExerciseButton ? 0.97f : 0.70f, tasks ? 0.12f : 0.26f));
                if (guideCard == GuideCard.Completion && returnToStartAction != null)
                    SetAnchors(continueButton.transform, new Vector2(0.30f, 0.18f), new Vector2(0.70f, 0.30f));
            }
            bool showCompletionActions = guideCard == GuideCard.Completion && returnToStartAction != null;
            if (completionContinueButton != null)
            {
                completionContinueButton.gameObject.SetActive(showCompletionActions);
                SetAnchors(completionContinueButton.transform, new Vector2(0.06f, 0.04f), new Vector2(0.48f, 0.16f));
            }
            if (completionBackButton != null)
            {
                completionBackButton.gameObject.SetActive(showCompletionActions);
                SetAnchors(completionBackButton.transform, new Vector2(0.52f, 0.04f), new Vector2(0.94f, 0.16f));
            }
            if (evacuationImage != null)
                evacuationImage.transform.parent.gameObject.SetActive(tasks && !rerouteGuideActive);
            if (teleportInstructions != null)
            {
                teleportInstructions.gameObject.SetActive(tasks);
                SetAnchors(teleportInstructions.transform,
                    new Vector2(0.04f, hasRouteExerciseButton ? 0.14f : 0.03f),
                    new Vector2(0.96f, hasRouteExerciseButton ? 0.30f : 0.23f));
            }
        }

        private static void SetAnchors(Transform target, Vector2 min, Vector2 max)
        {
            if (target == null)
                return;
            RectTransform rect = target.GetComponent<RectTransform>();
            rect.anchorMin = min;
            rect.anchorMax = max;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
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

            // Multiple player colliders must not restart the same spoken sentence.
            if (Time.time < nextVoiceTime)
                return;

            hasTriggered = true;
            EnsureSpeechBubbleExists();
            ApplyBubbleTransformAndStyles();

            string spokenMessage = string.IsNullOrEmpty(persistentMessage) ? dialogueMessage : persistentMessage;
            if (tmpDialogueText != null)
                tmpDialogueText.text = spokenMessage;
            else if (uiDialogueText != null)
                uiDialogueText.text = spokenMessage;

            var audio = CampusAudioDirector.Instance;
            AudioClip voice = voiceAudioSource != null && voiceAudioSource.clip != null && string.IsNullOrEmpty(persistentMessage)
                ? voiceAudioSource.clip : audio != null ? audio.FindVoice(spokenMessage) : null;
            audio?.PlayPopup();
            float voiceDelay = voice != null && audio != null ? audio.SpeakNpc(voice, transform) : 0f;
            float visibleDuration = voice != null ? Mathf.Max(displayDuration, voiceDelay + voice.length + 1f) : displayDuration;
            nextVoiceTime = Time.time + Mathf.Max(10f, voice != null ? voiceDelay + voice.length + 1f : 0f);

            if (speechBubbleRoot != null)
            {
                speechBubbleRoot.SetActive(true);
                if (displayDuration > 0f && string.IsNullOrEmpty(persistentMessage))
                {
                    if (hideCoroutine != null)
                        StopCoroutine(hideCoroutine);
                    hideCoroutine = StartCoroutine(HideBubbleRoutine(visibleDuration));
                }
            }

            if (npcAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
                npcAnimator.SetTrigger(animationTriggerName);

            if (audio == null && voiceAudioSource != null && voiceAudioSource.clip != null)
                voiceAudioSource.Play();
        }

        public void ShowCompletionDialogue(UnityEngine.Events.UnityAction onReturnToStart = null)
        {
            returnToStartAction = onReturnToStart;
            TriggerDialogue();
            ShowCompletionCard();
        }

        private void ShowCompletionCard()
        {
            ShowPersistentDialogue("SCENE 1 COMPLETE", dialogueMessage);
            guideCard = GuideCard.Completion;
            ConfigureCardButton("Review safety", ShowSafetyReview);
            if (returnToStartAction != null)
            {
                Transform panel = speechBubbleRoot.transform.Find("PanelBackground");
                if (completionContinueButton == null)
                {
                    completionContinueButton = CreateCardButton("Continue Experience", panel);
                    completionContinueButton.GetComponentInChildren<Text>().text = "Continue";
                }
                if (completionBackButton == null)
                {
                    completionBackButton = CreateCardButton("Return To Start", panel);
                    completionBackButton.GetComponentInChildren<Text>().text = "Back";
                }
                completionBackButton.onClick.RemoveAllListeners();
                completionBackButton.onClick.AddListener(returnToStartAction);
            }
            ApplyGuideCardLayout();
        }

        private void ShowSafetyReview()
        {
            ShowPersistentDialogue("SCENE 1 REVIEW", SafetyReview);
            guideCard = GuideCard.Review;
            ConfigureCardButton("Back to results", ShowCompletionCard);
            ApplyGuideCardLayout();
            CampusAudioDirector.Instance?.PlayPopup();
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

            persistentMessage = message;

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
            nextVoiceTime = 0f;
            persistentMessage = null;
            guideCard = GuideCard.None;
            returnToStartAction = null;
            if (completionContinueButton != null)
                completionContinueButton.gameObject.SetActive(false);
            if (completionBackButton != null)
                completionBackButton.gameObject.SetActive(false);
            if (continueButton != null)
                continueButton.gameObject.SetActive(false);
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
