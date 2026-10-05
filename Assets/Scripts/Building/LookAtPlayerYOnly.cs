using UnityEngine;

namespace CampusEmergency.Building
{
    public class LookAtPlayerYOnly : MonoBehaviour
    {
        [SerializeField] private Transform xrMainCamera;
        void Update()
        {
            if(xrMainCamera == null) return;
            Vector3 dir = xrMainCamera.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
