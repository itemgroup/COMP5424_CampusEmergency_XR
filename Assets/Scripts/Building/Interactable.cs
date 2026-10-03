using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace CampusEmergency.Building
{
    /// <summary>
    /// 可交互建筑构件基类：电梯（lift）、门、灭火器、报警器等都继承它。
    /// 场景中给物体挂 XRSimpleInteractable 并拖入 simpleInteractable 引用，
    /// XRI 的 activated 事件会自动转发到 <see cref="Interact"/>，子类无需自行订阅。
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("交互提示中显示的名称，例如“消防警报按钮”。")]
        [SerializeField] private string displayName = "Interactable";

        [Tooltip("可选：XRI 简单交互组件，activated 事件转发到 Interact()。")]
        [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable simpleInteractable;

        public string DisplayName => displayName;

        protected virtual void OnEnable()
        {
            if (simpleInteractable != null)
            {
                simpleInteractable.activated.AddListener(OnActivated);
            }
        }

        protected virtual void OnDisable()
        {
            if (simpleInteractable != null)
            {
                simpleInteractable.activated.RemoveListener(OnActivated);
            }
        }

        private void OnActivated(ActivateEventArgs args)
        {
            Interact(args.interactorObject.transform.gameObject);
        }

        /// <summary>玩家发起交互时调用。</summary>
        /// <param name="interactor">发起交互的对象（通常是玩家）。</param>
        public abstract void Interact(GameObject interactor);
    }
}
