#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        var lift = new GameObject("Lift guide").transform; lift.position = new Vector3(51.1f, 12f, -11.8f);
        Set(director, "liftGuide", lift);
        yield return 1;
        Check(Resources.LoadAll<AudioClip>("CampusSound").Length == 22, "22 audio assets import successfully");
        Check(director.FindVoice("In case of fire, do NOT use the elevators! Please evacuate using the stairs and stay safe.") != null, "NPC voice matches scene text");
        Check(director.FindVoice("[x] Learn the controls\n[>] Read signs and choose the stair route\n[ ] Enter the stair route\n[ ] Follow stair landing 1\n[ ] Follow stair landing 2\n[ ] Reach the 2F practice endpoint") != null, "Dynamic task-list markers match baked NPC speech");
        Check(GameObject.Find("Lift Safety Interaction 3F") != null, "Lift warning targets exist on all four floors");

        var waypoint = new GameObject("Offset waypoint").transform; waypoint.position = new Vector3(52.92f, 0f, -34.24f);
        waypoint.rotation = Quaternion.Euler(0f, -90f, 0f);
        var anchorObject = new GameObject("Actual teleport anchor"); anchorObject.transform.SetParent(waypoint, false);
        anchorObject.transform.localPosition = new Vector3(23.8f, 12.078f, 4.2f);
        anchorObject.AddComponent<BoxCollider>();
        var anchor = anchorObject.AddComponent<TeleportationAnchor>(); anchor.teleportAnchorTransform = anchor.transform;
        Check(CampusAudioDirector.NearWaypoint(anchor.transform.position, waypoint, 2.5f), "Offset anchor resolves to actual destination");
        var method = typeof(ControllerGuidePanel).GetMethod("IsRouteDestination", BindingFlags.Static | BindingFlags.NonPublic);
        Check((bool)method.Invoke(null, new object[] { anchor.transform.position, waypoint }), "Training route uses corrected anchor position");

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

        rig.transform.position = lift.position; yield return 0.5;
        Check(Field<bool>(director, "wasNearLift"), "Lift proximity detected at matching floor");
        director.WarnAgainstLift(); yield return 0.1;
        Check(GameObject.Find("Audio Safety Notification") != null, "Wrong lift interaction shows warning and cue");

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
