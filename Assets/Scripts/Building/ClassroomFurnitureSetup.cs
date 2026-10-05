using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace CampusEmergency.Building
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public sealed class ClassroomFurnitureSetup : MonoBehaviour
    {
        private const float SpawnAnchorFloorOffset = 0.078f;
        private const float DeskScale = 0.2f;
        private const float ChairScale = 0.2f;
        private const float CardboardScale = 0.18f;
        private const float DoorOpeningWidth = 4.2f;
        private const float DoorOpeningHeight = 2.29f;
        private const float DoorFrameDepthScale = 0.18f;

        [SerializeField] private Transform spawnAnchor;
        [SerializeField] private GameObject deskModel;
        [SerializeField] private GameObject chairModel;
        [SerializeField] private GameObject cardboardModel;
        [SerializeField] private GameObject doorwayModel;

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && gameObject.scene.IsValid() && !UnityEditor.EditorUtility.IsPersistent(gameObject))
                EnsureCampusFurnitureExists();
#endif
        }

        private void Start()
        {
            if (!Application.isPlaying)
                return;

            EnsureCampusFurnitureExists();
            AddPlayerPushSupport();
        }

        private void EnsureCampusFurnitureExists()
        {
            if (!ValidateSetup())
                return;

            if (transform.Find("Campus Furniture") != null || transform.Find("Runtime Campus Furniture") != null)
                return;

            GameObject furnitureRoot = new GameObject("Campus Furniture");
            furnitureRoot.transform.SetParent(transform, false);

#if UNITY_EDITOR
            if (!Application.isPlaying)
                UnityEditor.Undo.RegisterCreatedObjectUndo(furnitureRoot, "Generate Campus Furniture");
#endif

            PlaceClassroomFurniture(furnitureRoot.transform);
            PlaceDoorFrames(furnitureRoot.transform);
            PlaceCorridorBoxes(furnitureRoot.transform);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
#endif
        }

        private bool ValidateSetup()
        {
            bool valid = true;
            if (spawnAnchor == null)
            {
                Debug.LogError("[ClassroomFurnitureSetup] The fourth-floor classroom spawn anchor is not assigned.", this);
                valid = false;
            }

            if (deskModel == null || chairModel == null || cardboardModel == null || doorwayModel == null)
            {
                Debug.LogError("[ClassroomFurnitureSetup] Assign the desk, chair, cardboard box, and doorway models.", this);
                valid = false;
            }

            return valid;
        }

        private void PlaceClassroomFurniture(Transform parent)
        {
            float floorY = spawnAnchor.position.y - SpawnAnchorFloorOffset;
            Vector2[] deskOffsets =
            {
                new Vector2(-1.5f, -1.45f),
                new Vector2(1.5f, -1.45f),
                new Vector2(-1.5f, 1.45f),
                new Vector2(1.5f, 1.45f)
            };

            for (int i = 0; i < deskOffsets.Length; i++)
            {
                Vector2 offset = deskOffsets[i];
                Vector3 deskPosition = spawnAnchor.TransformPoint(new Vector3(offset.x, 0f, offset.y));
                Bounds deskBounds = GetScaledModelBounds(deskModel, Vector3.one * DeskScale);
                CreatePushable(
                    parent,
                    $"Classroom Desk {i + 1}",
                    deskModel,
                    deskPosition + Vector3.up * (floorY - spawnAnchor.position.y + deskBounds.extents.y),
                    spawnAnchor.rotation,
                    Vector3.one * DeskScale,
                    8f);

                Vector3 chairPosition = spawnAnchor.TransformPoint(new Vector3(offset.x, 0f, offset.y - 0.95f));
                Bounds chairBounds = GetScaledModelBounds(chairModel, Vector3.one * ChairScale);
                CreatePushable(
                    parent,
                    $"Classroom Chair {i + 1}",
                    chairModel,
                    chairPosition + Vector3.up * (floorY - spawnAnchor.position.y + chairBounds.extents.y),
                    spawnAnchor.rotation,
                    Vector3.one * ChairScale,
                    3f);
            }
        }

        private void PlaceDoorFrames(Transform parent)
        {
            float floorY = spawnAnchor.position.y - SpawnAnchorFloorOffset;
            Bounds modelBounds = GetLocalModelBounds(doorwayModel.transform);
            Vector3 frameScale = new Vector3(
                DoorOpeningWidth / modelBounds.size.x,
                DoorOpeningHeight / modelBounds.size.y,
                DoorFrameDepthScale);
            Bounds scaledBounds = GetScaledModelBounds(doorwayModel, frameScale);
            Vector3 doorwayCenter = spawnAnchor.TransformPoint(new Vector3(-4.05f, 0f, 0f));

            CreateVisual(
                parent,
                "Classroom Door Frame",
                doorwayModel,
                new Vector3(doorwayCenter.x, floorY + scaledBounds.extents.y, doorwayCenter.z),
                spawnAnchor.rotation * Quaternion.Euler(0f, 90f, 0f),
                frameScale);
        }

        private void PlaceCorridorBoxes(Transform parent)
        {
            float floorY = spawnAnchor.position.y - SpawnAnchorFloorOffset;
            Bounds boxBounds = GetScaledModelBounds(cardboardModel, Vector3.one * CardboardScale);
            double[] corridorOffsets =
            {
                -15.0, -12.0, -9.0, -6.0, -3.0,
                0.0, 3.0, 6.0, 9.0, 12.0
            };
            var random = new System.Random(5424);

            for (int i = 0; i < corridorOffsets.Length; i++)
            {
                float localX = -6.1f + (float)(random.NextDouble() * 0.9 - 0.45);
                float localZ = (float)corridorOffsets[i] + (float)(random.NextDouble() * 1.1 - 0.55);
                Vector3 floorPosition = spawnAnchor.TransformPoint(new Vector3(localX, 0f, localZ));
                CreatePushable(
                    parent,
                    $"Corridor Cardboard Box {i + 1:00}",
                    cardboardModel,
                    new Vector3(floorPosition.x, floorY + boxBounds.extents.y, floorPosition.z),
                    Quaternion.identity,
                    Vector3.one * CardboardScale,
                    1f);
            }
        }

        private void AddPlayerPushSupport()
        {
            CharacterController controller = FindFirstObjectByType<CharacterController>();
            if (controller == null)
            {
                Debug.LogError("[ClassroomFurnitureSetup] No CharacterController was found; the player cannot push the furniture.", this);
                return;
            }

            if (controller.GetComponent<CharacterControllerRigidbodyPusher>() == null)
                controller.gameObject.AddComponent<CharacterControllerRigidbodyPusher>();
        }

        private void CreatePushable(
            Transform parent,
            string objectName,
            GameObject model,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale,
            float mass)
        {
            Bounds bounds = GetScaledModelBounds(model, scale);
            GameObject root = CreateVisual(parent, objectName, model, position, rotation, scale);
            BoxCollider collider = root.AddComponent<BoxCollider>();
            collider.size = bounds.size;

            Rigidbody body = root.AddComponent<Rigidbody>();
            body.mass = mass;
            body.linearDamping = 1f;
            body.angularDamping = 2f;
            body.isKinematic = false;
            body.useGravity = true;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            XRGrabInteractable interactable = root.AddComponent<XRGrabInteractable>();
            interactable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        }

        private static GameObject CreateVisual(
            Transform parent,
            string objectName,
            GameObject model,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale)
        {
            var root = new GameObject(objectName);
            root.transform.SetParent(parent, false);
            root.transform.SetPositionAndRotation(position, rotation);

            Transform visual = Instantiate(model, root.transform, false).transform;
            visual.name = $"{objectName} Model";
            visual.localScale = scale;
            Bounds modelBounds = GetLocalModelBounds(visual);
            visual.localPosition = Vector3.Scale(-modelBounds.center, scale);
            return root;
        }

        private static Bounds GetScaledModelBounds(GameObject model, Vector3 scale)
        {
            Bounds modelBounds = GetLocalModelBounds(model.transform);
            return new Bounds(Vector3.zero, Vector3.Scale(modelBounds.size, scale));
        }

        private static Bounds GetLocalModelBounds(Transform root)
        {
            bool hasBounds = false;
            Bounds combined = default;
            MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>(true);
            foreach (MeshFilter meshFilter in meshFilters)
            {
                Mesh mesh = meshFilter.sharedMesh;
                if (mesh == null)
                    continue;

                Matrix4x4 meshToRoot = root.worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;
                Bounds meshBounds = mesh.bounds;
                Vector3 extents = meshBounds.extents;
                for (int x = -1; x <= 1; x += 2)
                for (int y = -1; y <= 1; y += 2)
                for (int z = -1; z <= 1; z += 2)
                {
                    Vector3 corner = meshBounds.center + Vector3.Scale(extents, new Vector3(x, y, z));
                    Vector3 point = meshToRoot.MultiplyPoint3x4(corner);
                    if (hasBounds)
                        combined.Encapsulate(point);
                    else
                    {
                        combined = new Bounds(point, Vector3.zero);
                        hasBounds = true;
                    }
                }
            }

            if (!hasBounds)
                throw new InvalidOperationException($"Model '{root.name}' has no readable mesh bounds.");

            return combined;
        }
    }
}
