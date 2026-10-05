using UnityEngine;

namespace CampusEmergency.Building
{
    /// <summary>
    /// 最简单的可开关门：通过 XR 交互或直接调用 Open()/Close() 控制。
    /// 挂在门板 GameObject 上，并在 Inspector 里指定铰链枢轴（门轴）。
    /// </summary>
    public class Door : Interactable
    {
        [Header("门参数")]
        [Tooltip("门轴 Transform（门板绕它旋转）。不填则以自身 pivot 为轴。")]
        [SerializeField] private Transform hingePivot;

        [Tooltip("开门后的旋转角度（度），正数向外开，负数向内开。")]
        [SerializeField] private float openAngle = 90f;

        [Tooltip("开关门动画速度（度/秒）。")]
        [SerializeField] private float smoothSpeed = 90f;

        /// <summary>当前是否处于打开状态。</summary>
        public bool IsOpen { get; private set; }

        private float _currentYaw;   // 当前实际绕 Y 轴的角度
        private float _targetYaw;    // 目标角度

        protected virtual void Start()
        {
            // 初始角度以起始位置为 0 基准
            _currentYaw = 0f;
            _targetYaw = 0f;
        }

        protected virtual void Update()
        {
            // 每帧插值到目标角度
            if (Mathf.Approximately(_currentYaw, _targetYaw)) return;

            _currentYaw = Mathf.MoveTowards(_currentYaw, _targetYaw, smoothSpeed * Time.deltaTime);
            ApplyRotation();
        }

        /// <summary>切换门的开/关状态（XR 交互会自动调用到这里）。</summary>
        public override void Interact(GameObject interactor)
        {
            Toggle();
        }

        /// <summary>切换开/关。</summary>
        public void Toggle()
        {
            if (IsOpen) Close();
            else Open();
        }

        /// <summary>开门。</summary>
        public void Open()
        {
            if (IsOpen) return;
            IsOpen = true;
            _targetYaw = openAngle;
            Debug.Log($"[Door] {name} 已打开");
        }

        /// <summary>关门。</summary>
        public void Close()
        {
            if (!IsOpen) return;
            IsOpen = false;
            _targetYaw = 0f;
            Debug.Log($"[Door] {name} 已关闭");
        }

        /// <summary>把当前 Yaw 角应用到门轴 / 自身旋转。</summary>
        private void ApplyRotation()
        {
            if (hingePivot != null)
            {
                // 绕门轴的 Y 轴旋转
                hingePivot.localRotation = Quaternion.Euler(0f, _currentYaw, 0f);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0f, _currentYaw, 0f);
            }
        }
    }
}