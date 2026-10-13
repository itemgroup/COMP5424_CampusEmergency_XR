using UnityEngine;

namespace CampusEmergency.Building
{
    public class LookAtPlayerYOnly : MonoBehaviour
    {
        [SerializeField] private Transform xrMainCamera;

        private void Awake()
        {
            FindTargetCamera();
        }

        private void FindTargetCamera()
        {
            if (xrMainCamera != null)
                return;

            if (Camera.main != null)
            {
                xrMainCamera = Camera.main.transform;
                return;
            }

            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
            {
                if (camera.enabled && camera.gameObject.activeInHierarchy)
                {
                    xrMainCamera = camera.transform;
                    break;
                }
            }
        }

        private void LateUpdate()
        {
            if (xrMainCamera == null)
                FindTargetCamera();
            if (xrMainCamera == null)
                return;

            Vector3 dir = xrMainCamera.position - transform.position;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
