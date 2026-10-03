using UnityEngine;

namespace CampusEmergency.Building
{
    /// <summary>
    /// 可交互建筑构件基类：电梯（lift）、门、灭火器、报警器等都继承它。
    /// 由玩家交互射线 / 碰撞检测调用 <see cref="Interact"/>。
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("交互提示中显示的名称，例如“消防警报按钮”。")]
        [SerializeField] private string displayName = "Interactable";

        public string DisplayName => displayName;

        /// <summary>玩家发起交互时调用。</summary>
        /// <param name="interactor">发起交互的对象（通常是玩家）。</param>
        public abstract void Interact(GameObject interactor);
    }
}
