using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using CampusEmergency.Building;
using CampusEmergency.Audio;

namespace CampusEmergency.UI
{
    [DisallowMultipleComponent]
    public sealed class ControllerGuidePanel : MonoBehaviour
    {
        [SerializeField] private Transform spawnAnchor;
        [SerializeField] private Camera headCamera;
        [SerializeField] private Transform downStairStart;
        [SerializeField] private Transform downStairAssist;
        [SerializeField] private Transform downStairAssist1;
        [SerializeField] private Transform downStairAssist2;
        [SerializeField] private Transform downStairFinished;
        [SerializeField] private NpcGuideDialogue elizabethGuideDialogue;
        [SerializeField] private NpcGuideDialogue elizabethEndDialogue;
        [SerializeField] private Texture2D evacuationDiagram;

        private const float CanvasScale = 0.001f;
        private const float PanelWidth = 1600f;
        private const float PanelHeight = 1000f;
        private const float HudCanvasScale = 0.0009f;
        private const float HudWidth = 1800f;
        private const float HudHeight = 500f;
        private const float RoutePointTolerance = 2.5f;
        private const float MinimumUiDistance = 0.65f;
        private const float MaximumUiDistance = 2.2f;
        private const float UiDistanceStep = 0.2f;
        private const float InitialGuideDistance = 1.4f;
        private const float InitialHudDistance = 1.1f;
        private const string ElizabethStartObjectName = "Elizabeth Warren animation Start";
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
        private static readonly string[] PracticeTasks =
        {
            "Learn the controls",
            "Read signs and choose the stair route",
            "Enter the stair route",
            "Follow stair landing 1",
            "Follow stair landing 2",
            "Reach the 2F practice endpoint"
        };

        private enum RouteProgress
        {
            AwaitingStart,
            RouteChoiceStarted,
            EnteredStairs,
            ReachedAssist1,
            ReachedAssist2,
            Completed
        }

        private readonly List<BaseTeleportationInteractable> teleportTargets = new List<BaseTeleportationInteractable>();
        private readonly bool[] completedTasks = new bool[PracticeTasks.Length];

        private InputAction moveAction;
        private InputAction turnAction;
        private InputAction teleportCancelAction;
        private InputAction activateAction;
        private GameObject guideCanvasObject;
        private GameObject hudCanvasObject;
        private GameObject pauseCanvasObject;
        private Text progressText;
        private Text feedbackText;
        private Text welcomeText;
        private Text hudFloorText;
        private Text hudObjectiveText;
        private Text hudTasksText;
        private Text hudTitleText;
        private Text hudCollapseLabel;
        private Button beginGuideButton;
        private Button skipGuideButton;
        private Button guideNearerButton;
        private Button guideFartherButton;
        private Button hudNearerButton;
        private Button hudFartherButton;
        private Button hudPauseButton;
        private Button hudReviewButton;
        private Button hudCollapseButton;
        private RectTransform hudRect;
        private int currentStep;
        private int currentTaskIndex;
        private bool guideInProgress;
        private bool floorTrackingConnected;
        private bool cameraWarningLogged;
        private bool hudExpanded = true;
        private RouteProgress routeProgress;
        private string currentFloor = "3F";
        private string currentObjective = "Review the controls";
        private float previousTimeScale = 1f;
        private bool isPaused;
        private bool endDialogueTriggered;
        private bool introductionPending = true;

        private void Awake()
        {
            if (spawnAnchor == null)
            {
                Debug.LogError("Controller guide requires the classroom spawn anchor.", this);
                enabled = false;
                return;
            }

            FindTutorialActions();
            ValidateRoutePoints();
            RegisterTeleportTargets();
            BuildPanel();
            BuildNavigationHud();
            BuildPauseMenu();
            if (guideCanvasObject == null || hudCanvasObject == null || pauseCanvasObject == null)
            {
                enabled = false;
                return;
            }

            guideCanvasObject.SetActive(false);
        }

        private IEnumerator Start()
        {
            // Let NPC Awake finish before showing its persistent welcome card.
            yield return null;
            if (elizabethGuideDialogue == null)
                elizabethGuideDialogue = FindElizabethStartGuideDialogue();
            if (elizabethGuideDialogue == null || evacuationDiagram == null)
            {
                Debug.LogError("Introduction requires the Start NPC and the evacuation diagram Texture2D.", this);
                yield break;
            }
            elizabethGuideDialogue.ShowIntroduction(
                "Welcome to the Requirements and Prototype Proposal Demonstration System!",
                ContinueIntroduction);
        }

        private void ContinueIntroduction()
        {
            if (!introductionPending)
                return;
            introductionPending = false;
            CampusAudioDirector.Instance?.PlayClick();
            ShowWelcome();
        }

        private void OnDestroy()
        {
            if (isPaused)
                Time.timeScale = previousTimeScale;

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

        private void LateUpdate()
        {
            if (!TryGetHeadCamera())
                return;

            Transform cameraTransform = headCamera.transform;
            if (!isPaused)
            {
                var origin = headCamera.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>();
                Vector3 feet = origin != null ? origin.transform.position : cameraTransform.position - Vector3.up * 1.6f;
                TryShowEndDialogue(feet);
                if (!introductionPending)
                    AdvanceRoute(feet);
            }
            PositionPanelInFront(pauseCanvasObject, InitialGuideDistance, -0.02f);
            UpdateDistanceButtons(guideCanvasObject, guideNearerButton, guideFartherButton);
            UpdateDistanceButtons(hudCanvasObject, hudNearerButton, hudFartherButton);
        }

        private bool TryGetHeadCamera()
        {
            if (headCamera == null)
                headCamera = Camera.main;

            if (headCamera == null)
            {
                if (!cameraWarningLogged)
                {
                    cameraWarningLogged = true;
                    Debug.LogError("Practice UI requires a MainCamera for positioning and distance adjustment.", this);
                }
                return false;
            }

            return true;
        }

        private void PositionPanelInFront(GameObject canvasObject, float distance, float verticalOffset)
        {
            if (canvasObject == null || !canvasObject.activeSelf || !TryGetHeadCamera())
                return;

            Transform cameraTransform = headCamera.transform;
            Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
            if (forward.sqrMagnitude < 0.001f)
                forward = Vector3.ProjectOnPlane(cameraTransform.up, Vector3.up);
            forward.Normalize();

            Vector3 position = cameraTransform.position + forward * distance + Vector3.up * verticalOffset;
            Quaternion rotation = Quaternion.LookRotation(-forward, Vector3.up);
            canvasObject.transform.SetPositionAndRotation(position, rotation);
        }

        private void ValidateRoutePoints()
        {
            if (downStairStart == null || downStairAssist == null ||
                downStairAssist1 == null || downStairAssist2 == null ||
                downStairFinished == null)
            {
                Debug.LogError("Practice route requires Inspector references to all five DownStair waypoint transforms.", this);
            }

            if (elizabethEndDialogue == null)
                Debug.LogError("Practice endpoint requires the End NPC dialogue reference.", this);
        }

        private void TryShowEndDialogue(Vector3 playerPosition)
        {
            if (endDialogueTriggered || downStairFinished == null || elizabethEndDialogue == null ||
                !elizabethEndDialogue.isActiveAndEnabled)
                return;

            bool nearEndpoint = IsRouteDestination(playerPosition, downStairFinished);
            bool nearNpc = (playerPosition - elizabethEndDialogue.transform.position).sqrMagnitude <=
                RoutePointTolerance * RoutePointTolerance;
            if (!nearEndpoint && !nearNpc)
                return;

            elizabethEndDialogue.ShowCompletionDialogue();
            endDialogueTriggered = true;
        }

        private GameObject CreateWorldCanvas(string objectName, float width, float height, float canvasScale, int sortingOrder, Vector3 localPosition)
        {
            var canvasObject = new GameObject(objectName, typeof(RectTransform), typeof(Canvas));
            canvasObject.transform.SetParent(spawnAnchor, false);
            canvasObject.transform.localPosition = localPosition;
            canvasObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            canvasObject.transform.localScale = new Vector3(-canvasScale, canvasScale, canvasScale);

            Canvas canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = sortingOrder;
            RectTransform rect = canvasObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(width, height);
            canvasObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>();
            return canvasObject;
        }

        private void FindTutorialActions()
        {
            moveAction = FindAction("XRI Left Locomotion", "Move");
            turnAction = FindAction("XRI Right Locomotion", "Snap Turn");
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
            if (guideInProgress && currentStep == 0 && context.ReadValue<Vector2>().sqrMagnitude > 0.09f)
                CompleteStep("Movement detected. Push the right joystick left or right to turn.");
        }

        private void OnTurn(InputAction.CallbackContext context)
        {
            if (guideInProgress && currentStep == 1 && context.ReadValue<Vector2>().sqrMagnitude > 0.09f)
                CompleteStep("45-degree snap turn detected. Push the right joystick up, aim at a valid surface, and teleport.");
        }

        private void OnTeleport(TeleportingEventArgs args)
        {
            if (args == null)
                return;

            StartCoroutine(ConfirmTeleport(args.teleportRequest.destinationPosition));
        }

        private IEnumerator ConfirmTeleport(Vector3 destination)
        {
            yield return null;
            yield return new WaitForEndOfFrame();

            if (headCamera == null)
                headCamera = Camera.main;

            if (headCamera == null)
            {
                if (!cameraWarningLogged)
                {
                    cameraWarningLogged = true;
                    Debug.LogError("Cannot confirm the teleport because no XR camera was found.", this);
                }
                yield break;
            }

            Vector3 cameraPosition = headCamera.transform.position;
            Vector2 cameraPositionXZ = new Vector2(cameraPosition.x, cameraPosition.z);
            Vector2 destinationXZ = new Vector2(destination.x, destination.z);
            if ((cameraPositionXZ - destinationXZ).sqrMagnitude > RoutePointTolerance * RoutePointTolerance)
                yield break;

            AdvanceRoute(destination);
            if (guideInProgress && currentStep == 2)
                CompleteStep("Teleport successful. Push the right joystick up to aim again, then press the right grip to cancel.");
        }

        public UnityEvent onTeleportCornerReached;

        private void AdvanceRoute(Vector3 destination)
        {
            if (downStairStart == null || downStairAssist == null ||
                downStairAssist1 == null || downStairAssist2 == null ||
                downStairFinished == null || routeProgress == RouteProgress.Completed)
                return;

            switch (routeProgress)
            {
                case RouteProgress.AwaitingStart when IsRouteDestination(destination, downStairStart):
                    CampusAudioDirector.Instance?.BeginEmergency();
                    routeProgress = RouteProgress.RouteChoiceStarted;
                    currentTaskIndex = 1;
                    currentObjective = "At the DownStair corner, read the signs and choose the stair route to 2F.";
                    currentFloor = "3F";
                    floorTrackingConnected = true;
                    onTeleportCornerReached?.Invoke();
                    break;
                case RouteProgress.RouteChoiceStarted when IsRouteDestination(destination, downStairAssist):
                    routeProgress = RouteProgress.EnteredStairs;
                    completedTasks[1] = true;
                    currentTaskIndex = 2;
                    currentObjective = "You have entered the stair route. Follow the next marked landing.";
                    currentFloor = "3F";
                    break;
                case RouteProgress.EnteredStairs when IsRouteDestination(destination, downStairAssist1):
                    routeProgress = RouteProgress.ReachedAssist1;
                    completedTasks[2] = true;
                    currentTaskIndex = 3;
                    currentObjective = "Stair landing 1 reached. Continue by following the stair route.";
                    currentFloor = "3F";
                    break;
                case RouteProgress.ReachedAssist1 when IsRouteDestination(destination, downStairAssist2):
                    routeProgress = RouteProgress.ReachedAssist2;
                    completedTasks[3] = true;
                    currentTaskIndex = 4;
                    currentObjective = "Stair landing 2 reached. Continue to the marked 2F endpoint.";
                    currentFloor = "2F";
                    break;
                case RouteProgress.ReachedAssist2 when IsRouteDestination(destination, downStairFinished):
                    routeProgress = RouteProgress.Completed;
                    completedTasks[4] = true;
                    completedTasks[5] = true;
                    currentTaskIndex = 5;
                    currentObjective = "Route complete. You reached the fictional 2F practice endpoint.";
                    currentFloor = "2F";
                    break;
                default:
                    return;
            }

            if (routeProgress == RouteProgress.Completed)
                CampusAudioDirector.Instance?.PlayCompletion();
            else
                CampusAudioDirector.Instance?.PlayCorrect();
            UpdateNavigationHud();
        }

        private static bool IsRouteDestination(Vector3 destination, Transform waypoint)
        {
            return CampusAudioDirector.NearWaypoint(destination, waypoint, RoutePointTolerance);
        }

        private void OnTeleportCancel(InputAction.CallbackContext context)
        {
            if (guideInProgress && currentStep == 3)
                CompleteStep("Cancellation detected. Point the ray at an interactive object and press the right trigger.");
        }

        private void OnActivate(InputAction.CallbackContext context)
        {
            if (guideInProgress && currentStep == 4)
                CompleteStep("Trigger detected. You have completed all controller exercises!");
        }

        private void CompleteStep(string feedback)
        {
            CampusAudioDirector.Instance?.PlayCorrect();
            currentStep++;
            if (currentStep == TutorialSteps.Length)
            {
                guideInProgress = false;
                if (routeProgress == RouteProgress.AwaitingStart)
                {
                    completedTasks[0] = true;
                    currentTaskIndex = 1;
                    currentObjective = "Controller practice complete. Continue to the DownStair corner.";
                }
                guideCanvasObject.SetActive(false);
                hudCanvasObject.SetActive(true);
                PositionPanelInFront(hudCanvasObject, InitialHudDistance, -0.38f);
                UpdateNavigationHud();
            }

            UpdateTutorialText(feedback);
        }

        private void UpdateTutorialText(string feedback)
        {
            if (progressText == null || feedbackText == null)
                return;

            if (currentStep >= TutorialSteps.Length)
                progressText.text = "Practice progress  All complete";
            else if (guideInProgress)
                progressText.text = $"Practice progress  {currentStep + 1}/{TutorialSteps.Length}  ·  {TutorialSteps[currentStep]}";
            else
                progressText.text = "Optional controller orientation  ·  5 short exercises";
            feedbackText.text = feedback;
        }

        private void BuildPanel()
        {
            Font font = CreateGuideFont();
            if (font == null)
            {
                Debug.LogError("Controller guide could not load a system font.", this);
                return;
            }

            var canvasObject = CreateWorldCanvas(
                "Controller Guide Canvas", PanelWidth, PanelHeight, CanvasScale, 20, new Vector3(0f, 1.8f, 2.35f));
            guideCanvasObject = canvasObject;
            var canvasRect = canvasObject.GetComponent<RectTransform>();

            CreateImage("Panel Background", canvasRect, PanelColor, Vector2.zero, Vector2.one);
            CreateImage("Header Accent", canvasRect, AccentColor, new Vector2(0.04f, 0.87f), new Vector2(0.96f, 0.878f));

            CreateText("Title", canvasRect, font, "Controller Quick Guide", 72, AccentColor,
                new Vector2(0.055f, 0.88f), new Vector2(0.95f, 0.98f), TextAnchor.MiddleLeft);
            CreateText("Subtitle", canvasRect, font, "Welcome to the Campus Emergency Practice · 3F to 2F", 36, SecondaryTextColor,
                new Vector2(0.06f, 0.80f), new Vector2(0.58f, 0.87f), TextAnchor.MiddleLeft);
            guideNearerButton = CreateButton("Guide Nearer Button", canvasRect, font, "Nearer",
                new Vector2(0.62f, 0.80f), new Vector2(0.78f, 0.87f), MoveGuideNearer);
            guideFartherButton = CreateButton("Guide Farther Button", canvasRect, font, "Farther",
                new Vector2(0.80f, 0.80f), new Vector2(0.96f, 0.87f), MoveGuideFarther);

            CreateImage("Movement Card", canvasRect, CardColor, new Vector2(0.045f, 0.245f), new Vector2(0.49f, 0.77f));
            CreateImage("Interaction Card", canvasRect, CardColor, new Vector2(0.51f, 0.245f), new Vector2(0.955f, 0.77f));

            CreateText("Movement Heading", canvasRect, font, "Welcome and Movement", 46, AccentColor,
                new Vector2(0.075f, 0.67f), new Vector2(0.46f, 0.75f), TextAnchor.MiddleLeft);
            welcomeText = CreateText("Movement Instructions", canvasRect, font,
                "Hello! I am your campus safety guide for this fictional practice.\n\nThis activity is for learning, not official emergency guidance. Follow real on-site instructions in an actual emergency.\n\nLeft joystick: move. Right joystick left or right: snap turn 45 degrees.",
                40, MainTextColor, new Vector2(0.075f, 0.29f), new Vector2(0.46f, 0.67f), TextAnchor.UpperLeft);

            CreateText("Interaction Heading", canvasRect, font, "Teleporting and Interacting", 46, AccentColor,
                new Vector2(0.54f, 0.67f), new Vector2(0.92f, 0.75f), TextAnchor.MiddleLeft);
            CreateText("Interaction Instructions", canvasRect, font,
                "Teleport: push the right joystick up, aim at a valid surface, then release to teleport.\n\nCancel teleport: press the right grip.\n\nInteract: point at an available object and press the right trigger. Use grip to grab supported objects.\n\nUse Pause on the navigation panel to pause, restart, or exit.",
                37, MainTextColor, new Vector2(0.54f, 0.28f), new Vector2(0.92f, 0.67f), TextAnchor.UpperLeft);

            progressText = CreateText("Tutorial Progress", canvasRect, font, string.Empty, 36, AccentColor,
                new Vector2(0.055f, 0.14f), new Vector2(0.95f, 0.22f), TextAnchor.MiddleLeft);
            feedbackText = CreateText("Tutorial Feedback", canvasRect, font, string.Empty, 31, SecondaryTextColor,
                new Vector2(0.055f, 0.075f), new Vector2(0.95f, 0.14f), TextAnchor.MiddleLeft);

            beginGuideButton = CreateButton("Begin Guide Button", canvasRect, font, "Begin guided practice",
                new Vector2(0.055f, 0.005f), new Vector2(0.35f, 0.065f), BeginGuide);
            skipGuideButton = CreateButton("Skip Guide Button", canvasRect, font, "Skip controls",
                new Vector2(0.37f, 0.005f), new Vector2(0.65f, 0.065f), SkipGuide);
            CreateButton("Guide Pause Button", canvasRect, font, "Pause",
                new Vector2(0.67f, 0.005f), new Vector2(0.95f, 0.065f), PausePractice);
        }

        private void BuildNavigationHud()
        {
            Font font = CreateGuideFont();
            if (font == null)
            {
                Debug.LogError("Practice HUD could not load a system font.", this);
                return;
            }

            hudCanvasObject = CreateWorldCanvas(
                "Practice Navigation HUD", HudWidth, HudHeight, HudCanvasScale, 19, new Vector3(0f, 1.45f, 2.35f));
            hudCanvasObject.SetActive(false);
            hudRect = hudCanvasObject.GetComponent<RectTransform>();
            CreateImage("HUD Background", hudRect, PanelColor, Vector2.zero, Vector2.one);
            hudTitleText = CreateText("HUD Title", hudRect, font, "CAMPUS EMERGENCY PRACTICE", 36, AccentColor,
                new Vector2(0.025f, 0.78f), new Vector2(0.68f, 0.96f), TextAnchor.MiddleLeft);
            hudFloorText = CreateText("HUD Floor", hudRect, font, string.Empty, 32, MainTextColor,
                new Vector2(0.025f, 0.56f), new Vector2(0.36f, 0.77f), TextAnchor.MiddleLeft);
            hudObjectiveText = CreateText("HUD Objective", hudRect, font, string.Empty, 32, MainTextColor,
                new Vector2(0.37f, 0.56f), new Vector2(0.68f, 0.77f), TextAnchor.MiddleLeft);
            hudTasksText = CreateText("HUD Tasks", hudRect, font, string.Empty, 25, SecondaryTextColor,
                new Vector2(0.025f, 0.07f), new Vector2(0.68f, 0.52f), TextAnchor.UpperLeft);
            hudTasksText.gameObject.SetActive(false);
            hudNearerButton = CreateButton("HUD Nearer Button", hudRect, font, "Nearer",
                new Vector2(0.70f, 0.78f), new Vector2(0.83f, 0.96f), MoveHudNearer);
            hudFartherButton = CreateButton("HUD Farther Button", hudRect, font, "Farther",
                new Vector2(0.85f, 0.78f), new Vector2(0.98f, 0.96f), MoveHudFarther);
            hudPauseButton = CreateButton("Pause Button", hudRect, font, "Pause",
                new Vector2(0.70f, 0.54f), new Vector2(0.98f, 0.76f), PausePractice);
            hudReviewButton = CreateButton("Review Guide Button", hudRect, font, "Review",
                new Vector2(0.70f, 0.29f), new Vector2(0.98f, 0.51f), ReviewGuide);
            hudCollapseButton = CreateButton("Collapse HUD Button", hudRect, font, "Hide tasks",
                new Vector2(0.70f, 0.04f), new Vector2(0.98f, 0.26f), ToggleHud);
            hudCollapseButton.gameObject.SetActive(false);
            hudCollapseLabel = hudCollapseButton.GetComponentInChildren<Text>();
            UpdateNavigationHud();
            UpdateHudExpandedState();
        }

        private void ToggleHud()
        {
            hudExpanded = !hudExpanded;
            UpdateHudExpandedState();
        }

        private void UpdateHudExpandedState()
        {
            if (hudRect == null)
                return;

            hudRect.sizeDelta = new Vector2(HudWidth, hudExpanded ? HudHeight : 270f);

            hudFloorText.gameObject.SetActive(hudExpanded);
            hudObjectiveText.gameObject.SetActive(hudExpanded);
            hudTasksText.gameObject.SetActive(false);
            hudTitleText.text = hudExpanded ? "CAMPUS EMERGENCY PRACTICE" : "PRACTICE TASKS";

            SetButtonAnchors(hudNearerButton,
                new Vector2(hudExpanded ? 0.70f : 0.025f, hudExpanded ? 0.78f : 0.36f),
                new Vector2(hudExpanded ? 0.83f : 0.32f, hudExpanded ? 0.96f : 0.68f));
            SetButtonAnchors(hudFartherButton,
                new Vector2(hudExpanded ? 0.85f : 0.37f, hudExpanded ? 0.78f : 0.36f),
                new Vector2(hudExpanded ? 0.98f : 0.68f, hudExpanded ? 0.96f : 0.68f));
            SetButtonAnchors(hudPauseButton, new Vector2(0.70f, hudExpanded ? 0.54f : 0.67f), new Vector2(0.98f, hudExpanded ? 0.76f : 0.94f));
            SetButtonAnchors(hudReviewButton, new Vector2(0.70f, hudExpanded ? 0.29f : 0.36f), new Vector2(0.98f, hudExpanded ? 0.51f : 0.63f));
            hudCollapseButton.gameObject.SetActive(false);
        }

        private void MoveGuideNearer()
        {
            AdjustPanelDistance(guideCanvasObject, -UiDistanceStep);
        }

        private void MoveGuideFarther()
        {
            AdjustPanelDistance(guideCanvasObject, UiDistanceStep);
        }

        private void MoveHudNearer()
        {
            AdjustPanelDistance(hudCanvasObject, -UiDistanceStep);
        }

        private void MoveHudFarther()
        {
            AdjustPanelDistance(hudCanvasObject, UiDistanceStep);
        }

        private void AdjustPanelDistance(GameObject panel, float step)
        {
            if (panel == null || !panel.activeSelf || !TryGetHeadCamera())
                return;

            Vector3 offset = Vector3.ProjectOnPlane(panel.transform.position - headCamera.transform.position, Vector3.up);
            float distance = offset.magnitude;
            float target = step < 0f
                ? Mathf.Min(distance, Mathf.Max(MinimumUiDistance, distance + step))
                : Mathf.Max(distance, Mathf.Min(MaximumUiDistance, distance + step));
            Vector3 direction = distance > 0.001f ? offset / distance :
                Vector3.ProjectOnPlane(headCamera.transform.forward, Vector3.up).normalized;
            if (direction.sqrMagnitude < 0.001f)
                direction = Vector3.ProjectOnPlane(headCamera.transform.up, Vector3.up).normalized;
            panel.transform.position += direction * (target - distance);
            UpdateDistanceButtons(guideCanvasObject, guideNearerButton, guideFartherButton);
            UpdateDistanceButtons(hudCanvasObject, hudNearerButton, hudFartherButton);
        }

        private void UpdateDistanceButtons(GameObject panel, Button nearer, Button farther)
        {
            if (panel == null || nearer == null || farther == null || headCamera == null)
                return;
            float distance = Vector3.ProjectOnPlane(panel.transform.position - headCamera.transform.position, Vector3.up).magnitude;
            nearer.interactable = distance > MinimumUiDistance + 0.001f;
            farther.interactable = distance < MaximumUiDistance - 0.001f;
        }

        private static void SetButtonAnchors(Button button, Vector2 anchorMin, Vector2 anchorMax)
        {
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
        }

        private void BuildPauseMenu()
        {
            Font font = CreateGuideFont();
            if (font == null)
            {
                Debug.LogError("Pause menu could not load a system font.", this);
                return;
            }

            pauseCanvasObject = CreateWorldCanvas(
                "Practice Pause Menu", PanelWidth * 0.72f, PanelHeight * 0.58f, CanvasScale, 30, new Vector3(0f, 1.8f, 2.32f));
            RectTransform rect = pauseCanvasObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(PanelWidth * 0.72f, PanelHeight * 0.58f);
            CreateImage("Pause Background", rect, PanelColor, Vector2.zero, Vector2.one);
            CreateText("Pause Title", rect, font, "Practice paused", 68, AccentColor,
                new Vector2(0.08f, 0.72f), new Vector2(0.92f, 0.94f), TextAnchor.MiddleCenter);
            CreateText("Pause Explanation", rect, font, "Take a break. Resume when you are ready, or restart/leave this practice.",
                36, MainTextColor, new Vector2(0.08f, 0.52f), new Vector2(0.92f, 0.72f), TextAnchor.MiddleCenter);
            CreateButton("Resume Button", rect, font, "Resume",
                new Vector2(0.08f, 0.30f), new Vector2(0.46f, 0.46f), ResumePractice);
            CreateButton("Restart Button", rect, font, "Restart",
                new Vector2(0.54f, 0.30f), new Vector2(0.92f, 0.46f), RestartPractice);
            CreateButton("Exit Button", rect, font, "Exit",
                new Vector2(0.31f, 0.08f), new Vector2(0.69f, 0.24f), ExitPractice);
            pauseCanvasObject.SetActive(false);
        }

        private void PausePractice()
        {
            if (isPaused)
                return;
            CampusAudioDirector.Instance?.PlayPopup();

            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isPaused = true;
            pauseCanvasObject.SetActive(true);
        }

        private void ResumePractice()
        {
            if (!isPaused)
                return;

            Time.timeScale = previousTimeScale;
            isPaused = false;
            pauseCanvasObject.SetActive(false);
        }

        private void RestartPractice()
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            if (!scene.IsValid() || scene.buildIndex < 0)
            {
                Debug.LogError("Cannot restart the practice because the active scene is not in Build Settings.", this);
                ResumePractice();
                return;
            }

            Time.timeScale = previousTimeScale;
            isPaused = false;
            SceneManager.LoadScene(scene.buildIndex);
        }

        private void ExitPractice()
        {
            ResumePractice();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void BeginGuide()
        {
            guideInProgress = true;
            currentStep = 0;
            if (routeProgress == RouteProgress.AwaitingStart)
            {
                if (!completedTasks[0])
                    currentTaskIndex = 0;
                currentObjective = "Try moving with the left joystick";
            }
            welcomeText.text = "Use the left joystick to move. Complete each control exercise at your own pace.";
            beginGuideButton.interactable = false;
            UpdateTutorialText("Practice is optional. You can skip or review these controls at any time.");
            UpdateNavigationHud();
        }

        private void SkipGuide()
        {
            guideInProgress = false;
            if (routeProgress == RouteProgress.AwaitingStart)
            {
                currentTaskIndex = 1;
                currentObjective = completedTasks[0]
                    ? "Continue to the DownStair corner and review the signs."
                    : "Controls skipped. Continue to the DownStair corner.";
            }
            guideCanvasObject.SetActive(false);
            hudCanvasObject.SetActive(true);
            PositionPanelInFront(hudCanvasObject, InitialHudDistance, -0.38f);
            UpdateNavigationHud();
        }

        private void ReviewGuide()
        {
            CampusAudioDirector.Instance?.PlayPopup();
            hudCanvasObject.SetActive(false);
            guideCanvasObject.SetActive(true);
            ShowWelcome();
        }

        private void ShowWelcome()
        {
            guideCanvasObject.SetActive(true);
            PositionPanelInFront(guideCanvasObject, InitialGuideDistance, -0.02f);
            CampusAudioDirector.Instance?.PlayPopup();
            guideInProgress = false;
            currentStep = 0;
            beginGuideButton.interactable = true;
            skipGuideButton.interactable = true;
            welcomeText.text = "Hello! I am your campus safety guide for this fictional practice.\n\nThis activity is for learning, not official emergency guidance. Follow real on-site instructions in an actual emergency.\n\nLeft joystick: move. Right joystick left or right: snap turn 45 degrees.";
            UpdateTutorialText("Select Begin guided practice to try each control, or skip the orientation.");
            hudCanvasObject.SetActive(false);
            UpdateNavigationHud();
        }

        public void SetPracticeObjective(string floor, string objective, int completedTaskCount)
        {
            if (string.IsNullOrWhiteSpace(floor) || string.IsNullOrWhiteSpace(objective))
            {
                Debug.LogError("Practice HUD requires a floor label and an objective.", this);
                return;
            }

            if (completedTaskCount < 0 || completedTaskCount > PracticeTasks.Length)
            {
                Debug.LogError($"Practice HUD task count must be between 0 and {PracticeTasks.Length}.", this);
                return;
            }

            currentFloor = floor;
            floorTrackingConnected = true;
            currentObjective = objective;
            for (int i = 0; i < completedTasks.Length; i++)
                completedTasks[i] = i < completedTaskCount;
            currentTaskIndex = Mathf.Min(completedTaskCount, PracticeTasks.Length - 1);
            UpdateNavigationHud();
        }

        private void UpdateNavigationHud()
        {
            string taskList = BuildTaskList();
            if (hudFloorText != null && hudObjectiveText != null)
            {
                hudFloorText.text = floorTrackingConnected
                    ? $"Current floor: {currentFloor}     Practice route: 3F to 2F"
                    : "Planned practice route: 3F to 2F";
                hudObjectiveText.text = $"Current objective: {currentObjective}";
            }
            if (hudTasksText != null)
                hudTasksText.text = taskList;

            if (introductionPending)
                return;

            if (elizabethGuideDialogue == null)
                elizabethGuideDialogue = FindElizabethStartGuideDialogue();

            if (elizabethGuideDialogue != null)
                elizabethGuideDialogue.ShowTaskGuide(taskList, evacuationDiagram);
        }

        private string BuildTaskList()
        {
            var taskLines = new System.Text.StringBuilder();
            for (int i = 0; i < PracticeTasks.Length; i++)
            {
                string marker = completedTasks[i] ? "[x]" : i == currentTaskIndex ? "[>]" : "[ ]";
                taskLines.Append(marker).Append(' ').Append(PracticeTasks[i]);
                if (i < PracticeTasks.Length - 1)
                    taskLines.Append('\n');
            }

            if (routeProgress == RouteProgress.AwaitingStart)
                taskLines.Append("\nContinue to the DownStair corner to begin route practice.");

            return taskLines.ToString();
        }

        private NpcGuideDialogue FindElizabethStartGuideDialogue()
        {
            GameObject npc = GameObject.Find(ElizabethStartObjectName);
            if (npc == null)
            {
                Debug.LogError($"Practice route could not find '{ElizabethStartObjectName}' for the task list.", this);
                return null;
            }

            NpcGuideDialogue dialogue = npc.GetComponent<NpcGuideDialogue>();
            if (dialogue == null)
                dialogue = npc.AddComponent<NpcGuideDialogue>();

            return dialogue;
        }

        private Button CreateButton(
            string objectName,
            RectTransform parent,
            Font font,
            string label,
            Vector2 anchorMin,
            Vector2 anchorMax,
            UnityEngine.Events.UnityAction onClick)
        {
            var buttonObject = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            RectTransform rect = buttonObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            Image background = buttonObject.GetComponent<Image>();
            background.color = AccentColor;
            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = background;
            button.transition = Selectable.Transition.ColorTint;
            ColorBlock colors = button.colors;
            colors.normalColor = AccentColor;
            colors.highlightedColor = new Color(0.38f, 0.86f, 0.95f, 1f);
            colors.pressedColor = new Color(0.14f, 0.57f, 0.69f, 1f);
            colors.selectedColor = colors.highlightedColor;
            colors.disabledColor = new Color(0.32f, 0.4f, 0.44f, 0.65f);
            button.colors = colors;

            CreateText("Label", rect, font, label, 26, PanelColor,
                new Vector2(0.04f, 0.03f), new Vector2(0.96f, 0.97f), TextAnchor.MiddleCenter);
            button.onClick.AddListener(onClick);
            button.onClick.AddListener(() => CampusAudioDirector.Instance?.PlayClick());
            return button;
        }

        private static Font CreateGuideFont()
        {
            return Font.CreateDynamicFontFromOSFont(
                new[] { "Arial", "Segoe UI", "Noto Sans" },
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

            Text text = textObject.GetComponent<Text>();
            text.font = font;
            text.text = content;
            text.fontSize = fontSize;
            text.color = color;
            text.alignment = alignment;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = Mathf.Max(24, fontSize - 8);
            text.resizeTextMaxSize = fontSize;
            text.raycastTarget = false;
            return text;
        }
    }
}
