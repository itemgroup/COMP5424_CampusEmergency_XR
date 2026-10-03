using UnityEngine;
using UnityEngine.InputSystem;

namespace CampusEmergency.Player
{
    /// <summary>
    /// 玩家 / XR Rig 连续移动脚本，挂在 XR Origin (XR Rig) 根节点。
    /// 输入来自 XRI Starter Assets 的 "XRI Default Input Actions"（Move 为 Vector2）。
    /// 瞬移与抓取交互由 XRI 组件（Teleportation Area / XRRayInteractor）在场景中配置，不经本脚本。
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Transform headTransform;
        [SerializeField] private InputActionReference moveAction;

        /// <summary>当前是否允许玩家移动（警报演示、过场时锁定）。</summary>
        public bool MovementLocked { get; set; }

        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (MovementLocked || moveAction == null || headTransform == null)
            {
                return;
            }

            Vector2 input = moveAction.action.ReadValue<Vector2>();
            if (input.sqrMagnitude < 0.01f)
            {
                return;
            }

            // VR 惯例：摇杆方向相对头部（视线）朝向
            Vector3 forward = headTransform.forward;
            forward.y = 0f;
            forward.Normalize();
            Vector3 right = headTransform.right;
            right.y = 0f;
            right.Normalize();

            Vector3 offset = (forward * input.y + right * input.x) * (moveSpeed * Time.deltaTime);
            if (characterController != null)
            {
                characterController.Move(offset);
            }
            else
            {
                transform.position += offset;
            }
        }
    }
}
