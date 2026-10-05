using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace CampusEmergency.UI
{
    [DisallowMultipleComponent]
    public sealed class ControllerGuidePanel : MonoBehaviour
    {
        [SerializeField] private Transform spawnAnchor;

        private const float CanvasScale = 0.0016f;
        private const float PanelWidth = 1600f;
        private const float PanelHeight = 1000f;

        private static readonly string[] TutorialSteps =
        {
            "Move with the left joystick",
            "Turn with the right joystick",
            "Teleport",
            "Cancel teleport",
            "Interact with the trigger"
        };

        private static readonly Color PanelColor = new Color(0.035f, 0.075f, 0.12f, 0.96f);
        private static readonly Color CardColor = new Color(0.08f, 0.15f, 0.22f, 0.98f);
        private static readonly Color AccentColor = new Color(0.23f, 0.78f, 0.9f, 1f);
        private static readonly Color MainTextColor = new Color(0.91f, 0.96f, 0.99f, 1f);
        private static readonly Color SecondaryTextColor = new Color(0.68f, 0.79f, 0.86f, 1f);

        private readonly List<BaseTeleportationInteractable> teleportTargets = new List<BaseTeleportationInteractable>();

        private InputAction moveAction;
        private InputAction turnAction;
        private InputAction teleportCancelAction;
        private InputAction activateAction;
        private Text progressText;
        private Text feedbackText;
        private int currentStep;

        private void Awake()
        {
            if (spawnAnchor == null)
            {
                Debug.LogError("Controller guide requires the classroom spawn anchor.", this);
                enabled = false;
                return;
            }

            FindTutorialActions();
            RegisterTeleportTargets();
            BuildPanel();
            UpdateTutorialText("Gently push the left joystick to move smoothly.");
        }

        private void OnDestroy()
        {
            if (moveAction != null)
                moveAction.performed -= OnMove;
            if (turnAction != null)
                turnAction.performed -= OnTurn;
            if (teleportCancelAction != null)
                teleportCancelAction.performed -= OnTeleportCancel;
            if (activateAction != null)
                activateAction.performed -= OnActivate;

            foreach (BaseTeleportationInteractable target in teleportTargets)
            {
                if (target != null)
                    target.teleporting.RemoveListener(OnTeleport);
            }
        }

        private void FindTutorialActions()
        {
            moveAction = FindAction("XRI Left Locomotion", "Move");
            turnAction = FindAction("XRI Right Locomotion", "Turn");
            teleportCancelAction = FindAction("XRI Right Locomotion", "Teleport Mode Cancel");
            activateAction = FindAction("XRI Right Interaction", "Activate");

            RegisterAction(moveAction, OnMove);
            RegisterAction(turnAction, OnTurn);
            RegisterAction(teleportCancelAction, OnTeleportCancel);
            RegisterAction(activateAction, OnActivate);
        }

        private static InputAction FindAction(string mapName, string actionName)
        {
            InputActionReference[] references = Resources.FindObjectsOfTypeAll<InputActionReference>();
            foreach (InputActionReference reference in references)
            {
                InputAction action = reference != null ? reference.action : null;
                if (action != null && action.actionMap != null &&
                    action.actionMap.name == mapName && action.name == actionName)
                    return action;
            }

            Debug.LogError($"Controller guide could not find input action '{mapName}/{actionName}'.");
            return null;
        }

        private static void RegisterAction(
            InputAction action,
            System.Action<InputAction.CallbackContext> callback)
        {
            if (action == null)
                return;

            action.performed += callback;
        }

        private void RegisterTeleportTargets()
        {
            BaseTeleportationInteractable[] targets =
                FindObjectsByType<BaseTeleportationInteractable>(FindObjectsSortMode.None);
            foreach (BaseTeleportationInteractable target in targets)
            {
                target.teleporting.AddListener(OnTeleport);
                teleportTargets.Add(target);
            }

            if (teleportTargets.Count == 0)
                Debug.LogError("Controller guide could not find any teleport targets to observe.");
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (currentStep == 0 && context.ReadValue<Vector2>().sqrMagnitude > 0.09f)
                CompleteStep("Movement detected. Push the right joystick left or right to turn.");
        }

        private void OnTurn(InputAction.CallbackContext context)
        {
            if (currentStep == 1 && context.ReadValue<Vector2>().sqrMagnitude > 0.09f)
                CompleteStep("Turning detected. Push the right joystick up, aim at a valid surface, and teleport.");
        }

        private void OnTeleport(TeleportingEventArgs args)
        {
            if (currentStep == 2)
                CompleteStep("Teleport successful. Push the right joystick up to aim again, then press the right grip to cancel.");
        }

        private void OnTeleportCancel(InputAction.CallbackContext context)
        {
            if (currentStep == 3)
                CompleteStep("Cancellation detected. Point the ray at an interactive object and press the right trigger.");
        }

        private void OnActivate(InputAction.CallbackContext context)
        {
            if (currentStep == 4)
                CompleteStep("Trigger detected. You have completed all controller exercises!");
        }

        private void CompleteStep(string feedback)
        {
            currentStep++;
            UpdateTutorialText(feedback);
        }

        private void UpdateTutorialText(string feedback)
        {
            if (progressText == null || feedbackText == null)
                return;

            progressText.text = currentStep < TutorialSteps.Length
                ? $"Practice progress  {currentStep + 1}/{TutorialSteps.Length}  ·  {TutorialSteps[currentStep]}"
                : "Practice progress  All complete";
            feedbackText.text = feedback;
        }

        private void BuildPanel()
        {
            var font = CreateGuideFont();
            if (font == null)
            {
                Debug.LogError("Controller guide could not load a system font.", this);
                return;
            }

            var canvasObject = new GameObject("Controller Guide Canvas", typeof(RectTransform), typeof(Canvas));
            canvasObject.transform.SetParent(transform, false);
            canvasObject.transform.position = spawnAnchor.TransformPoint(new Vector3(0f, 1.8f, 2.35f));
            canvasObject.transform.rotation = spawnAnchor.rotation * Quaternion.Euler(0f, 180f, 0f);
            canvasObject.transform.localScale = new Vector3(-CanvasScale, CanvasScale, CanvasScale);

            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 20;

            var canvasRect = canvasObject.GetComponent<RectTransform>();
            canvasRect.sizeDelta = new Vector2(PanelWidth, PanelHeight);

            CreateImage("Panel Background", canvasRect, PanelColor, Vector2.zero, Vector2.one);
            CreateImage("Header Accent", canvasRect, AccentColor, new Vector2(0.04f, 0.87f), new Vector2(0.96f, 0.875f));

            CreateText("Title", canvasRect, font, "Controller Quick Guide", 62, AccentColor,
                new Vector2(0.055f, 0.88f), new Vector2(0.95f, 0.98f), TextAnchor.MiddleLeft);
            CreateText("Subtitle", canvasRect, font, "Getting Started · Current Controls", 30, SecondaryTextColor,
                new Vector2(0.06f, 0.80f), new Vector2(0.95f, 0.87f), TextAnchor.MiddleLeft);

            CreateImage("Movement Card", canvasRect, CardColor, new Vector2(0.045f, 0.245f), new Vector2(0.49f, 0.77f));
            CreateImage("Interaction Card", canvasRect, CardColor, new Vector2(0.51f, 0.245f), new Vector2(0.955f, 0.77f));

            CreateText("Movement Heading", canvasRect, font, "Movement and Turning", 40, AccentColor,
                new Vector2(0.075f, 0.67f), new Vector2(0.46f, 0.75f), TextAnchor.MiddleLeft);
            CreateText("Movement Instructions", canvasRect, font,
                "Left joystick\nPush to walk smoothly\nMove speed: about 1.2 m/s\n\nRight joystick\nPush left or right to turn smoothly\nTurn speed: about 20 degrees/s",
                34, MainTextColor, new Vector2(0.075f, 0.29f), new Vector2(0.46f, 0.67f), TextAnchor.UpperLeft);

            CreateText("Interaction Heading", canvasRect, font, "Teleporting and Interacting", 40, AccentColor,
                new Vector2(0.54f, 0.67f), new Vector2(0.92f, 0.75f), TextAnchor.MiddleLeft);
            CreateText("Interaction Instructions", canvasRect, font,
                "Teleport\nPush the right joystick up to aim\nPoint at a valid surface to teleport\nRight grip: cancel teleport\n\nObject interaction\nTrigger: activate the targeted door or button\nGrip: grab or select (when supported)",
                31, MainTextColor, new Vector2(0.54f, 0.28f), new Vector2(0.92f, 0.67f), TextAnchor.UpperLeft);

            progressText = CreateText("Tutorial Progress", canvasRect, font, string.Empty, 30, AccentColor,
                new Vector2(0.055f, 0.14f), new Vector2(0.95f, 0.22f), TextAnchor.MiddleLeft);
            feedbackText = CreateText("Tutorial Feedback", canvasRect, font, string.Empty, 25, SecondaryTextColor,
                new Vector2(0.055f, 0.045f), new Vector2(0.95f, 0.14f), TextAnchor.MiddleLeft);
        }

        private static Font CreateGuideFont()
        {
            return Font.CreateDynamicFontFromOSFont(
                new[]
                {
                    "Arial",
                    "Segoe UI",
                    "Noto Sans"
                },
                64);
        }

        private static Image CreateImage(string objectName, RectTransform parent, Color color, Vector2 anchorMin, Vector2 anchorMax)
        {
            var imageObject = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            var rect = imageObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var image = imageObject.GetComponent<Image>();
            image.color = color;
            image.raycastTarget = false;
            return image;
        }

        private static Text CreateText(
            string objectName,
            RectTransform parent,
            Font font,
            string content,
            int fontSize,
            Color color,
            Vector2 anchorMin,
            Vector2 anchorMax,
            TextAnchor alignment)
        {
            var textObject = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            var rect = textObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var text = textObject.GetComponent<Text>();
            text.font = font;
            text.text = content;
            text.fontSize = fontSize;
            text.color = color;
            text.alignment = alignment;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = Mathf.Max(22, fontSize - 8);
            text.resizeTextMaxSize = fontSize;
            text.raycastTarget = false;
            return text;
        }
    }
}
