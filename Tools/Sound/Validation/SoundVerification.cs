#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using CampusEmergency.Audio;
using CampusEmergency.Building;
using CampusEmergency.UI;
using CampusEmergency.Emergency;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

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
        BuildRestartFixture();
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
        var reviewButton = Field<UnityEngine.UI.Button>(endpointNpc, "continueButton");
        Check(reviewButton.gameObject.activeInHierarchy && reviewButton.interactable &&
            reviewButton.GetComponentInChildren<UnityEngine.UI.Text>().text == "Review safety",
            "Completion card offers an optional controller-ray safety review button");
        float reviewVoiceTime = Field<float>(endpointNpc, "nextVoiceTime");
        var completionRect = Field<GameObject>(endpointNpc, "speechBubbleRoot").GetComponent<RectTransform>();
        float expectedBottom = Field<float>(endpointNpc, "bubbleHeight") + 0.4f -
            Field<float>(endpointNpc, "panelHeight") * Field<float>(endpointNpc, "bubbleScale") * 0.5f;
        Check(Mathf.Abs(completionRect.localPosition.y - completionRect.rect.height * completionRect.localScale.y * 0.5f -
            expectedBottom) < 0.001f, "Completion card lower edge is raised 0.4 meters above the original panel lower edge");
        reviewButton.onClick.Invoke();
        var reviewText = Field<UnityEngine.UI.Text>(endpointNpc, "uiDialogueText");
        Check(reviewText.text.Contains("designated stairs") && reviewText.text.Contains("Lifts may become unsafe") &&
            reviewText.text.Contains("If a route is closed") &&
            reviewText.text.Contains("not a real evacuation") &&
            !reviewText.text.Contains("You changed"),
            "Safety review explains stairs, lifts, closure awareness and the simulated endpoint without claiming reroute success");
        Check(reviewButton.GetComponentInChildren<UnityEngine.UI.Text>().text == "Back to results" &&
            Field<object>(endpointNpc, "hideCoroutine") == null &&
            Field<float>(endpointNpc, "nextVoiceTime") == reviewVoiceTime &&
            Field<object>(endpointGuide, "routeProgress").ToString() == "AwaitingStart",
            "Opening safety review preserves persistent display, voice cooldown and ordered route progress");
        Canvas.ForceUpdateCanvases();
        Check(Mathf.Abs(completionRect.localPosition.y - completionRect.rect.height * completionRect.localScale.y * 0.5f -
            expectedBottom) < 0.001f, "Taller safety review keeps the same raised lower edge, clear of the NPC head");
        Check(completionRect.localPosition.y + completionRect.rect.height * completionRect.localScale.y * 0.5f < 4f,
            "Raised review stays below the next floor at the End NPC's configured four-meter storey height");
        var reviewSettings = reviewText.GetGenerationSettings(reviewText.rectTransform.rect.size);
        reviewSettings.resizeTextForBestFit = false;
        reviewSettings.fontSize = reviewText.resizeTextMinSize;
        using (var reviewGenerator = new TextGenerator())
            Check(reviewGenerator.GetPreferredHeight(reviewText.text, reviewSettings) / reviewText.pixelsPerUnit <=
                reviewText.rectTransform.rect.height,
                "Safety review body fits its panel at the configured minimum readable font size");
        yield return 7.25;
        Check(Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<UnityEngine.UI.Text>(endpointNpc, "uiDialogueText").text.Contains("SAFETY REVIEW"),
            "Safety review remains visible beyond the former dialogue timeout");
        reviewButton.onClick.Invoke();
        Check(reviewText.text == endpointMessage &&
            reviewButton.GetComponentInChildren<UnityEngine.UI.Text>().text == "Review safety" &&
            Field<float>(endpointNpc, "nextVoiceTime") == reviewVoiceTime,
            "Back restores the configured completion message without replaying completion voice");
        reviewButton.onClick.Invoke();
        endpointNpc.ResetDialogue();
        Check(!Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<string>(endpointNpc, "persistentMessage") == null &&
            !reviewButton.gameObject.activeSelf,
            "Reset after safety review clears the review page and persistent message for the next experience");
        Set(endpointGuide, "endDialogueTriggered", false);
        showEndpoint.Invoke(endpointGuide, new object[] { endpointNpc.transform.position + Vector3.up * 4f });
        Check(!Field<bool>(endpointGuide, "endDialogueTriggered"),
            "End NPC proximity does not trigger completion from another floor");
        showEndpoint.Invoke(endpointGuide, new object[] { endpointNpc.transform.position });
        Check(Field<bool>(endpointGuide, "endDialogueTriggered") &&
            Field<GameObject>(endpointNpc, "speechBubbleRoot").activeSelf &&
            Field<UnityEngine.UI.Text>(endpointNpc, "uiDialogueText").text == endpointMessage &&
            reviewButton.GetComponentInChildren<UnityEngine.UI.Text>().text == "Review safety" &&
            Field<object>(endpointNpc, "hideCoroutine") == null,
            "Approaching End NPC after reset restores the completion card rather than the previous review page");
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
        var decisionCorner = new GameObject("Reroute decision corner").transform;
        decisionCorner.position = new Vector3(29.4f, 12.078f, 10.63f);
        var alternativeCorner = new GameObject("Reroute alternative corner").transform;
        alternativeCorner.position = new Vector3(29.4f, 12.078f, -10.44f);
        var stairCorner = new GameObject("Reroute stair corner").transform;
        stairCorner.position = new Vector3(48.72f, 12.078f, -10.44f);
        var exerciseObject = new GameObject("Reroute exercise verification");
        exerciseObject.SetActive(false);
        var exercise = exerciseObject.AddComponent<RouteChangeExercise>();
        Set(exercise, "decisionCorner", decisionCorner);
        Set(exercise, "alternativeCorner", alternativeCorner);
        Set(exercise, "stairCorner", stairCorner);
        exerciseObject.SetActive(true);
        var exerciseManager = Field<XRInteractionManager>(exercise, "interactionManager");
        var exerciseFilters = new List<IXRSelectFilter>();
        exerciseManager.selectFilters.GetAll(exerciseFilters);
        Check(exerciseFilters.Contains(exercise), "Reroute validation is registered on the real XR Interaction Manager");
        string blockedReason;
        Check(exercise.isActiveAndEnabled && !exercise.HasStarted &&
            !Field<GameObject>(exercise, "closure").activeSelf &&
            exercise.AllowsTeleport(decisionCorner.position, assembly.position, out blockedReason),
            "Optional reroute starts idle with no barrier or restrictions on ordinary exploration");
        Vector3 cameraBeforeSigns = origin.Camera.transform.position;
        Quaternion rotationBeforeSigns = origin.Camera.transform.rotation;
        bool signsReadable = true;
        var originalRouteSigns = Field<GameObject>(exercise, "originalSigns");
        originalRouteSigns.SetActive(true);
        foreach (Canvas routeSign in originalRouteSigns.GetComponentsInChildren<Canvas>(true))
            foreach (Vector3 side in new[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right })
            {
                origin.Camera.transform.position = routeSign.transform.position + side * 3f;
                yield return 0.1;
                var corners = new Vector3[4];
                routeSign.GetComponent<RectTransform>().GetWorldCorners(corners);
                Vector3 viewerRight = Vector3.Cross(Vector3.up,
                    routeSign.transform.position - origin.Camera.transform.position).normalized;
                float horizontalFacing = Vector3.Dot((corners[3] - corners[0]).normalized, viewerRight);
                float verticalFacing = Vector3.Dot((corners[1] - corners[0]).normalized, Vector3.up);
                signsReadable &= horizontalFacing > 0.99f && verticalFacing > 0.99f;
                if (horizontalFacing <= 0.99f || verticalFacing <= 0.99f)
                    Debug.Log($"Route sign orientation: side={side}, scale={routeSign.transform.localScale}, " +
                        $"horizontal={horizontalFacing}, vertical={verticalFacing}, rect={routeSign.GetComponent<RectTransform>().rect}");
            }
        originalRouteSigns.SetActive(false);
        origin.Camera.transform.SetPositionAndRotation(cameraBeforeSigns, rotationBeforeSigns);
        Check(signsReadable, "Original route sign text has viewer-correct left/right and up/down orientation from all four sides");
        var exerciseNpc = new GameObject("Reroute task card verification").AddComponent<NpcGuideDialogue>();
        Vector3 exerciseSpawn = new Vector3(23.8f, 12.078f, 4.2f);
        rig.transform.position = exerciseSpawn;
        exerciseNpc.ShowTaskGuide(tasks, diagram, () => exercise.BeginExercise(exerciseSpawn));
        var exerciseButton = Field<UnityEngine.UI.Button>(exerciseNpc, "continueButton");
        var taskCardRect = Field<GameObject>(exerciseNpc, "speechBubbleRoot").GetComponent<RectTransform>();
        var exerciseButtonRect = exerciseButton.GetComponent<RectTransform>();
        Check(taskCardRect.rect.height == 1200f &&
            Mathf.Abs(taskCardRect.localPosition.y - (Field<float>(exerciseNpc, "bubbleHeight") + 0.5f)) < 0.001f &&
            taskCardRect.localPosition.y + taskCardRect.rect.height * taskCardRect.localScale.y * 0.5f < 4f,
            "Start NPC task panel retains its original safe height below the next floor");
        Check(Mathf.Abs(exerciseButtonRect.anchorMin.x - 0.74f) < 0.001f &&
            Mathf.Abs(exerciseButtonRect.anchorMax.x - 0.97f) < 0.001f &&
            Mathf.Abs(exerciseButtonRect.anchorMin.y - 0.02f) < 0.001f &&
            Mathf.Abs(exerciseButtonRect.anchorMax.y - 0.12f) < 0.001f,
            "Start reroute button moves to the right panel edge, away from the NPC head");
        var taskTitleRect = Field<GameObject>(exerciseNpc, "speechBubbleRoot").transform
            .Find("PanelBackground/TitleText") as RectTransform;
        var taskMessageRect = Field<UnityEngine.UI.Text>(exerciseNpc, "uiDialogueText").rectTransform;
        var diagramFrame = Field<UnityEngine.UI.RawImage>(exerciseNpc, "evacuationImage").transform.parent as RectTransform;
        var instructionsRect = Field<UnityEngine.UI.Text>(exerciseNpc, "teleportInstructions").rectTransform;
        Check(taskTitleRect.anchorMin.y == 0.85f && taskTitleRect.anchorMax.y == 0.98f &&
            taskMessageRect.anchorMin.y == 0.32f && taskMessageRect.anchorMax.y == 0.80f &&
            diagramFrame.anchorMin.y == 0.27f && diagramFrame.anchorMax.y == 0.80f &&
            instructionsRect.anchorMin.y == 0.14f && instructionsRect.anchorMax.y == 0.30f,
            "Task card content restores its original unraised layout");
        Check(exerciseButton.gameObject.activeInHierarchy &&
            exerciseButton.GetComponentInChildren<UnityEngine.UI.Text>().text == "Start reroute" &&
            Field<UnityEngine.UI.RawImage>(exerciseNpc, "evacuationImage").transform.parent.gameObject.activeSelf,
            "Optional exercise offers Start reroute while preserving the ordinary task diagram");
        int routeChanges = 0;
        exercise.Changed += () => routeChanges++;
        exerciseButton.onClick.Invoke();
        Check(exercise.HasStarted && !exercise.RecoveryCompleted && routeChanges == 1 &&
            Field<GameObject>(exercise, "originalSigns").activeSelf &&
            !Field<GameObject>(exercise, "closure").activeSelf,
            "Starting reroute establishes the original route before any closure appears");
        Check(!exercise.BeginExercise(exerciseSpawn) && routeChanges == 1,
            "Repeated start cannot reset an active reroute exercise");
        Check(exercise.AllowsTeleport(exerciseSpawn, decisionCorner.position, out blockedReason) &&
            !exercise.AllowsTeleport(exerciseSpawn, stairCorner.position, out blockedReason) &&
            !string.IsNullOrWhiteSpace(blockedReason),
            "Exercise permits the initial corridor but explains why direct teleport to stairs is unavailable");
        exercise.ObservePlayer(alternativeCorner.position, false);
        Check(Field<object>(exercise, "phase").ToString() == "OriginalRoute",
            "Visiting the alternative corner before the closure does not count as recovery");
        exercise.ObservePlayer(decisionCorner.position, true);
        Check(Field<object>(exercise, "phase").ToString() == "OriginalRoute",
            "Pause prevents a route closure from triggering");
        exercise.ObservePlayer(decisionCorner.position + Vector3.down * 4f, false);
        Check(Field<object>(exercise, "phase").ToString() == "OriginalRoute",
            "Standing below the decision corner cannot trigger closure from another floor");
        exercise.ObservePlayer(decisionCorner.position, false);
        var routeBarrier = Field<GameObject>(exercise, "closure");
        Check(routeBarrier.activeSelf && !Field<GameObject>(exercise, "originalSigns").activeSelf &&
            Field<GameObject>(exercise, "alternativeSigns").activeSelf && routeChanges == 2,
            "Reaching the decision corner changes the previously available route to closed exactly once");
        exercise.ObservePlayer(decisionCorner.position, false);
        Check(routeChanges == 2, "Remaining at the closure does not repeat the event");
        Physics.SyncTransforms();
        Vector3 barrierCenter = routeBarrier.transform.position + Vector3.up * 1.2f;
        Check(routeBarrier.GetComponent<BoxCollider>().Raycast(
            new Ray(barrierCenter - Vector3.right * 2f, Vector3.right), out RaycastHit barrierHit, 4f) &&
            !routeBarrier.GetComponent<BoxCollider>().isTrigger &&
            routeBarrier.GetComponent<Rigidbody>() == null,
            "The visible closure has a static solid collider across the corridor, without a falling Rigidbody");
        var walkProbe = new GameObject("Barrier continuous movement verification");
        walkProbe.transform.position = new Vector3(barrierCenter.x - 2f, 12.078f, barrierCenter.z);
        var walkController = walkProbe.AddComponent<CharacterController>();
        walkController.height = 1.6f;
        walkController.center = Vector3.up * 0.8f;
        walkController.radius = 0.25f;
        Physics.SyncTransforms();
        CollisionFlags barrierCollision = walkController.Move(Vector3.right * 4f);
        Check(walkProbe.transform.position.x < barrierCenter.x &&
            (barrierCollision & CollisionFlags.Sides) != 0,
            "A real CharacterController continuous move is stopped by the closure instead of passing through");
        UnityEngine.Object.Destroy(walkProbe);
        Check(!exercise.AllowsTeleport(decisionCorner.position,
                new Vector3(50.671f, 12.078f, 10.666f), out blockedReason) &&
            !exercise.AllowsTeleport(decisionCorner.position, assembly.position, out blockedReason) &&
            !exercise.AllowsTeleport(decisionCorner.position, stairCorner.position, out blockedReason),
            "Both fixed and arbitrary destinations beyond the barrier, downstairs and across the courtyard are rejected");
        Check(!exercise.Process(null, anchor),
            "The registered selection filter rejects a real stair anchor before recovery");
        var rayObject = new GameObject("Reroute XR ray verification");
        var routeRay = rayObject.AddComponent<XRRayInteractor>();
        routeRay.lineType = XRRayInteractor.LineType.StraightLine;
        routeRay.maxRaycastDistance = 5f;
        routeRay.raycastMask = 1 << 9;
        var testAreaObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        testAreaObject.name = "Reroute arbitrary floor verification";
        testAreaObject.layer = 9;
        testAreaObject.transform.localScale = new Vector3(1f, 0.1f, 1f);
        var testArea = testAreaObject.AddComponent<TeleportationArea>();
        testAreaObject.transform.position = new Vector3(50.671f, 12.028f, 10.666f);
        rayObject.transform.SetPositionAndRotation(testAreaObject.transform.position + Vector3.up * 2f,
            Quaternion.Euler(90f, 0f, 0f));
        Physics.SyncTransforms();
        routeRay.PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase.Dynamic);
        Check(routeRay.TryGetCurrent3DRaycastHit(out RaycastHit blockedFloorHit) &&
            blockedFloorHit.collider == testAreaObject.GetComponent<Collider>() &&
            !exerciseManager.IsSelectPossible(routeRay, testArea),
            "A real XR ray hitting a free-area floor beyond the barrier cannot select it through the manager");
        Check(exercise.AllowsTeleport(new Vector3(30.5f, 12.078f, 8.5f),
            new Vector3(34f, 12.078f, 10.63f), out blockedReason) &&
            !exercise.AllowsTeleport(decisionCorner.position, new Vector3(40f, 12.078f, 0f), out blockedReason),
            "Valid corner-to-corridor teleports are allowed without treating their flight line as walking, while courtyard landings stay blocked");
        Check(exercise.AllowsTeleport(new Vector3(barrierCenter.x - 0.25f, 12.078f, decisionCorner.position.z),
                decisionCorner.position, out blockedReason) &&
            !exercise.AllowsTeleport(new Vector3(barrierCenter.x - 0.25f, 12.078f, decisionCorner.position.z),
                new Vector3(49f, 12.078f, decisionCorner.position.z), out blockedReason),
            "A player near the barrier can teleport back to safety, but not through to the other side");
        routeRay.selectActionTrigger = XRBaseInputInteractor.InputTriggerType.State;
        routeRay.selectInput.inputSourceMode = XRInputButtonReader.InputSourceMode.ManualValue;
        routeRay.selectInput.manualPerformed = true;
        routeRay.selectInput.manualValue = 1f;
        routeRay.PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase.Dynamic);
        Check(routeRay.isSelectActive, "Notification regression uses an actively held real XR select input");
        Set(exercise, "nextNoticeTime", 0f);
        exercise.Process(routeRay, testArea);
        var blockedNotice = GameObject.Find("Audio Safety Notification").GetComponent<SoundNotice>();
        object firstBlockedHide = Field<object>(blockedNotice, "hide");
        float firstBlockedTime = Field<float>(exercise, "nextNoticeTime");
        Check(Field<UnityEngine.UI.Text>(blockedNotice, "message").text.StartsWith("TELEPORT UNAVAILABLE") &&
            Field<List<IXRSelectInteractor>>(exercise, "notifiedInteractors").Count == 1,
            "An actual invalid teleport gesture reports its reason once");
        yield return 4.25;
        exercise.Process(routeRay, testArea);
        Check(Field<object>(blockedNotice, "hide") == firstBlockedHide &&
            Field<float>(exercise, "nextNoticeTime") == firstBlockedTime,
            "Holding the same invalid teleport gesture does not repeat a popup after the former four-second cooldown");
        routeRay.selectInput.manualPerformed = false;
        routeRay.selectInput.manualValue = 0f;
        routeRay.PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase.Dynamic);
        typeof(RouteChangeExercise).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(exercise, null);
        Check(Field<List<IXRSelectInteractor>>(exercise, "notifiedInteractors").Count == 0,
            "Releasing the teleport gesture rearms feedback for a new attempt");
        routeRay.selectInput.manualPerformed = true;
        routeRay.selectInput.manualValue = 1f;
        routeRay.PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase.Dynamic);
        exercise.Process(routeRay, testArea);
        Check(Field<float>(exercise, "nextNoticeTime") > firstBlockedTime,
            "A new invalid teleport gesture can report its reason again");
        routeRay.selectInput.manualPerformed = false;
        routeRay.selectInput.manualValue = 0f;
        routeRay.PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase.Dynamic);
        testAreaObject.transform.position = alternativeCorner.position - Vector3.up * 0.05f;
        rayObject.transform.position = testAreaObject.transform.position + Vector3.up * 2f;
        Physics.SyncTransforms();
        // XRRayInteractor caches its sampled line for the current frame.
        yield return 0.1;
        routeRay.PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase.Dynamic);
        Check(routeRay.TryGetCurrent3DRaycastHit(out RaycastHit allowedFloorHit) &&
            allowedFloorHit.collider == testAreaObject.GetComponent<Collider>() &&
            exerciseManager.IsSelectPossible(routeRay, testArea),
            "The same real XR ray and free-area floor remain selectable on the available alternative corridor");
        Check(exercise.AllowsTeleport(decisionCorner.position, alternativeCorner.position, out blockedReason),
            "The opposite same-floor corridor remains available for teleporting");
        exercise.ObservePlayer(alternativeCorner.position, false);
        Check(Field<object>(exercise, "phase").ToString() == "AlternativeReached" &&
            exercise.AllowsTeleport(alternativeCorner.position, stairCorner.position, out blockedReason) &&
            !exercise.RecoveryCompleted, "Alternative corner opens the connecting corridor, not an immediate success");
        exercise.ObservePlayer(stairCorner.position, false);
        Check(exercise.RecoveryCompleted && routeChanges == 4 &&
            exercise.AllowsTeleport(stairCorner.position, assembly.position, out blockedReason) &&
            !exercise.AllowsTeleport(new Vector3(33f, 12.078f, 10.63f),
                new Vector3(49f, 12.078f, 10.63f), out blockedReason),
            "Rejoining stairs restores normal destinations but keeps the closed barrier non-teleportable");
        exerciseNpc.ShowTaskGuide(tasks, diagram, null, true);
        Check(!exerciseButton.gameObject.activeSelf &&
            !Field<UnityEngine.UI.RawImage>(exerciseNpc, "evacuationImage").transform.parent.gameObject.activeSelf &&
            Field<UnityEngine.UI.Text>(exerciseNpc, "teleportInstructions").text.Contains("environment signs"),
            "Active exercise hides the answer diagram and start button, retaining environmental-sign guidance");
        var strictGuideObject = new GameObject("Strict exercise endpoint verification");
        strictGuideObject.SetActive(false);
        var strictGuide = strictGuideObject.AddComponent<ControllerGuidePanel>();
        var strictEndNpc = new GameObject("Strict exercise End NPC").AddComponent<NpcGuideDialogue>();
        strictEndNpc.transform.position = new Vector3(52.563f, 8.047f, -9.41f);
        Set(strictGuide, "routeChangeExercise", exercise);
        Set(strictGuide, "downStairFinished", strictEndNpc.transform);
        Set(strictGuide, "elizabethEndDialogue", strictEndNpc);
        showEndpoint.Invoke(strictGuide, new object[] { strictEndNpc.transform.position });
        Check(!Field<bool>(strictGuide, "endDialogueTriggered") &&
            !Field<GameObject>(strictEndNpc, "speechBubbleRoot").activeSelf,
            "Recovery alone cannot show success before the ordered stair checkpoints are complete");
        Set(strictGuide, "downStairStart", stairCorner);
        var stairLandings = new Transform[3];
        Vector3[] landingPositions = {
            new Vector3(57.048f, 12.078f, -9.41f), new Vector3(57.913f, 10.014f, -14.771f),
            new Vector3(58.172f, 8.047f, -9.232f) };
        for (int i = 0; i < stairLandings.Length; i++)
        {
            stairLandings[i] = new GameObject("Ordered stair landing " + i).transform;
            stairLandings[i].position = landingPositions[i];
        }
        Set(strictGuide, "downStairAssist", stairLandings[0]);
        Set(strictGuide, "downStairAssist1", stairLandings[1]);
        Set(strictGuide, "downStairAssist2", stairLandings[2]);
        var advanceExerciseRoute = typeof(ControllerGuidePanel).GetMethod("AdvanceRoute", BindingFlags.Instance | BindingFlags.NonPublic);
        advanceExerciseRoute.Invoke(strictGuide, new object[] { strictEndNpc.transform.position });
        Check(Field<object>(strictGuide, "routeProgress").ToString() == "AwaitingStart",
            "An out-of-order endpoint after recovery still cannot complete the stair route");
        foreach (Transform point in new[] { stairCorner, stairLandings[0], stairLandings[1], stairLandings[2] })
            advanceExerciseRoute.Invoke(strictGuide, new object[] { point.position });
        Set(strictGuide, "headCamera", origin.Camera);
        Set(strictGuide, "introductionPending", false);
        Set(strictGuide, "elizabethGuideDialogue", exerciseNpc);
        Set(strictGuide, "evacuationDiagram", diagram);
        Set(strictGuide, "nextIncompleteEndNoticeTime", 0f);
        Vector3 beforeFinalArrival = rig.transform.position;
        rig.transform.position = strictEndNpc.transform.position;
        lateUpdate.Invoke(strictGuide, null);
        rig.transform.position = beforeFinalArrival;
        Check(Field<object>(strictGuide, "routeProgress").ToString() == "Completed",
            "The original five stair checkpoints still complete in order after reroute recovery");
        Check(Field<float>(strictGuide, "nextIncompleteEndNoticeTime") == 0f,
            "The real final arrival frame advances the last checkpoint before checking success, without a false incomplete warning");
        showEndpoint.Invoke(strictGuide, new object[] { strictEndNpc.transform.position });
        Check(Field<bool>(strictGuide, "endDialogueTriggered") &&
            Field<GameObject>(strictEndNpc, "speechBubbleRoot").activeSelf,
            "Recovery plus completed stair checkpoints enables the persistent endpoint completion and review");
        var finishContinue = Field<UnityEngine.UI.Button>(strictEndNpc, "completionContinueButton");
        var finishBack = Field<UnityEngine.UI.Button>(strictEndNpc, "completionBackButton");
        Check(finishContinue.gameObject.activeInHierarchy && finishBack.gameObject.activeInHierarchy &&
            finishContinue.GetComponentInChildren<UnityEngine.UI.Text>().text == "Continue" &&
            finishBack.GetComponentInChildren<UnityEngine.UI.Text>().text == "Back",
            "The final End NPC card offers Continue and Back alongside the retained safety review");
        Vector3 beforeContinue = rig.transform.position;
        Quaternion rotationBeforeContinue = rig.transform.rotation;
        float voiceBeforeContinue = Field<float>(strictEndNpc, "nextVoiceTime");
        finishContinue.onClick.Invoke();
        Check(rig.transform.position == beforeContinue && rig.transform.rotation == rotationBeforeContinue &&
            Field<object>(strictGuide, "routeProgress").ToString() == "Completed" &&
            Field<float>(strictEndNpc, "nextVoiceTime") == voiceBeforeContinue &&
            Field<GameObject>(strictEndNpc, "speechBubbleRoot").activeSelf,
            "Continue does not move, rotate, reset progress, hide the card or replay completion voice");
        var finishReview = Field<UnityEngine.UI.Button>(strictEndNpc, "continueButton");
        finishReview.onClick.Invoke();
        Check(!finishContinue.gameObject.activeSelf && !finishBack.gameObject.activeSelf &&
            finishReview.GetComponentInChildren<UnityEngine.UI.Text>().text == "Back to results",
            "Safety review uses Back to results and hides the classroom-return controls to avoid ambiguous Back buttons");
        finishReview.onClick.Invoke();
        Check(finishContinue.gameObject.activeSelf && finishBack.gameObject.activeSelf,
            "Returning from safety review restores both finish actions");
        strictEndNpc.ResetDialogue();
        Check(!finishContinue.gameObject.activeSelf && !finishBack.gameObject.activeSelf,
            "Dialogue reset hides both completion action buttons");
        UnityEngine.Object.Destroy(strictGuideObject);
        UnityEngine.Object.Destroy(strictEndNpc.gameObject);
        foreach (Transform landing in stairLandings) UnityEngine.Object.Destroy(landing.gameObject);
        UnityEngine.Object.Destroy(exerciseNpc.gameObject);
        UnityEngine.Object.Destroy(rayObject);
        UnityEngine.Object.Destroy(testAreaObject);
        exerciseObject.SetActive(false);
        exerciseManager.selectFilters.GetAll(exerciseFilters);
        Check(!exerciseFilters.Contains(exercise), "Disabling the exercise removes its global interaction filter");
        UnityEngine.Object.Destroy(exerciseObject);
        UnityEngine.Object.Destroy(decisionCorner.gameObject);
        UnityEngine.Object.Destroy(alternativeCorner.gameObject);
        UnityEngine.Object.Destroy(stairCorner.gameObject);
        director.EndEmergency();
        UnityEngine.Object.Destroy(director.gameObject); yield return 0.25;
        Check(CampusAudioDirector.Instance == null, "Scene cleanup releases singleton and listeners");
        foreach (double delay in VerifyClassroomRestart())
            yield return delay;
    }

    private const string RestartScenePath = "Assets/RestartVerification.unity";

    private static void BuildRestartFixture()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var diagram = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/SignsAndPosters/Fire_Evacuation_Diagram.png");
        var skybox = new Material(Shader.Find("Skybox/Panoramic"));
        AssetDatabase.CreateAsset(skybox, "Assets/RestartSkybox.mat");
        var actions = ScriptableObject.CreateInstance<UnityEngine.InputSystem.InputActionAsset>();
        foreach (var entry in new[] { ("XRI Left Locomotion", "Move"), ("XRI Right Locomotion", "Snap Turn"),
            ("XRI Right Locomotion", "Teleport Mode Cancel"), ("XRI Right Interaction", "Activate") })
        {
            var map = actions.FindActionMap(entry.Item1) ?? actions.AddActionMap(entry.Item1);
            map.AddAction(entry.Item2);
        }
        AssetDatabase.CreateAsset(actions, "Assets/RestartActions.asset");
        foreach (var action in actions)
        {
            var reference = UnityEngine.InputSystem.InputActionReference.Create(action);
            reference.name = action.name;
            AssetDatabase.AddObjectToAsset(reference, actions);
        }
        var rig = new GameObject("Restart XR Origin");
        var origin = rig.AddComponent<XROrigin>();
        var cameraObject = new GameObject("Main Camera");
        cameraObject.tag = "MainCamera";
        cameraObject.transform.SetParent(rig.transform, false);
        cameraObject.transform.localPosition = Vector3.up * 1.6f;
        origin.Camera = cameraObject.AddComponent<Camera>();
        cameraObject.AddComponent<AudioListener>();
        origin.CameraFloorOffsetObject = cameraObject;
        var mediator = rig.AddComponent<LocomotionMediator>();
        mediator.xrOrigin = origin;
        rig.AddComponent<TeleportationProvider>().mediator = mediator;
        var actionManager = rig.AddComponent<UnityEngine.XR.Interaction.Toolkit.Inputs.InputActionManager>();
        actionManager.actionAssets = new List<UnityEngine.InputSystem.InputActionAsset> { actions };
        rig.transform.position = new Vector3(23.8f, 12.078f, 4.2f);
        var interactionManager = new GameObject("XR Interaction Manager");
        interactionManager.AddComponent<XRInteractionManager>();
        new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem),
            typeof(UnityEngine.XR.Interaction.Toolkit.UI.XRUIInputModule));
        var spawn = new GameObject("Classroom Spawn").transform;
        spawn.position = rig.transform.position;
        var startNpc = new GameObject("Restart Start NPC").AddComponent<NpcGuideDialogue>();
        startNpc.transform.position = spawn.position + Vector3.forward * 3f;
        var endNpc = new GameObject("Restart End NPC").AddComponent<NpcGuideDialogue>();
        endNpc.transform.position = new Vector3(52.563f, 8.047f, -9.41f);
        Set(endNpc, "dialogueMessage", "You have completed the experience for Scene 1\u2014congratulations.");
        var positions = new[] { new Vector3(29.4f, 12.078f, 10.63f), new Vector3(29.4f, 12.078f, -10.44f),
            new Vector3(48.72f, 12.078f, -10.44f), new Vector3(57.048f, 12.078f, -9.41f),
            new Vector3(57.913f, 10.014f, -14.771f), new Vector3(58.172f, 8.047f, -9.232f),
            endNpc.transform.position };
        var waypoints = new List<Transform>();
        foreach (Vector3 position in positions)
        {
            var point = new GameObject("Restart Waypoint");
            point.transform.position = position;
            point.AddComponent<BoxCollider>();
            point.AddComponent<TeleportationAnchor>().teleportAnchorTransform = point.transform;
            waypoints.Add(point.transform);
        }
        var guideObject = new GameObject("Restart Guide");
        var exercise = guideObject.AddComponent<RouteChangeExercise>();
        Set(exercise, "decisionCorner", waypoints[0]);
        Set(exercise, "alternativeCorner", waypoints[1]);
        Set(exercise, "stairCorner", waypoints[2]);
        var guide = guideObject.AddComponent<ControllerGuidePanel>();
        Set(guide, "spawnAnchor", spawn);
        Set(guide, "headCamera", origin.Camera);
        Set(guide, "elizabethGuideDialogue", startNpc);
        Set(guide, "elizabethEndDialogue", endNpc);
        Set(guide, "evacuationDiagram", diagram);
        Set(guide, "routeChangeExercise", exercise);
        string[] routeFields = { "downStairStart", "downStairAssist", "downStairAssist1", "downStairAssist2", "downStairFinished" };
        for (int i = 0; i < routeFields.Length; i++) Set(guide, routeFields[i], waypoints[i + 2]);
        var sign = new GameObject("Restart Lift Sign").transform;
        sign.position = new Vector3(51f, 13.5f, -17f);
        var audio = new GameObject("Restart Audio").AddComponent<CampusAudioDirector>();
        Set(audio, "liftSigns", new[] { sign });
        var prop = GameObject.CreatePrimitive(PrimitiveType.Cube);
        prop.name = "Restart Prop";
        prop.transform.position = new Vector3(25f, 12.5f, 4f);
        var menuObject = new GameObject("Restart Main Menu", typeof(RectTransform), typeof(Canvas));
        var menu = menuObject.AddComponent<MainMenuController>();
        Set(menu, "background", diagram);
        Set(menu, "surroundings", skybox);
        Set(menu, "backgroundMusic", Resources.LoadAll<AudioClip>("CampusSound").First());
        Set(menu, "keepActiveRoots", new[] { interactionManager });
        EditorSceneManager.SaveScene(scene, RestartScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(RestartScenePath, true) };
        AssetDatabase.SaveAssets();
    }

    private static IEnumerable<double> VerifyClassroomRestart()
    {
        // The fixture has no controller prefab to hold/load its InputActionReference sub-assets.
        var fixtureActions = AssetDatabase.LoadAllAssetsAtPath("Assets/RestartActions.asset")
            .OfType<InputActionReference>().ToArray();
        UnityEngine.SceneManagement.SceneManager.LoadScene(RestartScenePath);
        yield return 1.5;
        var menu = UnityEngine.Object.FindFirstObjectByType<MainMenuController>();
        Check(menu != null && menu.GetComponent<Canvas>().enabled &&
            Field<AudioSource>(menu, "musicSource").isPlaying,
            "Ordinary scene entry still displays Enter System and plays menu music");
        menu.EnterSystem();
        yield return 0.25;
        var manager = new GameObject("Persistent restart state").AddComponent<CampusEmergency.Core.GameManager>();
        for (int attempt = 0; attempt < 2; attempt++)
        {
            var guide = UnityEngine.Object.FindFirstObjectByType<ControllerGuidePanel>();
            var exercise = UnityEngine.Object.FindFirstObjectByType<RouteChangeExercise>();
            var startNpc = Field<NpcGuideDialogue>(guide, "elizabethGuideDialogue");
            var endNpc = Field<NpcGuideDialogue>(guide, "elizabethEndDialogue");
            var origin = UnityEngine.Object.FindFirstObjectByType<XROrigin>();
            var prop = GameObject.Find("Restart Prop");
            Check(Field<InputAction>(guide, "moveAction") != null && Field<InputAction>(guide, "turnAction") != null &&
                Field<InputAction>(guide, "activateAction") != null && Field<InputAction>(guide, "teleportCancelAction") != null,
                "The reloaded classroom reconnects all four tutorial input actions");
            Field<UnityEngine.UI.Button>(startNpc, "continueButton").onClick.Invoke();
            Check(exercise.BeginExercise(origin.transform.position), "A fresh reroute can begin in clean attempt " + attempt);
            exercise.ObservePlayer(Field<Transform>(exercise, "decisionCorner").position, false);
            prop.transform.position += Vector3.right * 10f;
            Field<GameObject>(guide, "guideCanvasObject").transform.position += Vector3.forward * 7f;
            manager.ChangeState(CampusEmergency.Core.GameState.Emergency);
            var completed = typeof(ControllerGuidePanel).GetField("routeProgress", BindingFlags.Instance | BindingFlags.NonPublic);
            completed.SetValue(guide, Enum.Parse(completed.FieldType, "Completed"));
            Array.Fill(Field<bool[]>(guide, "completedTasks"), true);
            var phase = typeof(RouteChangeExercise).GetField("phase", BindingFlags.Instance | BindingFlags.NonPublic);
            phase.SetValue(exercise, Enum.Parse(phase.FieldType, "Rejoined"));
            origin.transform.position = endNpc.transform.position;
            typeof(ControllerGuidePanel).GetMethod("TryShowEndDialogue", BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(guide, new object[] { endNpc.transform.position });
            var oldBarrier = Field<GameObject>(exercise, "closure");
            var oldAudio = CampusAudioDirector.Instance;
            var oldNotice = GameObject.Find("Audio Safety Notification");
            Check(oldBarrier.activeSelf && oldAudio.IsEmergencyActive && oldNotice != null,
                "Restart fixture contains an active barrier, fire audio, popup and moved prop before Back");
            Field<UnityEngine.UI.Button>(endNpc, "completionBackButton").onClick.Invoke();
            yield return 1.5;
            Check(guide == null && exercise == null && endNpc == null && origin == null &&
                prop == null && oldBarrier == null && oldAudio == null && oldNotice == null,
                "Back unloads the previous experience including runtime objects, old XR rig and moved props");
            var freshGuide = UnityEngine.Object.FindFirstObjectByType<ControllerGuidePanel>();
            var freshExercise = UnityEngine.Object.FindFirstObjectByType<RouteChangeExercise>();
            var freshMenu = UnityEngine.Object.FindFirstObjectByType<MainMenuController>(FindObjectsInactive.Include);
            var freshStart = Field<NpcGuideDialogue>(freshGuide, "elizabethGuideDialogue");
            var freshOrigin = UnityEngine.Object.FindFirstObjectByType<XROrigin>();
            Check(!freshMenu.gameObject.activeSelf && !freshMenu.GetComponent<Canvas>().enabled &&
                Field<AudioSource>(freshMenu, "musicSource") == null &&
                Field<UnityEngine.UI.Text>(freshStart, "uiDialogueText").text.StartsWith("Welcome to the Requirements") &&
                Field<bool>(freshGuide, "introductionPending"),
                "Back skips Enter System and its music, opening the fresh classroom welcome instead");
            Check(Vector3.Distance(freshOrigin.transform.position, Field<Transform>(freshGuide, "spawnAnchor").position) < 0.01f &&
                Vector3.Distance(GameObject.Find("Restart Prop").transform.position, new Vector3(25f, 12.5f, 4f)) < 0.01f,
                "Reload restores the classroom spawn and original prop placement");
            Check(!freshExercise.HasStarted && !Field<GameObject>(freshExercise, "closure").activeSelf &&
                Field<object>(freshGuide, "routeProgress").ToString() == "AwaitingStart" &&
                !Field<bool[]>(freshGuide, "completedTasks").Any(value => value) &&
                !Field<bool>(freshGuide, "endDialogueTriggered") &&
                !Field<GameObject>(Field<NpcGuideDialogue>(freshGuide, "elizabethEndDialogue"), "speechBubbleRoot").activeSelf,
                "Reload clears closure, task checkmarks, endpoint success and prior completion UI");
            Check(!CampusAudioDirector.Instance.IsEmergencyActive &&
                manager.CurrentState == CampusEmergency.Core.GameState.Boot && Time.timeScale == 1f &&
                UnityEngine.Object.FindFirstObjectByType<XRInteractionManager>().selectFilters.count == 1,
                "Reload resets fire/global state and time while registering exactly one new route filter");
        }
        var finalGuide = UnityEngine.Object.FindFirstObjectByType<ControllerGuidePanel>();
        typeof(ControllerGuidePanel).GetMethod("RestartPractice", BindingFlags.Instance | BindingFlags.NonPublic)
            .Invoke(finalGuide, null);
        yield return 1.5;
        Check(UnityEngine.Object.FindFirstObjectByType<MainMenuController>()?.GetComponent<Canvas>().enabled == true,
            "Classroom skip is consumed once: the normal Restart action still opens Enter System");
        UnityEngine.Object.FindFirstObjectByType<MainMenuController>().EnterSystem();
        yield return 0.25;
        GC.KeepAlive(fixtureActions);
        UnityEngine.Object.Destroy(manager.gameObject);
    }
}
#endif
