using UnityEngine;

namespace CampusEmergency.UI
{
    /// <summary>
    /// UI 总管：HUD、主菜单、警报 / 疏散提示、结算面板的显隐切换。
    /// 只负责表现层：监听 Core / Emergency 的事件并刷新界面，不修改业务数据。
    /// 所有 UI 引用通过 Inspector 拖入，避免运行时动态查找。
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // TODO: [SerializeField] private CanvasGroup hudLayer;
        // TODO: [SerializeField] private CanvasGroup menuLayer;
        // TODO: [SerializeField] private CanvasGroup popupLayer;
        // TODO: [SerializeField] private TMP_Text interactionPromptText;

        // TODO: OnEnable / OnDisable 中订阅 / 退订 GameManager.StateChanged
        // 与 EmergencyManager 的警报事件，按状态切换各层级显隐。
    }
}
