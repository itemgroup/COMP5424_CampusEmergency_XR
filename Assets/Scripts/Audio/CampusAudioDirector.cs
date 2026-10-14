using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace CampusEmergency.Audio
{
    /// <summary>Scene-local audio. All clips are offline assets, including on Quest.</summary>
    [DefaultExecutionOrder(-900)]
    [DisallowMultipleComponent]
    public sealed class CampusAudioDirector : MonoBehaviour
    {
        public static CampusAudioDirector Instance { get; private set; }
        [Header("Mix (0 silences a category)")]
        [Range(0f, 1f)] public float masterVolume = 0.8f;
        [Range(0f, 1f)] public float alarmVolume = 0.32f;
        [Range(0f, 1f)] public float voiceVolume = 0.85f;
        [Range(0f, 1f)] public float effectsVolume = 0.45f;
        [Range(0f, 1f)] public float footstepVolume = 0.22f;
        [Header("SampleScene layout")]
        [SerializeField] private Transform routeStart;
        [SerializeField] private Transform liftGuide;
        [SerializeField] private Transform assemblyWaypoint;
        [SerializeField] private Vector3 buildingCenter = new Vector3(40f, 0f, 0f);
        [SerializeField] private Vector2 buildingHalfSize = new Vector2(20.45f, 20.45f);
        [SerializeField] private Vector2 courtyardHalfSize = new Vector2(9.15f, 9.15f);
        [SerializeField] private float groundFloorHeight;
        [SerializeField, Min(1f)] private float alarmFadeSeconds = 8f;
        [SerializeField, Min(1f)] private float outdoorFadeDistance = 15f;
        [SerializeField, Min(1f)] private float paRepeatSeconds = 55f;

        private AudioSource alarm, speech, effects, footsteps, ambience;
        private AudioClip alarmClip, activationClip, popupClip, correctClip, incorrectClip;
        private AudioClip teleportClip, completionClip, windClip;
        private AudioClip[] floorSteps, stairSteps;
        private VoiceCatalog catalog;
        private readonly Queue<SpeechRequest> speechQueue = new Queue<SpeechRequest>();
        private readonly List<TeleportationProvider> providers = new List<TeleportationProvider>();
        private XROrigin origin;
        private Camera head;
        private Vector3 previousFeet;
        private bool feetInitialized, emergencyActive, assemblyReached, wasNearLift;
        private float stepDistance, nextStepTime, nextPaTime, lastLiftTime = -100f;
        private float lastPopupTime = -100f, lastFeedbackTime = -100f, suppressStepsUntil;
        private float lastLiftAttempt = -100f;
        private float alarmLevel, outdoorAmount;
        private bool paused;
        private Coroutine speechRoutine;
        public bool IsEmergencyActive => emergencyActive;
        public bool HasReachedAssembly => assemblyReached;
        public float AlarmLevel => alarmLevel;

        [Serializable] private sealed class VoiceEntry { public string text; public string clip; }
        [Serializable] private sealed class VoiceCatalog { public VoiceEntry[] entries; }
        private sealed class SpeechRequest
        {
            public AudioClip clip;
            public Transform speaker;
            public bool announcement;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            alarm = Source("Alarm", true, 0f);
            speech = Source("Speech", false, 0f);
            effects = Source("Feedback", false, 0f);
            footsteps = Source("Footsteps", false, 0f);
            ambience = Source("Outdoor ambience", true, 0f);
            alarmClip = Clip("alarm_loop"); activationClip = Clip("alarm_activation");
            popupClip = Clip("popup"); correctClip = Clip("correct");
            incorrectClip = Clip("incorrect"); teleportClip = Clip("teleport");
            completionClip = Clip("completion"); windClip = Clip("outdoor_wind");
            floorSteps = new[] { Clip("step_floor_1"), Clip("step_floor_2"), Clip("step_floor_3") };
            stairSteps = new[] { Clip("step_stair_1"), Clip("step_stair_2"), Clip("step_stair_3") };
            TextAsset data = Resources.Load<TextAsset>("CampusSound/voice_catalog");
            if (data != null) catalog = JsonUtility.FromJson<VoiceCatalog>(data.text);
            else Debug.LogError("[CampusAudio] Missing voice catalog.", this);
        }

        private AudioSource Source(string sourceName, bool loop, float spatialBlend)
        {
            var obj = new GameObject(sourceName);
            obj.transform.SetParent(transform, false);
            var source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false; source.loop = loop;
            source.spatialBlend = spatialBlend;
            source.minDistance = 2f; source.maxDistance = 18f;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.dopplerLevel = 0f;
            return source;
        }

        private static AudioClip Clip(string name)
        {
            AudioClip clip = Resources.Load<AudioClip>("CampusSound/" + name);
            if (clip == null) Debug.LogError("[CampusAudio] Missing clip: " + name);
            return clip;
        }

        private void OnEnable()
        {
            foreach (var provider in FindObjectsByType<TeleportationProvider>(FindObjectsSortMode.None))
            {
                providers.Add(provider);
                provider.locomotionEnded += OnTeleportEnded;
            }
        }

        private void OnDisable()
        {
            foreach (var provider in providers)
                if (provider != null) provider.locomotionEnded -= OnTeleportEnded;
            providers.Clear();
            if (speechRoutine != null) StopCoroutine(speechRoutine);
            speechRoutine = null; speechQueue.Clear();
            foreach (var source in new[] { alarm, speech, effects, footsteps, ambience })
                if (source != null) source.Stop();
        }

        private void OnDestroy() { if (Instance == this) Instance = null; }

        private void Start()
        {
            if (assemblyWaypoint != null)
            {
                var marker = new GameObject("Assembly Point Sign");
                marker.transform.SetParent(assemblyWaypoint, false);
                marker.transform.localPosition = Vector3.up * 1.9f;
                marker.AddComponent<SoundAreaSign>().SetText("ASSEMBLY POINT\nRemain here and wait for instructions");
            }
            if (liftGuide == null) return;
            for (int floor = 0; floor < 4; floor++)
            {
                var sign = new GameObject("Lift Safety Interaction " + (floor == 0 ? "G" : floor.ToString()) + "F");
                sign.transform.SetParent(transform, false);
                Vector3 position = liftGuide.position + liftGuide.right * 1.6f;
                position.y = groundFloorHeight + floor * 4f;
                sign.transform.SetPositionAndRotation(position, liftGuide.rotation);
                sign.AddComponent<LiftSafetyAudio>();
            }
        }

        private void Update()
        {
            bool shouldPause = Time.timeScale == 0f;
            if (shouldPause != paused)
            {
                paused = shouldPause;
                foreach (var source in new[] { alarm, speech, footsteps, ambience })
                { if (paused) source.Pause(); else source.UnPause(); }
                feetInitialized = false;
            }
            if (paused) return;
            if (origin == null) origin = FindFirstObjectByType<XROrigin>();
            if (head == null) head = origin != null ? origin.Camera : Camera.main;
            if (head == null) return;
            Vector3 feet = origin != null ? origin.transform.position : head.transform.position - Vector3.up * 1.6f;
            // Room-scale head sway must not create virtual footsteps or false stair changes.
            UpdateSteps(feet);
            if (!emergencyActive && routeStart != null && NearWaypoint(feet, routeStart, 2.5f)) BeginEmergency();
            if (emergencyActive) UpdateEvacuation(feet);
            UpdateMix();
        }

        public static Vector3 WaypointPosition(Transform waypoint)
        {
            var anchor = waypoint != null ? waypoint.GetComponentInChildren<TeleportationAnchor>() : null;
            return anchor != null && anchor.teleportAnchorTransform != null
                ? anchor.teleportAnchorTransform.position : waypoint != null ? waypoint.position : Vector3.zero;
        }

        public static bool NearWaypoint(Vector3 position, Transform waypoint, float radius)
        { return waypoint != null && (position - WaypointPosition(waypoint)).sqrMagnitude <= radius * radius; }

        public void BeginEmergency()
        {
            if (emergencyActive) return;
            emergencyActive = true; assemblyReached = false;
            alarm.clip = alarmClip; alarm.volume = 0f; alarm.Play();
            OneShot(activationClip, effectsVolume);
            QueueAnnouncement("pa_emergency");
            nextPaTime = Time.time + paRepeatSeconds;
        }

        public void EndEmergency()
        {
            emergencyActive = false; alarmLevel = 0f; alarm.Stop(); ambience.Stop();
            speechQueue.Clear();
            if (speechRoutine != null) StopCoroutine(speechRoutine);
            speechRoutine = null; speech.Stop();
        }

        private void UpdateEvacuation(Vector3 feet)
        {
            bool ground = Mathf.Abs(feet.y - groundFloorHeight) < 1.5f;
            float dx = Mathf.Max(0f, Mathf.Abs(feet.x - buildingCenter.x) - buildingHalfSize.x);
            float dz = Mathf.Max(0f, Mathf.Abs(feet.z - buildingCenter.z) - buildingHalfSize.y);
            float outsideDistance = Mathf.Sqrt(dx * dx + dz * dz);
            float courtyardDepth = Mathf.Min(courtyardHalfSize.x - Mathf.Abs(feet.x - buildingCenter.x),
                courtyardHalfSize.y - Mathf.Abs(feet.z - buildingCenter.z));
            bool outside = outsideDistance > 0.1f || courtyardDepth > 0.2f;
            float depth = Mathf.Max(outsideDistance, courtyardDepth);
            outdoorAmount = ground && outside
                ? Mathf.Lerp(0.65f, 1f, Mathf.Clamp01(depth / outdoorFadeDistance)) : 0f;
            if (outdoorAmount > 0f && !ambience.isPlaying)
            { ambience.clip = windClip; ambience.Play(); }
            if (ground && !assemblyReached && NearWaypoint(feet, assemblyWaypoint, 2.5f))
            {
                assemblyReached = true;
                speechQueue.Clear(); // Do not announce indoor evacuation after reaching safety.
                PlayCompletion();
                QueueAnnouncement("pa_assembly");
                SoundNotice.Show("Assembly point", "You have reached the designated assembly point. Please remain here and wait for further instructions.");
            }
            Vector3 liftPosition = liftGuide != null ? liftGuide.position : new Vector3(51.1f, 12f, -11.8f);
            // The lift shaft repeats at 0, 4, 8 and 12 m in this building.
            bool floor = Mathf.Abs(feet.y - (groundFloorHeight + Mathf.Round((feet.y - groundFloorHeight) / 4f) * 4f)) < 1.5f;
            bool nearLift = floor && new Vector2(feet.x - liftPosition.x, feet.z - liftPosition.z).sqrMagnitude < 25f;
            if (nearLift && !wasNearLift && Time.time - lastLiftTime > 25f && !assemblyReached)
            { lastLiftTime = Time.time; QueueAnnouncement("pa_lifts"); }
            wasNearLift = nearLift;
            if (!assemblyReached && outdoorAmount == 0f && Time.time >= nextPaTime)
            { QueueAnnouncement("pa_emergency"); nextPaTime = Time.time + paRepeatSeconds; }
        }

        private void UpdateMix()
        {
            float target = emergencyActive ? Mathf.Lerp(1f, 0.12f, outdoorAmount) : 0f;
            if (assemblyReached) target = 0.06f;
            alarmLevel = Mathf.MoveTowards(alarmLevel, target, Time.deltaTime / Mathf.Max(1f, alarmFadeSeconds));
            float duck = speech.isPlaying ? 0.28f : 1f;
            alarm.volume = masterVolume * alarmVolume * alarmLevel * duck;
            speech.volume = masterVolume * voiceVolume;
            effects.volume = masterVolume;
            footsteps.volume = masterVolume * footstepVolume;
            ambience.volume = Mathf.MoveTowards(ambience.volume, masterVolume * 0.12f * outdoorAmount, Time.deltaTime * 0.03f);
        }

        private void UpdateSteps(Vector3 feet)
        {
            if (!feetInitialized) { previousFeet = feet; feetInitialized = true; return; }
            Vector3 delta = feet - previousFeet; previousFeet = feet;
            if (Time.time < suppressStepsUntil || delta.magnitude > 1.5f)
            { stepDistance = 0f; return; }
            float distance = new Vector2(delta.x, delta.z).magnitude;
            if (distance < 0.002f) return;
            if (!Physics.Raycast(feet + Vector3.up * 0.3f, Vector3.down, out RaycastHit hit, 0.7f, ~0, QueryTriggerInteraction.Ignore))
            { stepDistance = 0f; return; }
            stepDistance += distance;
            bool stairs = Mathf.Abs(delta.y) > 0.004f || hit.normal.y < 0.96f;
            if (stepDistance < (stairs ? 0.65f : 0.9f) || Time.time < nextStepTime) return;
            stepDistance = 0f; nextStepTime = Time.time + 0.22f;
            AudioClip[] clips = stairs ? stairSteps : floorSteps;
            footsteps.pitch = UnityEngine.Random.Range(0.96f, 1.04f);
            footsteps.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
        }

        private void OnTeleportEnded(LocomotionProvider provider)
        {
            OneShot(teleportClip, effectsVolume * 0.7f);
            suppressStepsUntil = Time.time + 0.35f; feetInitialized = false; stepDistance = 0f;
        }

        public void PlayPopup()
        {
            if (Time.unscaledTime - lastPopupTime < 0.15f) return;
            lastPopupTime = Time.unscaledTime; OneShot(popupClip, effectsVolume * 0.6f);
        }
        public void PlayCorrect()
        {
            if (Time.unscaledTime - lastFeedbackTime < 0.15f) return;
            lastFeedbackTime = Time.unscaledTime; OneShot(correctClip, effectsVolume);
        }
        public void PlayIncorrect() { OneShot(incorrectClip, effectsVolume); }
        public void PlayCompletion() { OneShot(completionClip, effectsVolume); }
        public void PlayClick() { OneShot(popupClip, effectsVolume * 0.35f); }
        private void OneShot(AudioClip clip, float volume)
        { if (clip != null && effects != null) { effects.volume = masterVolume; effects.PlayOneShot(clip, volume); } }

        public void WarnAgainstLift()
        {
            if (!emergencyActive || Time.unscaledTime - lastLiftAttempt < 2f) return;
            lastLiftAttempt = Time.unscaledTime;
            PlayIncorrect(); QueueAnnouncement("pa_lifts");
            SoundNotice.Show("Use the stairs", "Do not use lifts during a fire. Please proceed calmly to the designated assembly point.");
        }

        public AudioClip FindVoice(string text)
        {
            if (catalog == null || catalog.entries == null) return null;
            string key = NormalizeText(text);
            foreach (VoiceEntry entry in catalog.entries)
                if (NormalizeText(entry.text) == key) return Clip(entry.clip);
            Debug.LogWarning("[CampusAudio] No baked voice for changed NPC text; regenerate Tools/Sound voice assets: " + text);
            return null;
        }

        public static string NormalizeText(string text)
        {
            text = System.Text.RegularExpressions.Regex.Replace(text ?? "", "<[^>]*>|\\[(?:x|>| )\\]", "");
            return System.Text.RegularExpressions.Regex.Replace(text, "\\s+", " ").Trim().ToLowerInvariant();
        }

        public float SpeakNpc(AudioClip clip, Transform speaker)
        {
            float delay = speech.isPlaying && speech.clip != null ? Mathf.Max(0f, speech.clip.length - speech.time) + 0.4f : 0f;
            foreach (var request in speechQueue) delay += request.clip.length + 0.4f;
            Enqueue(clip, speaker, false);
            return delay;
        }
        private void QueueAnnouncement(string clipName) { Enqueue(Clip(clipName), null, true); }
        private void Enqueue(AudioClip clip, Transform speaker, bool announcement)
        {
            if (clip == null || !isActiveAndEnabled || speechQueue.Count >= 4) return;
            foreach (var request in speechQueue) if (request.clip == clip) return;
            if (speech.clip == clip && speech.isPlaying) return;
            speechQueue.Enqueue(new SpeechRequest { clip = clip, speaker = speaker, announcement = announcement });
            if (speechRoutine == null) speechRoutine = StartCoroutine(PlaySpeech());
        }

        private IEnumerator PlaySpeech()
        {
            // Yield before dequeueing so the Coroutine handle cannot become stale on an empty queue.
            yield return null;
            while (speechQueue.Count > 0)
            {
                SpeechRequest request = speechQueue.Dequeue();
                speech.spatialBlend = request.announcement ? 0f : 0.65f;
                if (request.speaker != null) speech.transform.position = request.speaker.position + Vector3.up * 1.6f;
                speech.clip = request.clip; speech.volume = masterVolume * voiceVolume; speech.Play();
                // Scaled time freezes both the queue and audio while the practice is paused.
                yield return new WaitForSeconds(request.clip.length + 0.4f);
            }
            speechRoutine = null;
        }
    }
}
