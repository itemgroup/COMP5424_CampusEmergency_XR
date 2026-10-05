using System.Collections.Generic;
using UnityEngine;

namespace CampusEmergency.Building
{
    [DisallowMultipleComponent]
    public sealed class CharacterControllerRigidbodyPusher : MonoBehaviour
    {
        private const float PushCooldown = 0.15f;
        private const float PushStrength = 0.75f;

        private readonly Dictionary<Rigidbody, float> lastPushTimes = new Dictionary<Rigidbody, float>();
        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("[CharacterControllerRigidbodyPusher] This component requires a CharacterController.", this);
                enabled = false;
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.rigidbody;
            if (body == null || body.isKinematic)
                return;

            Vector3 pushDirection = hit.moveDirection;
            pushDirection.y = 0f;
            if (pushDirection.sqrMagnitude < 0.0001f || characterController.velocity.sqrMagnitude < 0.04f)
                return;

            float now = Time.time;
            if (lastPushTimes.TryGetValue(body, out float lastPushTime) && now - lastPushTime < PushCooldown)
                return;

            lastPushTimes[body] = now;
            float speed = Mathf.Clamp(characterController.velocity.magnitude * PushStrength, 0.25f, 1.5f);
            body.AddForceAtPosition(pushDirection.normalized * speed * body.mass, hit.point, ForceMode.Impulse);
        }
    }
}
