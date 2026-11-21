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
using CampusEmergency.Emergency;

namespace CampusEmergency.UI
{
    [DisallowMultipleComponent]
    public sealed class ControllerGuidePanel : MonoBehaviour
    {
        [SerializeField] private Transform spawnAnchor;
        [SerializeField] private Camera headCamera;
        [SerializeField] private Transform[] downStairCheckpoints;
        [SerializeField] private Transform teleportEnd;
        private Transform teleportEndTarget;
        [SerializeField] private Transform groundAssemblyPoint;
        private bool routeSelected;
        public bool CanCompleteAssembly => routeSelected && !introductionPending &&
            routeProgress == RouteProgress.Completed;
        private readonly List<UnityEngine.XR.Interaction.Toolkit.Locomotion.LocomotionProvider> pausedLocomotion =
            new List<UnityEngine.XR.Interaction.Toolkit.Locomotion.LocomotionProvider>();
        [SerializeField] private NpcGuideDialogue elizabethGuideDialogue;
        [SerializeField] private NpcGuideDialogue elizabethEndDialogue;
        [SerializeField] private Texture2D evacuationDiagram;
        [SerializeField] private RouteChangeExercise routeChangeExercise;

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
        private string[] PracticeTasks = System.Array.Empty<string>();

        private enum RouteProgress
        {
            AwaitingStart,
            FollowingStairs,
            ReachedGround,
            Completed
        }

        private readonly List<BaseTeleportationInteractable> teleportTargets = new List<BaseTeleportationInteractable>();
        private bool[] completedTasks;
        private int nextStairCheckpoint;

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
        private float nextIncompleteEndNoticeTime;

        private void Awake()
        {
            if (spawnAnchor == null)
            {
                Debug.LogError("Controller guide requires the classroom spawn anchor.", this);
                enabled = false;
                return;
            }

            if (elizabethGuideDialogue != null) elizabethGuideDialogue.AutomaticDialogue = false;
            if (elizabethEndDialogue != null) elizabethEndDialogue.AutomaticDialogue = false;
            FindTutorialActions();
            ResolveRouteCheckpoints();
            ValidateRoutePoints();
            if (!enabled) return;
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
            if (routeChangeExercise != null)
                routeChangeExercise.Changed += OnRouteExerciseChanged;
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
            elizabethGuideDialogue.ResetDialogue();
            ShowWelcome();
        }

        private void OnDestroy()
        {
            if (routeChangeExercise != null)
                routeChangeExercise.Changed -= OnRouteExerciseChanged;
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
            var origin = headCamera.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>();
            Vector3 feet = origin != null ? origin.transform.position : cameraTransform.position - Vector3.up * 1.6f;
            if (routeChangeExercise != null)
                routeChangeExercise.ObservePlayer(feet, isPaused);
            if (!isPaused)
            {
                if (!introductionPending && routeSelected)
                    AdvanceRoute(feet);
                TryShowEndDialogue(feet);
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

        private void ResolveRouteCheckpoints()
        {
            if (downStairCheckpoints == null || downStairCheckpoints.Length == 0)
            {
                var checkpoints = new List<Transform>();
                foreach (Transform candidate in FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (candidate.gameObject.scene == gameObject.scene &&
                        candidate.name.StartsWith("Teleport Corner DownStair Assist"))
                        checkpoints.Add(candidate);
                }
                checkpoints.Sort((first, second) =>
                {
                    Vector3 firstPosition = CampusAudioDirector.WaypointPosition(first);
                    Vector3 secondPosition = CampusAudioDirector.WaypointPosition(second);
                    return Mathf.Abs(firstPosition.y - secondPosition.y) > 0.1f
                        ? secondPosition.y.CompareTo(firstPosition.y)
                        : secondPosition.x.CompareTo(firstPosition.x);
                });
                downStairCheckpoints = checkpoints.ToArray();
            }

            if (teleportEnd == null)
            {
                foreach (Transform candidate in FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (candidate.gameObject.scene == gameObject.scene &&
                        candidate.name.Replace(" ", "").Equals("TeleportEnd", System.StringComparison.OrdinalIgnoreCase))
                    {
                        teleportEnd = candidate;
                        break;
                    }
                }
            }
            ResolveTeleportEndTarget();
            PracticeTasks = new string[downStairCheckpoints.Length + 2];
            PracticeTasks[0] = "Learn the controls";
            for (int checkpoint = 0; checkpoint < downStairCheckpoints.Length; checkpoint++)
                PracticeTasks[checkpoint + 1] = "Reach " + (downStairCheckpoints[checkpoint] != null
                    ? downStairCheckpoints[checkpoint].name : "missing stair checkpoint");
            PracticeTasks[PracticeTasks.Length - 1] = "Stand on Teleport End in the G floor courtyard";
            completedTasks = new bool[PracticeTasks.Length];
        }

        private void ResolveTeleportEndTarget()
        {
            teleportEndTarget = null;
            if (teleportEnd == null || groundAssemblyPoint == null) return;
            TeleportationAnchor[] anchors = teleportEnd.GetComponentsInChildren<TeleportationAnchor>();
            if (anchors.Length == 0 && IsInGroundCourtyard(teleportEnd.position))
                teleportEndTarget = teleportEnd;
            float closestDistance = float.PositiveInfinity;
            foreach (TeleportationAnchor anchor in anchors)
            {
                Transform target = anchor.teleportAnchorTransform;
                if (!anchor.isActiveAndEnabled || target == null || !IsInGroundCourtyard(target.position)) continue;
                Vector3 offset = target.position - groundAssemblyPoint.position;
                if (offset.sqrMagnitude >= closestDistance)
                    continue;
                closestDistance = offset.sqrMagnitude;
                teleportEndTarget = target;
            }
        }

        private bool IsInGroundCourtyard(Vector3 position)
        {
            if (groundAssemblyPoint == null) return false;
            Vector3 offset = position - groundAssemblyPoint.position;
            return Mathf.Abs(offset.y) <= 0.45f && Mathf.Abs(offset.x) <= 9.15f && Mathf.Abs(offset.z) <= 9.15f;
        }

        private void ValidateRoutePoints()
        {
            if (downStairCheckpoints == null || downStairCheckpoints.Length == 0 ||
                System.Array.Exists(downStairCheckpoints, checkpoint => checkpoint == null))
            {
                Debug.LogError("Practice route requires the DownStair Assist checkpoints.", this);
                enabled = false;
            }

            if (groundAssemblyPoint == null)
            {
                Debug.LogError("Practice requires the G floor assembly point.", this);
                enabled = false;
            }
            if (teleportEndTarget == null)
            {
                Debug.LogError("Both routes require an active Teleport End landing point in the G floor courtyard. Check its scene position and TeleportationAnchor target.", this);
                enabled = false;
            }
            if (elizabethEndDialogue == null)
                Debug.LogError("Practice endpoint requires the End NPC dialogue reference.", this);
        }

        private void TryShowEndDialogue(Vector3 playerPosition)
        {
            if (introductionPending || !routeSelected || endDialogueTriggered || groundAssemblyPoint == null || elizabethEndDialogue == null ||
                !elizabethEndDialogue.isActiveAndEnabled)
                return;

            if (!IsAtTeleportEnd(playerPosition))
                return;
            if (routeProgress != RouteProgress.Completed)
            {
                if (Time.unscaledTime >= nextIncompleteEndNoticeTime)
                {
                    nextIncompleteEndNoticeTime = Time.unscaledTime + 10f;
                    SoundNotice.Show("PRACTICE NOT COMPLETE", (routeChangeExercise == null || !routeChangeExercise.HasStarted || routeChangeExercise.RecoveryCompleted)
                        ? nextStairCheckpoint < downStairCheckpoints.Length
                            ? "Missing checkpoint: " + downStairCheckpoints[nextStairCheckpoint].name + ". Reach it before returning to Teleport End."
                            : "Stand on the Teleport End landing point to finish."
                        : routeChangeExercise.Objective);
                }
                return;
            }

            hudCanvasObject.SetActive(false);
            guideCanvasObject.SetActive(false);
            SoundNotice.HideCurrent();
            elizabethGuideDialogue?.ResetDialogue();
            endDialogueTriggered = true;
            elizabethEndDialogue.ShowCompletionDialogue(ReturnToSpawn, ExitPractice);
        }

        private void ReturnToSpawn()
        {
            if (isPaused || Time.timeScale == 0f)
            {
                SoundNotice.Show("PRACTICE PAUSED", "Resume the practice before returning to the classroom.");
                return;
            }
            foreach (MainMenuController menu in FindObjectsByType<MainMenuController>(
                FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (menu.gameObject.scene == gameObject.scene)
                {
                    menu.RestartAtClassroom();
                    return;
                }
            }
            Debug.LogError("A clean classroom restart requires the scene's MainMenuController.", this);
            SoundNotice.Show("RESTART UNAVAILABLE", "The classroom restart is not configured in this scene.");
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
            if (!isPaused && guideInProgress && currentStep == 0 && context.ReadValue<Vector2>().sqrMagnitude > 0.09f)
                CompleteStep("Movement detected. Push the right joystick left or right to turn.");
        }

        private void OnTurn(InputAction.CallbackContext context)
        {
            if (!isPaused && guideInProgress && currentStep == 1 && context.ReadValue<Vector2>().sqrMagnitude > 0.09f)
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

            if (routeChangeExercise != null)
                routeChangeExercise.ObservePlayer(destination, isPaused);
            if (!isPaused)
                AdvanceRoute(destination);
            if (!isPaused && guideInProgress && currentStep == 2)
                CompleteStep("Teleport successful. Push the right joystick up to aim again, then press the right grip to cancel.");
        }

        public UnityEvent onTeleportCornerReached;

        private void AdvanceRoute(Vector3 destination)
        {
            if (introductionPending || !routeSelected || isPaused || routeProgress == RouteProgress.Completed) return;
            if (routeChangeExercise != null && routeChangeExercise.HasStarted && !routeChangeExercise.RecoveryCompleted)
                return;
            if (downStairCheckpoints == null || downStairCheckpoints.Length == 0 || teleportEnd == null || groundAssemblyPoint == null)
                return;

            if (nextStairCheckpoint < downStairCheckpoints.Length)
            {
                Transform checkpoint = downStairCheckpoints[nextStairCheckpoint];
                if (!IsRouteDestination(destination, checkpoint)) return;
                if (nextStairCheckpoint == 0)
                {
                    CampusAudioDirector.Instance?.BeginEmergency();
                    onTeleportCornerReached?.Invoke();
                }
                completedTasks[nextStairCheckpoint + 1] = true;
                nextStairCheckpoint++;
                routeProgress = nextStairCheckpoint == downStairCheckpoints.Length
                    ? RouteProgress.ReachedGround : RouteProgress.FollowingStairs;
                currentTaskIndex = nextStairCheckpoint + 1;
                floorTrackingConnected = true;
                float height = CampusAudioDirector.WaypointPosition(checkpoint).y - groundAssemblyPoint.position.y;
                int floor = Mathf.Clamp(Mathf.CeilToInt((height - 0.1f) / 4f), 0, 3);
                currentFloor = floor == 0 ? "G" : floor + "F";
                currentObjective = nextStairCheckpoint < downStairCheckpoints.Length
                    ? "Follow the stairs to " + downStairCheckpoints[nextStairCheckpoint].name + "."
                    : "All stair checkpoints reached. Stand on Teleport End in the G floor courtyard to finish.";
                CampusAudioDirector.Instance?.PlayCorrect();
                UpdateNavigationHud();
                return;
            }

            if (!IsAtTeleportEnd(destination)) return;
            routeProgress = RouteProgress.Completed;
            completedTasks[completedTasks.Length - 1] = true;
            currentTaskIndex = completedTasks.Length - 1;
            currentFloor = "G";
            currentObjective = "You reached Teleport End. Your practice is complete.";
            CampusAudioDirector.Instance?.CompleteAssembly();
            UpdateNavigationHud();
        }

        private bool IsAtTeleportEnd(Vector3 position)
        {
            if (teleportEndTarget == null) return false;
            Vector3 offset = position - teleportEndTarget.position;
            return Mathf.Abs(offset.y) <= 0.45f && offset.x * offset.x + offset.z * offset.z <= 0.8f * 0.8f;
        }

        private void BeginRouteExercise()
        {
            if (routeChangeExercise == null || isPaused || introductionPending ||
                routeProgress != RouteProgress.AwaitingStart || !TryGetHeadCamera())
                return;
            var origin = headCamera.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>();
            Vector3 feet = origin != null ? origin.transform.position : headCamera.transform.position - Vector3.up * 1.6f;
            if (routeChangeExercise.BeginExercise(feet))
            {
                SelectDirectRoute();
                endDialogueTriggered = false;
                elizabethEndDialogue?.ResetDialogue();
            }
        }

        private void SelectDirectRoute()
        {
            routeSelected = true;
            currentTaskIndex = 1;
            currentObjective = routeChangeExercise != null && routeChangeExercise.HasStarted
                ? routeChangeExercise.Objective : "Follow the corridor to the first DownStair Assist, then follow the stair checkpoints to G.";
            elizabethGuideDialogue?.ResetDialogue();
            hudCanvasObject.SetActive(true);
            PositionPanelInFront(hudCanvasObject, InitialHudDistance, -0.38f);
            UpdateNavigationHud();
        }

        private void OnRouteExerciseChanged()
        {
            currentFloor = "3F";
            floorTrackingConnected = true;
            currentTaskIndex = 1;
            currentObjective = routeChangeExercise.Objective;
            UpdateNavigationHud();
        }

        private static bool IsRouteDestination(Vector3 destination, Transform waypoint)
        {
            return CampusAudioDirector.NearWaypoint(destination, waypoint, RoutePointTolerance);
        }

        private void OnTeleportCancel(InputAction.CallbackContext context)
        {
            if (!isPaused && guideInProgress && currentStep == 3)
                CompleteStep("Cancellation detected. Point the ray at an interactive object and press the right trigger.");
        }

        private void OnActivate(InputAction.CallbackContext context)
        {
            if (isPaused || !guideInProgress || currentStep != 4) return;
            foreach (var ray in FindObjectsByType<UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor>(FindObjectsSortMode.None))
            {
                if (ray.TryGetCurrent3DRaycastHit(out RaycastHit hit) &&
                    hit.collider.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>() != null)
                {
                    CompleteStep("Interaction confirmed. Choose your route to G.");
                    return;
                }
            }
            UpdateTutorialText("Aim at an interactive object, then press the trigger. Or select Continue to routes.");
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
                    currentObjective = routeChangeExercise != null && routeChangeExercise.HasStarted
                        ? routeChangeExercise.Objective : "Controller practice complete. Continue to the DownStair corner.";
                }
                SkipGuide();
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
            CreateText("Subtitle", canvasRect, font, "Welcome to the Campus Emergency Practice · 3F to G", 36, SecondaryTextColor,
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
            skipGuideButton = CreateButton("Skip Guide Button", canvasRect, font, "Continue to routes",
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
            foreach (var provider in FindObjectsByType<UnityEngine.XR.Interaction.Toolkit.Locomotion.LocomotionProvider>(FindObjectsSortMode.None))
            {
                if (!provider.enabled) continue;
                pausedLocomotion.Add(provider);
                provider.enabled = false;
            }
            guideCanvasObject.SetActive(false);
            hudCanvasObject.SetActive(false);
            elizabethGuideDialogue?.ResetDialogue();
            pauseCanvasObject.SetActive(true);
        }

        private void ResumePractice()
        {
            if (!isPaused)
                return;

            Time.timeScale = previousTimeScale;
            isPaused = false;
            foreach (var provider in pausedLocomotion)
                if (provider != null) provider.enabled = true;
            pausedLocomotion.Clear();
            pauseCanvasObject.SetActive(false);
            guideCanvasObject.SetActive(introductionPending);
            hudCanvasObject.SetActive(!introductionPending && routeSelected);
            UpdateNavigationHud();
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
            introductionPending = false;
            guideInProgress = false;
            if (routeProgress == RouteProgress.AwaitingStart)
            {
                currentTaskIndex = 1;
                currentObjective = routeChangeExercise != null && routeChangeExercise.HasStarted
                    ? routeChangeExercise.Objective : completedTasks[0]
                    ? "Continue to the DownStair corner and review the signs."
                    : "Controls skipped. Continue to the DownStair corner.";
            }
            guideCanvasObject.SetActive(false);
            hudCanvasObject.SetActive(routeSelected);
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
            introductionPending = true;
            elizabethGuideDialogue?.ResetDialogue();
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
                    ? $"Current floor: {currentFloor}     Practice route: 3F to G"
                    : "Planned practice route: 3F to G";
                hudObjectiveText.text = $"Current objective: {currentObjective}";
            }
            if (hudTasksText != null)
                hudTasksText.text = taskList;

            if (introductionPending || routeSelected)
                return;

            if (elizabethGuideDialogue == null)
                elizabethGuideDialogue = FindElizabethStartGuideDialogue();

            if (elizabethGuideDialogue != null)
            {
                bool exerciseActive = routeChangeExercise != null && routeChangeExercise.HasStarted;
                bool canStartExercise = routeChangeExercise != null && routeChangeExercise.isActiveAndEnabled &&
                    !exerciseActive && routeProgress == RouteProgress.AwaitingStart;
                elizabethGuideDialogue.ShowTaskGuide(taskList, evacuationDiagram,
                    canStartExercise ? BeginRouteExercise : (UnityAction)null, exerciseActive, SelectDirectRoute);
            }
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
                taskLines.Append(routeChangeExercise != null && routeChangeExercise.HasStarted
                    ? "\nReroute exercise: " + routeChangeExercise.Objective
                    : "\nReach the first DownStair Assist to begin the stair checkpoints.");

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
            background.color = Color.white;
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
            return Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
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
