using UnityEngine;

namespace CampusEmergency.Player
{
    /// <summary>
    /// 玩家 / XR Rig 入口脚本，挂在 XR Rig 根节点。
    /// 职责：连续移动、瞬移、头部/手柄追踪、Input System 输入采集与分发。
    ///
    /// 注意：当前工程尚未安装 XR Interaction Toolkit，
    /// 待安装后在此接入基于 InputAction 的移动 / 瞬移 / 交互输入。
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Transform headTransform;

        /// <summary>当前是否允许玩家移动（警报演示、过场时锁定）。</summary>
        public bool MovementLocked { get; set; }

        // TODO: [SerializeField] private InputActionReference moveAction;
        // TODO: [SerializeField] private InputActionReference teleportModeAction;
        // TODO: [SerializeField] private InputActionReference interactAction;

        private void Update()
        {
            if (MovementLocked)
            {
                return;
            }

            // TODO: 读取 moveAction，驱动 Rig 移动 / 传送
        }
    }
}
