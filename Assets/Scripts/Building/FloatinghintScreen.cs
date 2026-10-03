using UnityEngine;
using TMPro;
using CampusEmergency.Building;

namespace CampusEmergency.Building
{
    /// <summary>
    /// Quest3 空间悬浮提示屏幕，继承项目统一Interactable基类
    /// XR手柄射线选中按下交互，触发提示逻辑；屏幕自带缓慢悬浮抖动动画
    /// </summary>
    public class FloatingHintScreen : Interactable
    {
        [Header("悬浮提示屏幕设置")]
        [Tooltip("做悬浮微动动画的父物体")]
        [SerializeField] private Transform floatPivot;
        [Tooltip("显示提示文字TMP组件")]
        [SerializeField] private TMP_Text hintText;
        [Tooltip("屏幕上下浮动幅度")]
        [SerializeField] private float floatAmplitude = 0.08f;
        [Tooltip("浮动循环速度")]
        [SerializeField] private float floatSpeed = 0.4f;

        [Tooltip("屏幕交互触发后是否隐藏屏幕")]
        [SerializeField] private bool hideAfterInteract = false;

        private Vector3 _pivotOriginPos;

        protected virtual void Start()
        {
            if(floatPivot != null)
            {
                _pivotOriginPos = floatPivot.localPosition;
            }
        }

        protected virtual void Update()
        {
            // 持续悬浮上下微动
            if(floatPivot != null)
            {
                float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
                floatPivot.localPosition = new Vector3(_pivotOriginPos.x, _pivotOriginPos.y + offsetY, _pivotOriginPos.z);
            }
        }

        /// <summary>XR交互触发（手柄按下激活）</summary>
        public override void Interact(GameObject interactor)
        {
            Debug.Log($"[悬浮提示] 被玩家交互 {interactor.name}");

            // ==========在这里写你的游戏提醒业务逻辑==========
            ShowGameTip("欢迎来到校园应急演练\n请走向大门进行下一步交互");

            if(hideAfterInteract)
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>外部可以调用，修改屏幕文字</summary>
        public void ShowGameTip(string content)
        {
            if(hintText != null)
            {
                hintText.text = content;
            }
        }
    }
}
