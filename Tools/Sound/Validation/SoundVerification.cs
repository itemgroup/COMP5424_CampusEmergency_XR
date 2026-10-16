#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using CampusEmergency.Audio;
using CampusEmergency.Building;
using CampusEmergency.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

// Copy into Assets/Editor of an isolated validation project. Never upgrades the original project.
public static class SoundVerification
{
    private const string Key = "CampusSoundVerification";
    private static IEnumerator<double> steps;
    private static double resumeAt;
    private static readonly List<string> passes = new List<string>();
    [InitializeOnLoadMethod]
    private static void Initialize()
    { EditorApplication.playModeStateChanged += OnPlayMode; }
    public static void Run()
    {
        SessionState.SetBool(Key, true);
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        EditorApplication.isPlaying = true;
    }
    private static void OnPlayMode(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.EnteredPlayMode || !SessionState.GetBool(Key, false)) return;
        steps = Verify().GetEnumerator(); resumeAt = 0;
        EditorApplication.update += Tick;
    }
    private static void Tick()
    {
        if (EditorApplication.timeSinceStartup < resumeAt) return;
        try
        {
            if (!steps.MoveNext()) { Finish(true, ""); return; }
            resumeAt = EditorApplication.timeSinceStartup + steps.Current;
        }
        catch (Exception e) { Finish(false, e.ToString()); }
    }
    private static void Finish(bool success, string error)
    {
        EditorApplication.update -= Tick; SessionState.SetBool(Key, false);
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "sound-verification.txt");
        File.WriteAllText(path, (success ? "PASS\n" : "FAIL\n") + string.Join("\n", passes) + "\n" + error);
        Debug.Log("SOUND_VERIFICATION " + (success ? "PASS" : "FAIL") + " " + error);
        EditorApplication.Exit(success ? 0 : 1);
    }
    private static void Check(bool condition, string name)
    { if (!condition) throw new Exception(name); passes.Add(name); Debug.Log("SOUND_CHECK PASS: " + name); }
    private static T Field<T>(object instance, string name)
    { return (T)instance.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance); }
    private static void Set(object instance, string name, object value)
    { instance.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, value); }
    private static IEnumerable<double> Verify()
    {
        new GameObject("XR Interaction Manager").AddComponent<XRInteractionManager>();
        var rig = new GameObject("Verification XR Origin"); rig.SetActive(false);
        var origin = rig.AddComponent<XROrigin>();
        var cameraObject = new GameObject("Main Camera"); cameraObject.tag = "MainCamera";
        cameraObject.transform.SetParent(rig.transform); cameraObject.transform.localPosition = Vector3.up * 1.6f;
        origin.Camera = cameraObject.AddComponent<Camera>(); cameraObject.AddComponent<AudioListener>();
        origin.CameraFloorOffsetObject = cameraObject;
        var mediator = rig.AddComponent<LocomotionMediator>(); mediator.xrOrigin = origin;
        var provider = rig.AddComponent<TeleportationProvider>(); provider.mediator = mediator;
        rig.transform.position = new Vector3(40f, 12f, 0f); rig.SetActive(true);
        var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.position = new Vector3(40f, 11.5f, 0f); floor.transform.localScale = new Vector3(30f, 1f, 30f);
        var assembly = new GameObject("Assembly waypoint").transform;
        assembly.position = new Vector3(40f, 0.048f, 0f);
        var director = new GameObject("Campus Audio Director").AddComponent<CampusAudioDirector>();
        Set(director, "assemblyWaypoint", assembly);
        var liftSigns = new Transform[8];
        for (int floorIndex = 0; floorIndex < 4; floorIndex++)
            for (int liftIndex = 0; liftIndex < 2; liftIndex++)
            {
                int index = floorIndex * 2 + liftIndex;
                liftSigns[index] = new GameObject("LiftSign " + index).transform;
                liftSigns[index].position = new Vector3(51f + liftIndex * 3.2f, floorIndex * 4f + 1.5f, -17.19f);
            }
        Set(director, "liftSigns", liftSigns);
        yield return 1;
        Check(Resources.LoadAll<AudioClip>("CampusSound").Length == 22, "22 audio assets import successfully");
        Check(director.FindVoice("In case of fire, do NOT use the elevators! Please evacuate using the stairs and stay safe.") != null, "NPC voice matches scene text");
        Check(director.FindVoice("[x] Learn the controls\n[>] Read signs and choose the stair route\n[ ] Enter the stair route\n[ ] Follow stair landing 1\n[ ] Follow stair landing 2\n[ ] Reach the 2F practice endpoint") != null, "Dynamic task-list markers match baked NPC speech");
        var liftInteractions = UnityEngine.Object.FindObjectsByType<LiftSafetyAudio>(FindObjectsSortMode.None);
        Check(liftInteractions.Length == 8 &&
            liftSigns.All(marker => liftInteractions.Any(target => Field<Transform>(target, "liftMarker") == marker &&
                Vector3.Distance(target.transform.position, marker.position + new Vector3(0f, 0.5f, 0.5f)) < 0.001f)),
            "All eight lift interaction panels use the actual sign positions on four floors");
        Check(liftInteractions.All(target => !target.GetComponent<Collider>().enabled &&
            !Field<GameObject>(target, "label").activeSelf),
            "Lift panels and colliders are hidden when the player is away");
        Check(director.FindNearbyLiftSign(new Vector3(51.1f, 13.6f, -11.8f)) == null &&
            director.FindNearbyLiftSign(new Vector3(50.17f, 9.6f, -10.39f)) == null,
            "Elevator NPC and End NPC locations do not count as lift sign proximity");
        foreach (Transform marker in liftSigns)
            Check(director.FindNearbyLiftSign(marker.position + Vector3.forward) == marker,
                "Actual lift marker is detected: " + marker.name);
        Check(!CampusAudioDirector.IsNearLiftSign(liftSigns[0].position + Vector3.up * 4f, liftSigns[0]) &&
            !CampusAudioDirector.IsNearLiftSign(liftSigns[0].position + Vector3.forward * 2.51f, liftSigns[0]),
            "Lift detection rejects other floors and positions outside 2.5 meters");

        var waypoint = new GameObject("Offset waypoint").transform; waypoint.position = new Vector3(52.92f, 0f, -34.24f);
        waypoint.rotation = Quaternion.Euler(0f, -90f, 0f);
        var anchorObject = new GameObject("Actual teleport anchor"); anchorObject.transform.SetParent(waypoint, false);
        anchorObject.transform.localPosition = new Vector3(23.8f, 12.078f, 4.2f);
        anchorObject.AddComponent<BoxCollider>();
        var anchor = anchorObject.AddComponent<TeleportationAnchor>(); anchor.teleportAnchorTransform = anchor.transform;
        Check(CampusAudioDirector.NearWaypoint(anchor.transform.position, waypoint, 2.5f), "Offset anchor resolves to actual destination");
        var method = typeof(ControllerGuidePanel).GetMethod("IsRouteDestination", BindingFlags.Static | BindingFlags.NonPublic);
        Check((bool)method.Invoke(null, new object[] { anchor.transform.position, waypoint }), "Training route uses corrected anchor position");

        var endpointGuideObject = new GameObject("Endpoint guide verification");
        endpointGuideObject.SetActive(false);
        var endpointGuide = endpointGuideObject.AddComponent<ControllerGuidePanel>();
        var startNpc = new GameObject("Start NPC introduction verification").AddComponent<NpcGuideDialogue>();
        var diagram = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/SignsAndPosters/Fire_Evacuation_Diagram.png");
        Check(diagram != null, "Evacuation diagram imports as Texture2D");
        Set(endpointGuide, "spawnAnchor", endpointGuideObject.transform);
        Set(endpointGuide, "elizabethGuideDialogue", startNpc);
        Set(endpointGuide, "evacuationDiagram", diagram);
        foreach (string builder in new[] { "BuildPanel", "BuildNavigationHud", "BuildPauseMenu" })
            typeof(ControllerGuidePanel).GetMethod(builder, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(endpointGuide, null);
        var guideCanvas = Field<GameObject>(endpointGuide, "guideCanvasObject");
        guideCanvas.SetActive(false);
        var introduction = (System.Collections.IEnumerator)typeof(ControllerGuidePanel)
            .GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(endpointGuide, null);
        introduction.MoveNext();
        introduction.MoveNext();
        string welcome = "Welcome to the Requirements and Prototype Proposal Demonstration System!";
        Check(Field<UnityEngine.UI.Text>(startNpc, "uiDialogueText").text == welcome &&
            !guideCanvas.activeSelf, "Start NPC welcome precedes the optional controller tutorial");
        typeof(ControllerGuidePanel).GetMethod("UpdateNavigationHud", BindingFlags.Instance | BindingFlags.NonPublic)
            .Invoke(endpointGuide, null);
        Check(Field<UnityEngine.UI.Text>(startNpc, "uiDialogueText").text == welcome,
            "Task updates do not overwrite welcome before Continue");
        var continueButton = Field<UnityEngine.UI.Button>(startNpc, "continueButton");
        Check(continueButton.gameObject.activeSelf && continueButton.interactable &&
            Field<GameObject>(startNpc, "speechBubbleRoot").GetComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>() != null,
            "Continue is an enabled XR-raycastable button");
        continueButton.onClick.Invoke();
        Check(!Field<bool>(endpointGuide, "introductionPending") && guideCanvas.activeSelf &&
            !continueButton.gameObject.activeSelf,
            "Continue reveals controller tutorial and dismisses welcome button");
        var diagramImage = Field<UnityEngine.UI.RawImage>(startNpc, "evacuationImage");
        Check(diagramImage.texture == diagram && diagramImage.transform.parent.gameObject.activeSelf &&
            diagramImage.GetComponent<UnityEngine.UI.AspectRatioFitter>().aspectRatio == (float)diagram.width / diagram.height,
            "Task card includes the correct evacuation diagram with preserved aspect ratio");
        string tasksAtEntry = Field<UnityEngine.UI.Text>(startNpc, "uiDialogueText").text;
        Check(tasksAtEntry.Contains("Learn the controls") && tasksAtEntry.Contains("Reach the 2F practice endpoint") &&
            Field<UnityEngine.UI.Text>(startNpc, "teleportInstructions").text.Contains("Use the marked teleport points"),
            "Task list and teleport-effect instructions appear together");
        startNpc.ApplyBubbleTransformAndStyles();
        continueButton.onClick.Invoke();
        Check(Field<GameObject>(startNpc, "speechBubbleRoot").GetComponent<RectTransform>().sizeDelta == new Vector2(1700f, 1050f) &&
            Field<UnityEngine.UI.Text>(startNpc, "uiDialogueText").text == tasksAtEntry,
            "Repeated Continue and bubble styling preserve the task card");
        var uiLateUpdate = typeof(ControllerGuidePanel).GetMethod("LateUpdate", BindingFlags.Instance | BindingFlags.NonPublic);
        var guideNearer = Field<UnityEngine.UI.Button>(endpointGuide, "guideNearerButton");
        var guideFarther = Field<UnityEngine.UI.Button>(endpointGuide, "guideFartherButton");
        Vector3 guidePosition = guideCanvas.transform.position;
        Quaternion guideRotation = guideCanvas.transform.rotation;
        guideNearer.onClick.Invoke();
        Check(Mathf.Abs(Vector3.Distance(guidePosition, guideCanvas.transform.position) - 0.2f) < 0.001f &&
            Mathf.Abs(guideCanvas.transform.position.y - guidePosition.y) < 0.001f &&
            Quaternion.Angle(guideCanvas.transform.rotation, guideRotation) < 0.001f,
            "Guide Nearer physically moves its panel 0.2 meters without changing height or orientation");
        guideFarther.onClick.Invoke();
        Check(Vector3.Distance(guidePosition, guideCanvas.transform.position) < 0.001f,
            "Guide Farther physically reverses the Nearer adjustment");
        for (int i = 0; i < 15; i++) guideNearer.onClick.Invoke();
        Check(!guideNearer.interactable && Mathf.Abs(Vector3.ProjectOnPlane(
            guideCanvas.transform.position - origin.Camera.transform.position, Vector3.up).magnitude - 0.65f) < 0.001f,
            "Guide Nearer stops at 0.65 meters and disables its button");
        for (int i = 0; i < 15; i++) guideFarther.onClick.Invoke();
        Check(!guideFarther.interactable && Mathf.Abs(Vector3.ProjectOnPlane(
            guideCanvas.transform.position - origin.Camera.transform.position, Vector3.up).magnitude - 2.2f) < 0.001f,
            "Guide Farther stops at 2.2 meters and disables its button");
        guidePosition = guideCanvas.transform.position;
        Vector3 uiRigPosition = rig.transform.position;
        Quaternion uiCameraRotation = origin.Camera.transform.rotation;
        rig.transform.position += new Vector3(2f, 4f, 3f);
        origin.Camera.transform.rotation = Quaternion.Euler(0f, 80f, 0f);
        uiLateUpdate.Invoke(endpointGuide, null);
        Check(Vector3.Distance(guidePosition, guideCanvas.transform.position) < 0.001f &&
            Quaternion.Angle(guideRotation, guideCanvas.transform.rotation) < 0.001f,
            "Guide remains world-fixed after head rotation and player translation");
        rig.transform.position = uiRigPosition;
        origin.Camera.transform.rotation = uiCameraRotation;
        Field<UnityEngine.UI.Button>(endpointGuide, "beginGuideButton").onClick.Invoke();
        var completeStep = typeof(ControllerGuidePanel).GetMethod("CompleteStep", BindingFlags.Instance | BindingFlags.NonPublic);
        for (int i = 0; i < 5; i++) completeStep.Invoke(endpointGuide, new object[] { "Control verified" });
        var navigationHud = Field<GameObject>(endpointGuide, "hudCanvasObject");
        Check(!guideCanvas.activeSelf && navigationHud.activeSelf,
            "Completing all five controller exercises opens the navigation panel");
        Vector3 hudPosition = navigationHud.transform.position;
        Quaternion hudRotation = navigationHud.transform.rotation;
        rig.transform.position += new Vector3(2f, 4f, 3f);
        origin.Camera.transform.rotation = Quaternion.Euler(0f, 125f, 0f);
        uiLateUpdate.Invoke(endpointGuide, null);
        Check(Vector3.Distance(hudPosition, navigationHud.transform.position) < 0.001f &&
            Quaternion.Angle(hudRotation, navigationHud.transform.rotation) < 0.001f,
            "Post-tutorial navigation panel does not follow head rotation, walking or teleport displacement");
        Field<UnityEngine.UI.Button>(endpointGuide, "hudNearerButton").onClick.Invoke();
        Check(Mathf.Abs(Vector3.Distance(hudPosition, navigationHud.transform.position) - 0.2f) < 0.001f,
            "Nearer adjusts a distant fixed HUD by one step instead of snapping it to the player");
        rig.transform.position = uiRigPosition;
        origin.Camera.transform.rotation = uiCameraRotation;
        var hudNearer = Field<UnityEngine.UI.Button>(endpointGuide, "hudNearerButton");
        var hudFarther = Field<UnityEngine.UI.Button>(endpointGuide, "hudFartherButton");
        hudPosition = navigationHud.transform.position;
        hudNearer.onClick.Invoke();
        Check(Mathf.Abs(Vector3.Distance(hudPosition, navigationHud.transform.position) - 0.2f) < 0.001f &&
            Mathf.Abs(navigationHud.transform.position.y - hudPosition.y) < 0.001f &&
            Quaternion.Angle(hudRotation, navigationHud.transform.rotation) < 0.001f,
            "HUD Nearer physically moves its panel while preserving height and orientation");
        hudFarther.onClick.Invoke();
        Check(Vector3.Distance(hudPosition, navigationHud.transform.position) < 0.001f,
            "HUD Farther physically reverses the Nearer adjustment");
        for (int i = 0; i < 15; i++) hudNearer.onClick.Invoke();
        Check(!hudNearer.interactable && Mathf.Abs(Vector3.ProjectOnPlane(
            navigationHud.transform.position - origin.Camera.transform.position, Vector3.up).magnitude - 0.65f) < 0.001f,
            "HUD Nearer clamps to 0.65 meters");
        for (int i = 0; i < 15; i++) hudFarther.onClick.Invoke();
        Check(!hudFarther.interactable && Mathf.Abs(Vector3.ProjectOnPlane(
            navigationHud.transform.position - origin.Camera.transform.position, Vector3.up).magnitude - 2.2f) < 0.001f,
            "HUD Farther clamps to 2.2 meters");
        Field<UnityEngine.UI.Button>(endpointGuide, "hudReviewButton").onClick.Invoke();
        Check(guideCanvas.activeSelf && !navigationHud.activeSelf &&
            Mathf.Abs(Vector3.ProjectOnPlane(guideCanvas.transform.position -
                origin.Camera.transform.position, Vector3.up).magnitude - 1.4f) < 0.001f,
            "Review explicitly reopens the fixed guide in front of the current viewer");
        Field<UnityEngine.UI.Button>(endpointGuide, "skipGuideButton").onClick.Invoke();
        hudPosition = navigationHud.transform.position;
        rig.transform.position += Vector3.right;
        uiLateUpdate.Invoke(endpointGuide, null);
        Check(navigationHud.activeSelf && Vector3.Distance(hudPosition, navigationHud.transform.position) < 0.001f,
            "Skipping the guide also opens a world-fixed navigation panel");
        rig.transform.position = uiRigPosition;
        UnityEngine.Object.Destroy(startNpc.gameObject);
        var endpointNpc = new GameObject("Endpoint NPC verification").AddComponent<NpcGuideDialogue>();
        string endpointMessage = "You have completed the experience for Scene 1\u2014congratulations.";
        Set(endpointNpc, "dialogueMessage", endpointMessage);
        Set(endpointGuide, "downStairFinished", waypoint);
        Set(endpointGuide, "elizabethEndDialogue", endpointNpc);
        var showEndpoint = typeof(ControllerGuidePanel).GetMethod("TryShowEndDialogue", BindingFlags.Instance | BindingFlags.NonPublic);
        Vector3 arrival = anchor.transform.position;
        endpointNpc.transform.position = arrival + Vector3.right * 6.5f;
        showEndpoint.Invoke(endpointGuide, new object[] { arrival + Vector3.right * 2.51f });
        Check(!Field<bool>(endpointGuide, "endDialogueTriggered"), "Endpoint dialogue does not trigger outside actual anchor radius");
        showEndpoint.Invoke(endpointGuide, new object[] { new Vector3(arrival.x, arrival.y + 4f, arrival.z) });
        Check(!Field<bool>(endpointGuide, "endDialogueTriggered"), "Endpoint dialogue does not trigger from another floor");
        endpointNpc.enabled = false;
        showEndpoint.Invoke(endpointGuide, new object[] { arrival });
        Check(!Field<bool>(endpointGuide, "endDialogueTriggered"), "Inactive endpoint NPC does not consume arrival trigger");
        endpointNpc.enabled = true;
        showEndpoint.Invoke(endpointGuide, new object[] { arrival + Vector3.right * 2.49f });
        Check(Field<bool>(endpointGuide, "endDialogueTriggered") &&
            Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<UnityEngine.UI.Text>(endpointNpc, "uiDialogueText").text == endpointMessage,
            "Direct endpoint arrival shows configured End NPC text without prior route checkpoints");
        Check(Field<object>(endpointGuide, "routeProgress").ToString() == "AwaitingStart",
            "Endpoint dialogue leaves ordered task progress unchanged");
        float completionVoiceTime = Field<float>(endpointNpc, "nextVoiceTime");
        showEndpoint.Invoke(endpointGuide, new object[] { arrival });
        Check(Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<float>(endpointNpc, "nextVoiceTime") == completionVoiceTime &&
            Field<object>(endpointNpc, "hideCoroutine") == null,
            "Repeated endpoint arrival preserves the persistent card without replaying dialogue");
        yield return 7.25;
        Check(Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<UnityEngine.UI.Text>(endpointNpc, "uiDialogueText").text == endpointMessage,
            "Completion card remains visible beyond its former seven-second timeout");
        endpointNpc.ResetDialogue();
        Set(endpointGuide, "endDialogueTriggered", false);
        showEndpoint.Invoke(endpointGuide, new object[] { endpointNpc.transform.position + Vector3.up * 4f });
        Check(!Field<bool>(endpointGuide, "endDialogueTriggered"),
            "End NPC proximity does not trigger completion from another floor");
        showEndpoint.Invoke(endpointGuide, new object[] { endpointNpc.transform.position });
        Check(Field<bool>(endpointGuide, "endDialogueTriggered") &&
            Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<object>(endpointNpc, "hideCoroutine") == null,
            "Approaching End NPC away from the teleport anchor shows the persistent completion card");
        var lateUpdate = typeof(ControllerGuidePanel).GetMethod("LateUpdate", BindingFlags.Instance | BindingFlags.NonPublic);
        Set(endpointGuide, "headCamera", origin.Camera);
        Set(endpointGuide, "introductionPending", true);
        guideCanvas.SetActive(false);
        Vector3 previousRigPosition = rig.transform.position;
        endpointNpc.ResetDialogue();
        Set(endpointGuide, "endDialogueTriggered", false);
        Set(endpointGuide, "isPaused", true);
        rig.transform.position = arrival;
        lateUpdate.Invoke(endpointGuide, null);
        Check(!Field<bool>(endpointGuide, "endDialogueTriggered"),
            "Actual frame update still blocks endpoint completion while paused");
        Set(endpointGuide, "isPaused", false);
        foreach (Vector3 targetPosition in new[] { arrival, endpointNpc.transform.position })
        {
            endpointNpc.ResetDialogue();
            Set(endpointGuide, "endDialogueTriggered", false);
            rig.transform.position = targetPosition;
            lateUpdate.Invoke(endpointGuide, null);
            Check(Field<bool>(endpointGuide, "endDialogueTriggered") &&
                Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
                Field<UnityEngine.UI.Text>(endpointNpc, "uiDialogueText").text == endpointMessage &&
                Field<object>(endpointNpc, "hideCoroutine") == null,
                "Actual frame update shows permanent completion before Continue at " + targetPosition);
        }
        AudioClip completionVoice = director.FindVoice(endpointMessage);
        bool completionVoiceRequested = completionVoice != null &&
            Field<AudioSource>(director, "speech").clip == completionVoice;
        foreach (object item in (System.Collections.IEnumerable)Field<object>(director, "speechQueue"))
            if ((AudioClip)item.GetType().GetField("clip").GetValue(item) == completionVoice && completionVoice != null)
                completionVoiceRequested = true;
        Check(completionVoiceRequested, "Completion before Continue plays or queues the configured completion voice");
        Check(Field<bool>(endpointGuide, "introductionPending") && !guideCanvas.activeSelf &&
            Field<object>(endpointGuide, "routeProgress").ToString() == "AwaitingStart",
            "Completion does not bypass Continue for the tutorial or advance ordered route progress");
        rig.transform.position = previousRigPosition;
        UnityEngine.Object.Destroy(endpointGuideObject);
        UnityEngine.Object.Destroy(endpointNpc.gameObject);
        director.EndEmergency();
        yield return 0.1;

        director.BeginEmergency(); director.BeginEmergency();
        Check(director.IsEmergencyActive, "Emergency starts idempotently");
        yield return 9;
        Check(director.AlarmLevel > 0.95f, "Indoor alarm fades in to configured level");
        var speech = Field<AudioSource>(director, "speech");
        Check(speech.clip != null && speech.clip.name == "pa_emergency", "Exact emergency PA is selected");
        Check(Field<AudioSource>(director, "alarm").volume < director.masterVolume * director.alarmVolume * 0.4f, "Speech ducks alarm for intelligibility");

        string tasks = "[x] Learn the controls\n[>] Read signs and choose the stair route\n[ ] Enter the stair route\n[ ] Follow stair landing 1\n[ ] Follow stair landing 2\n[ ] Reach the 2F practice endpoint";
        var npc = new GameObject("Verification NPC").AddComponent<NpcGuideDialogue>();
        npc.ShowPersistentDialogue("EVACUATION TASK LIST", tasks);
        npc.TriggerDialogue();
        Check(Field<UnityEngine.UI.Text>(npc, "uiDialogueText").text == tasks, "Triggered NPC preserves currently displayed task-list text");
        bool taskVoiceQueued = false;
        foreach (object item in (System.Collections.IEnumerable)Field<object>(director, "speechQueue"))
            if (((AudioClip)item.GetType().GetField("clip").GetValue(item)).name == "npc_tasks_route") taskVoiceQueued = true;
        Check(taskVoiceQueued, "Triggered NPC queues the voice for the visible text");

        // A requested teleport has a real delay. No arrival sound is allowed before it completes.
        provider.delayTime = 1f;
        float before = Field<float>(director, "suppressStepsUntil");
        bool ended = false; provider.locomotionEnded += _ => ended = true;
        Check(provider.QueueTeleportRequest(new TeleportRequest { destinationPosition = new Vector3(42f, 12f, 0f), destinationRotation = Quaternion.identity, matchOrientation = MatchOrientation.None }), "XRI accepts real teleport request");
        yield return 0.25;
        Check(!ended && Field<float>(director, "suppressStepsUntil") == before, "No teleport sound on request/aim/cancel phase");
        yield return 1.25;
        Check(ended && rig.transform.position.x > 41f, "Real XRI teleport completes and moves player");
        Check(Field<float>(director, "suppressStepsUntil") > before, "Arrival audio is triggered by completed teleport");
        yield return 0.5;
        rig.transform.position += Vector3.right * 0.5f; yield return 0.1;
        rig.transform.position += Vector3.right * 0.5f; yield return 0.1;
        Check(Field<float>(director, "nextStepTime") > 0f, "Grounded continuous movement triggers footsteps");

        rig.transform.position = new Vector3(51.1f, 12f, -11.8f); yield return 0.2;
        director.WarnAgainstLift();
        Check(!Field<bool>(director, "wasNearLift") && GameObject.Find("Audio Safety Notification") == null,
            "NPC proximity cannot show the lift warning even when explicitly requested");
        Transform lift = liftSigns[6];
        rig.transform.position = lift.position - Vector3.up * 1.5f + Vector3.forward; yield return 0.5;
        Check(Field<bool>(director, "wasNearLift"), "Lift proximity detected at matching floor");
        Check(liftInteractions.Any(target => Field<Transform>(target, "liftMarker") == lift &&
            target.GetComponent<Collider>().enabled && Field<GameObject>(target, "label").activeSelf),
            "Nearby lift marker enables its panel and XR collider");
        director.WarnAgainstLift(); yield return 0.1;
        GameObject notice = GameObject.Find("Audio Safety Notification");
        Check(notice != null && Vector3.Distance(notice.transform.position, lift.position + new Vector3(0f, 0.5f, 0.5f)) < 0.001f,
            "Wrong lift warning is anchored at the matching lift marker, not the player or NPC");
        rig.transform.position = new Vector3(40f, 12f, 0f); yield return 0.1;
        Check(liftInteractions.All(target => !target.GetComponent<Collider>().enabled &&
            !Field<GameObject>(target, "label").activeSelf), "Moving away hides lift panels and disables raycast targets");

        float level = director.AlarmLevel;
        Time.timeScale = 0f; yield return 0.3;
        Check(Mathf.Abs(director.AlarmLevel - level) < 0.02f, "Pause freezes alarm fades");
        Time.timeScale = 1f; yield return 0.3;

        rig.transform.position = new Vector3(29.585f, 0.048f, 10.468f); yield return 0.5;
        Check(Field<float>(director, "outdoorAmount") == 0f, "G-floor covered corridor does not count as outdoors");
        rig.transform.position = new Vector3(40f, 0.048f, 6f); yield return 9;
        Check(director.AlarmLevel < 0.45f && !director.HasReachedAssembly, "G-floor open courtyard fades alarm without false completion");
        rig.transform.position = assembly.position; yield return 0.5;
        Check(director.HasReachedAssembly, "Assembly point triggers success once");
        yield return 2;
        Check(director.AlarmLevel <= 0.07f, "Assembly retains only distant alarm");
        director.EndEmergency();
        Check(!director.IsEmergencyActive && director.AlarmLevel == 0f, "EndEmergency clears alarm and speech queue");
        UnityEngine.Object.Destroy(director.gameObject); yield return 0.25;
        Check(CampusAudioDirector.Instance == null, "Scene cleanup releases singleton and listeners");
    }
}
#endif
