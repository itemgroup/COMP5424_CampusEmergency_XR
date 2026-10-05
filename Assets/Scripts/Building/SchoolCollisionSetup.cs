using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace CampusEmergency.Building
{
    [DefaultExecutionOrder(-1000)]
    [DisallowMultipleComponent]
    public sealed class SchoolCollisionSetup : MonoBehaviour
    {
        private const float FloorHeightTolerance = 0.06f;
        private const float MinimumWalkableNormalY = 0.9f;
        private const float UpNormalToleranceDegrees = 20f;

        private static readonly float[] FloorHeights = { 0f, 4f, 8f, 12f };

        private readonly List<Mesh> generatedMeshes = new List<Mesh>();

        private void Awake()
        {
            BuildCollisionGeometry();
        }

        private void OnDestroy()
        {
            foreach (Mesh mesh in generatedMeshes)
            {
                if (Application.isPlaying)
                {
                    Destroy(mesh);
                }
                else
                {
                    DestroyImmediate(mesh);
                }
            }
        }

        private void BuildCollisionGeometry()
        {
            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(true);
            if (meshFilters.Length == 0)
            {
                Debug.LogError("[SchoolCollisionSetup] No child MeshFilters were found.", this);
                return;
            }

            var collisionVertices = new List<Vector3>();
            var collisionTriangles = new List<int>();
            var floorVertices = new List<Vector3>[FloorHeights.Length];
            var floorTriangles = new List<int>[FloorHeights.Length];
            for (int i = 0; i < FloorHeights.Length; i++)
            {
                floorVertices[i] = new List<Vector3>();
                floorTriangles[i] = new List<int>();
            }

            Matrix4x4 worldToSchool = transform.worldToLocalMatrix;
            foreach (MeshFilter meshFilter in meshFilters)
            {
                Mesh sourceMesh = meshFilter.sharedMesh;
                if (sourceMesh == null)
                {
                    continue;
                }

                if (!sourceMesh.isReadable)
                {
                    Debug.LogError(
                        $"[SchoolCollisionSetup] Mesh '{sourceMesh.name}' is not readable. Enable Read/Write on the school model import settings.",
                        meshFilter);
                    return;
                }

                Vector3[] sourceVertices = sourceMesh.vertices;
                int[] sourceTriangles = sourceMesh.triangles;
                Matrix4x4 meshToSchool = worldToSchool * meshFilter.transform.localToWorldMatrix;

                for (int i = 0; i < sourceTriangles.Length; i += 3)
                {
                    Vector3 a = meshToSchool.MultiplyPoint3x4(sourceVertices[sourceTriangles[i]]);
                    Vector3 b = meshToSchool.MultiplyPoint3x4(sourceVertices[sourceTriangles[i + 1]]);
                    Vector3 c = meshToSchool.MultiplyPoint3x4(sourceVertices[sourceTriangles[i + 2]]);
                    Vector3 normal = Vector3.Cross(b - a, c - a);
                    int floorIndex = FindWalkableFloor(a, b, c, normal);

                    if (floorIndex >= 0)
                    {
                        if (normal.y < 0f)
                        {
                            (b, c) = (c, b);
                        }

                        AppendTriangle(floorVertices[floorIndex], floorTriangles[floorIndex], a, b, c);
                    }
                    else
                    {
                        AppendTriangle(collisionVertices, collisionTriangles, a, b, c);
                    }
                }
            }

            for (int i = 0; i < FloorHeights.Length; i++)
            {
                if (floorTriangles[i].Count == 0)
                {
                    Debug.LogError(
                        $"[SchoolCollisionSetup] No walkable mesh surface was found at local height {FloorHeights[i]} m; no school colliders were created.",
                        this);
                    return;
                }
            }

            Mesh collisionMesh = CreateMesh("School Collision Mesh", collisionVertices, collisionTriangles);
            var collisionCollider = GetComponent<MeshCollider>();
            if (collisionCollider == null)
            {
                collisionCollider = gameObject.AddComponent<MeshCollider>();
            }

            collisionCollider.sharedMesh = collisionMesh;
            generatedMeshes.Add(collisionMesh);

            var floorColliders = new List<Collider>(FloorHeights.Length);
            for (int i = 0; i < FloorHeights.Length; i++)
            {
                Mesh floorMesh = CreateMesh($"School Walkable Floor {FloorHeights[i]} m", floorVertices[i], floorTriangles[i]);
                var floorObject = new GameObject($"Walkable Floor {FloorHeights[i]} m");
                floorObject.transform.SetParent(transform, false);
                var floorCollider = floorObject.AddComponent<MeshCollider>();
                floorCollider.sharedMesh = floorMesh;
                floorColliders.Add(floorCollider);
                generatedMeshes.Add(floorMesh);
            }

            TeleportationArea teleportationArea = GetComponent<TeleportationArea>();
            if (teleportationArea == null)
            {
                Debug.LogError("[SchoolCollisionSetup] A TeleportationArea is required on the school root.", this);
                return;
            }

            teleportationArea.colliders.Clear();
            teleportationArea.colliders.AddRange(floorColliders);
            teleportationArea.filterSelectionByHitNormal = true;
            teleportationArea.upNormalToleranceDegrees = UpNormalToleranceDegrees;
        }

        private static int FindWalkableFloor(Vector3 a, Vector3 b, Vector3 c, Vector3 normal)
        {
            if (normal.sqrMagnitude < 0.000001f)
            {
                return -1;
            }

            float normalY = normal.y / normal.magnitude;
            if (normalY < MinimumWalkableNormalY)
            {
                return -1;
            }

            float minimumY = Mathf.Min(a.y, b.y, c.y);
            float maximumY = Mathf.Max(a.y, b.y, c.y);
            if (maximumY - minimumY > FloorHeightTolerance)
            {
                return -1;
            }

            float centerY = (a.y + b.y + c.y) / 3f;
            for (int i = 0; i < FloorHeights.Length; i++)
            {
                if (Mathf.Abs(centerY - FloorHeights[i]) <= FloorHeightTolerance)
                {
                    return i;
                }
            }

            return -1;
        }

        private static void AppendTriangle(List<Vector3> vertices, List<int> triangles, Vector3 a, Vector3 b, Vector3 c)
        {
            int startIndex = vertices.Count;
            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);
            triangles.Add(startIndex);
            triangles.Add(startIndex + 1);
            triangles.Add(startIndex + 2);
        }

        private Mesh CreateMesh(string meshName, List<Vector3> vertices, List<int> triangles)
        {
            var mesh = new Mesh
            {
                name = meshName,
                indexFormat = IndexFormat.UInt32,
            };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0, false);
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}
