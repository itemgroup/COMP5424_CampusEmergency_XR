using System;
using System.Collections.Generic;
using CampusEmergency.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace CampusEmergency.Emergency
{
    [DisallowMultipleComponent]
    public sealed class RouteChangeExercise : MonoBehaviour, IXRSelectFilter
    {
        [SerializeField] private Transform decisionCorner;
        [SerializeField] private Transform alternativeCorner;
        [SerializeField] private Transform stairCorner;
        private enum Phase { Idle, OriginalRoute, Closed, AlternativeReached, Rejoined }
        private Phase phase;
        private XRInteractionManager interactionManager;
        private Vector3 decision;
        private Vector3 alternative;
        private Vector3 stairs;
        private Vector3 playerPosition;
        private GameObject originalSigns;
        private GameObject closure;
        private GameObject alternativeSigns;
        private Material barrierMaterial;
        private Material exitMaterial;
        private Mesh arrowMesh;
        private bool paused;
        private float nextNoticeTime;
        private readonly List<IXRSelectInteractor> notifiedInteractors = new List<IXRSelectInteractor>();

        private const float FloorTolerance = 0.45f;
        private const float CornerRadius = 2.5f;
        private float BarrierX => (decision.x + stairs.x) * 0.5f;
        public bool HasStarted => phase != Phase.Idle;
        public bool RecoveryCompleted => phase == Phase.Rejoined;
        public bool canProcess => isActiveAndEnabled && HasStarted;
        public event Action Changed;
        public string Objective => phase == Phase.OriginalRoute
            ? "Follow the original route to the marked 3F decision corner."
            : phase == Phase.Closed
                ? "The route ahead is closed. Check EXIT signs for an available route."
                : phase == Phase.AlternativeReached
                    ? "Alternative corridor reached. Follow its EXIT signs towards the stairs."
                    : "Alternative route rejoins the stairs. Continue through the stair checkpoints to 2F.";

        private void Awake()
        {
            interactionManager = FindFirstObjectByType<XRInteractionManager>(FindObjectsInactive.Include);
            if (decisionCorner == null || alternativeCorner == null || stairCorner == null || interactionManager == null)
            {
                Debug.LogError("Route exercise requires three corridor waypoints and an XR Interaction Manager.", this);
                enabled = false;
                return;
            }
            decision = CampusAudioDirector.WaypointPosition(decisionCorner);
            alternative = CampusAudioDirector.WaypointPosition(alternativeCorner);
            stairs = CampusAudioDirector.WaypointPosition(stairCorner);
            if (Mathf.Abs(decision.y - alternative.y) > FloorTolerance ||
                Mathf.Abs(decision.y - stairs.y) > FloorTolerance ||
                stairs.x - decision.x < 10f || decision.z - alternative.z < 10f)
            {
                Debug.LogError("Route exercise waypoints must describe the same-floor west/north/south corridor layout.", this);
                enabled = false;
                return;
            }
            BuildSignsAndBarrier();
        }

        private void OnEnable()
        {
            if (interactionManager != null)
                interactionManager.selectFilters.Add(this);
        }

        private void OnDisable()
        {
            notifiedInteractors.Clear();
            if (interactionManager != null)
                interactionManager.selectFilters.Remove(this);
        }

        private void Update()
        {
            for (int i = notifiedInteractors.Count - 1; i >= 0; i--)
            {
                IXRSelectInteractor interactor = notifiedInteractors[i];
                if ((interactor is UnityEngine.Object component && component == null) || !interactor.isSelectActive)
                    notifiedInteractors.RemoveAt(i);
            }
        }

        public bool BeginExercise(Vector3 feet)
        {
            if (!isActiveAndEnabled || HasStarted)
                return false;
            if (Mathf.Abs(feet.y - decision.y) > FloorTolerance || !InWestCorridor(feet))
            {
                SoundNotice.Show("START AT THE CLASSROOM", "Return to the 3F classroom-side corridor to start this optional exercise.");
                return false;
            }
            playerPosition = feet;
            phase = Phase.OriginalRoute;
            nextNoticeTime = Time.unscaledTime + 4f;
            originalSigns.SetActive(true);
            Changed?.Invoke();
            SoundNotice.Show("ORIGINAL ROUTE", "Follow the marked classroom-side corner, then the corridor towards the stairs. Check signs as you move.");
            return true;
        }

        public void ObservePlayer(Vector3 feet, bool isPaused)
        {
            paused = isPaused;
            if (paused || !HasStarted)
                return;
            playerPosition = feet;
            if (phase == Phase.OriginalRoute &&
                ((feet - decision).sqrMagnitude <= CornerRadius * CornerRadius ||
                 (Mathf.Abs(feet.y - decision.y) <= FloorTolerance &&
                  feet.z >= decision.z - 1.45f && feet.x > decision.x && feet.x < BarrierX - 1f)))
            {
                phase = Phase.Closed;
                originalSigns.SetActive(false);
                closure.SetActive(true);
                alternativeSigns.SetActive(true);
                nextNoticeTime = Time.unscaledTime + 4f;
                CampusAudioDirector.Instance?.BeginEmergency();
                SoundNotice.Show("ROUTE NOW CLOSED", "The route ahead is unavailable. Stop and check the nearby EXIT signs.");
                Changed?.Invoke();
            }
            else if (phase == Phase.Closed && (feet - alternative).sqrMagnitude <= CornerRadius * CornerRadius)
            {
                phase = Phase.AlternativeReached;
                CampusAudioDirector.Instance?.PlayCorrect();
                Changed?.Invoke();
            }
            else if (phase == Phase.AlternativeReached && (feet - stairs).sqrMagnitude <= CornerRadius * CornerRadius)
            {
                phase = Phase.Rejoined;
                CampusAudioDirector.Instance?.PlayCorrect();
                Changed?.Invoke();
            }
        }

        public bool AllowsTeleport(Vector3 from, Vector3 destination, out string reason)
        {
            reason = "";
            if (!HasStarted)
                return true;
            if (paused)
            {
                reason = "Resume the practice before teleporting.";
                return false;
            }
            if (!RecoveryCompleted && Mathf.Abs(destination.y - decision.y) > FloorTolerance)
            {
                reason = "Find an available route on this floor before using the stairs.";
                return false;
            }
            Vector3 direction = destination - from;
            var closedBounds = new Bounds(
                new Vector3(BarrierX, decision.y - 0.078f + 1.2f, decision.z), new Vector3(0.8f, 2.4f, 3.2f));
            bool retreating = closedBounds.Contains(from) && !closedBounds.Contains(destination) &&
                (from.x - BarrierX) * (destination.x - BarrierX) > 0f;
            if (phase != Phase.OriginalRoute && !retreating &&
                (closedBounds.Contains(destination) ||
                 (direction.sqrMagnitude > 0.0001f &&
                  closedBounds.IntersectRay(new Ray(from, direction), out float entryDistance) &&
                  entryDistance <= direction.magnitude)))
            {
                reason = "This route is closed. Check the EXIT signs for an available route.";
                return false;
            }
            if (!RecoveryCompleted && !InOpenCorridor(destination))
            {
                reason = "Choose a landing point on an available corridor. Follow its EXIT signs before continuing to the stairs.";
                return false;
            }
            return true;
        }

        public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
        {
            if (!HasStarted || !(interactable is BaseTeleportationInteractable teleport))
                return true;
            Vector3 destination;
            if (teleport is TeleportationAnchor anchor && anchor.teleportAnchorTransform != null)
                destination = anchor.teleportAnchorTransform.position;
            else if (teleport is TeleportationArea && interactor is XRRayInteractor ray &&
                ray.TryGetCurrent3DRaycastHit(out RaycastHit hit) && teleport.colliders.Contains(hit.collider))
                destination = hit.point;
            else
            {
                return false;
            }
            bool allowed = AllowsTeleport(playerPosition, destination, out string reason);
            if (!allowed && interactor != null && interactor.isSelectActive)
                NotifyBlocked(interactor, reason);
            return allowed;
        }

        private void NotifyBlocked(IXRSelectInteractor interactor, string reason)
        {
            if (notifiedInteractors.Contains(interactor))
                return;
            notifiedInteractors.Add(interactor);
            if (Time.unscaledTime < nextNoticeTime)
                return;
            nextNoticeTime = Time.unscaledTime + 4f;
            SoundNotice.Show("TELEPORT UNAVAILABLE", reason);
        }

        private bool InWestCorridor(Vector3 point) =>
            point.x >= decision.x - 9.5f && point.x <= decision.x + 1.5f &&
            point.z >= alternative.z - 1.5f && point.z <= decision.z + 1.5f;

        private bool InOpenCorridor(Vector3 point) =>
            InWestCorridor(point) ||
            (point.z >= decision.z - 1.45f && point.z <= decision.z + 1.45f &&
             point.x >= decision.x - 1.5f && point.x < BarrierX - 0.5f) ||
            (phase == Phase.AlternativeReached &&
             point.z >= alternative.z - 1.5f && point.z <= alternative.z + 1.25f &&
             point.x >= alternative.x - 1.5f && point.x <= stairs.x + CornerRadius);

        private void BuildSignsAndBarrier()
        {
            Material template = Resources.Load<Material>("RouteExerciseMaterial");
            if (template == null || template.shader == null)
            {
                Debug.LogError("Route exercise requires Resources/RouteExerciseMaterial with its URP unlit shader.", this);
                enabled = false;
                return;
            }
            barrierMaterial = new Material(template) { color = new Color(0.9f, 0.25f, 0.08f) };
            exitMaterial = new Material(template) { color = new Color(0.15f, 0.8f, 0.4f) };
            originalSigns = new GameObject("Original Route Signs");
            originalSigns.transform.SetParent(transform, false);
            CreateSign(originalSigns.transform, decision + Vector3.up * 2.3f,
                "ORIGINAL ROUTE\nFollow this corridor towards the stairs.");
            CreateArrow(originalSigns.transform, decision - Vector3.forward * 4f, Vector3.forward);
            CreateArrow(originalSigns.transform, decision + Vector3.right * 1.5f, Vector3.right);
            closure = new GameObject("Closed Corridor Barrier");
            closure.transform.SetParent(transform, false);
            closure.transform.position = new Vector3(BarrierX, decision.y - 0.078f, decision.z);
            var wall = closure.AddComponent<BoxCollider>();
            wall.center = new Vector3(0f, 1.2f, 0f);
            wall.size = new Vector3(0.3f, 2.4f, 3.2f);
            for (int i = 0; i < 3; i++)
            {
                var rail = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rail.name = "Route Closed Rail";
                rail.transform.SetParent(closure.transform, false);
                rail.transform.localPosition = new Vector3(0f, 0.45f + i * 0.65f, 0f);
                rail.transform.localScale = new Vector3(0.15f, 0.18f, 3.2f);
                rail.GetComponent<Renderer>().sharedMaterial = barrierMaterial;
                Destroy(rail.GetComponent<Collider>());
            }
            CreateSign(closure.transform, closure.transform.position + Vector3.up * 2f,
                "ROUTE CLOSED\nDo not enter.\nCheck EXIT signs.");
            alternativeSigns = new GameObject("Alternative Route Signs");
            alternativeSigns.transform.SetParent(transform, false);
            CreateSign(alternativeSigns.transform, decision + Vector3.up * 2.3f,
                "EXIT / STAIRS\nUse the opposite corridor.\nFollow the green arrows.");
            CreateArrow(alternativeSigns.transform, decision - Vector3.forward * 2f, Vector3.back);
            CreateSign(alternativeSigns.transform, alternative + Vector3.up * 2.3f,
                "EXIT / STAIRS\nContinue along this corridor.");
            CreateArrow(alternativeSigns.transform, alternative + Vector3.right * 2f, Vector3.right);
            originalSigns.SetActive(false);
            closure.SetActive(false);
            alternativeSigns.SetActive(false);
        }

        private static void CreateSign(Transform parent, Vector3 position, string message)
        {
            var sign = new GameObject("Route Sign", typeof(RectTransform), typeof(Canvas));
            sign.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            sign.transform.SetParent(parent, false);
            sign.transform.position = position;
            sign.transform.localScale = new Vector3(-0.0015f, 0.0015f, 0.0015f);
            sign.GetComponent<RectTransform>().sizeDelta = new Vector2(1000f, 330f);
            sign.AddComponent<CampusEmergency.Building.LookAtPlayerYOnly>();
            var background = new GameObject("Background", typeof(RectTransform), typeof(Image));
            background.transform.SetParent(sign.transform, false);
            var rect = background.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            background.GetComponent<Image>().color = new Color(0.02f, 0.18f, 0.1f, 0.97f);
            background.GetComponent<Image>().raycastTarget = false;
            var label = new GameObject("Text", typeof(RectTransform), typeof(Text));
            label.transform.SetParent(background.transform, false);
            rect = label.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.one * 20f;
            rect.offsetMax = Vector2.one * -20f;
            Text text = label.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 44;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;
            text.text = message;
        }

        private void CreateArrow(Transform parent, Vector3 position, Vector3 direction)
        {
            if (arrowMesh == null)
            {
                arrowMesh = new Mesh { name = "Route Arrow" };
                arrowMesh.vertices = new[] {
                    new Vector3(-0.13f, 0f, -0.6f), new Vector3(0.13f, 0f, -0.6f),
                    new Vector3(-0.13f, 0f, 0.2f), new Vector3(0.13f, 0f, 0.2f),
                    new Vector3(-0.45f, 0f, 0.2f), new Vector3(0.45f, 0f, 0.2f),
                    new Vector3(0f, 0f, 0.8f) };
                arrowMesh.triangles = new[] { 0, 2, 1, 1, 2, 3, 4, 6, 5 };
                arrowMesh.RecalculateNormals();
                arrowMesh.RecalculateBounds();
            }
            var arrow = new GameObject("Exit Direction Arrow", typeof(MeshFilter), typeof(MeshRenderer));
            arrow.transform.SetParent(parent, false);
            arrow.transform.position = new Vector3(position.x, decision.y - 0.038f, position.z);
            arrow.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            arrow.GetComponent<MeshFilter>().sharedMesh = arrowMesh;
            arrow.GetComponent<MeshRenderer>().sharedMaterial = exitMaterial;
        }

        private void OnDestroy()
        {
            if (barrierMaterial != null) Destroy(barrierMaterial);
            if (exitMaterial != null) Destroy(exitMaterial);
            if (arrowMesh != null) Destroy(arrowMesh);
        }
    }
}
