using CampusEmergency.Core;
using UnityEngine;

namespace CampusEmergency.Emergency
{
    /// <summary>
    /// 应急事件总管：火情生成、火灾警报、疏散路线引导、应急任务流程控制。
    /// 通过 GameManager.Instance 驱动全局状态机（Emergency / Evacuating / Ended）。
    /// </summary>
    public class EmergencyManager : MonoBehaviour
    {
        /// <summary>是否正处于应急事件中。</summary>
        public bool IsEmergencyActive { get; private set; }

        /// <summary>触发火灾警报：报警、通知 UI 与音频、切换全局状态。</summary>
        public void TriggerFireAlarm()
        {
            if (IsEmergencyActive)
            {
                return;
            }

            IsEmergencyActive = true;

            // TODO: 播放警报音效、点燃火源粒子
            GameManager.Instance.ChangeState(GameState.Emergency);
            Debug.Log("[Emergency] 火灾警报已触发");
        }

        /// <summary>开始疏散：计算路线、引导玩家与 NPC 前往安全出口。</summary>
        public void StartEvacuation()
        {
            if (!IsEmergencyActive)
            {
                Debug.LogWarning("[Emergency] 尚未触发警报，忽略疏散请求");
                return;
            }

            // TODO: 计算疏散路线、更新路标
            GameManager.Instance.ChangeState(GameState.Evacuating);
            Debug.Log("[Emergency] 疏散开始");
        }

        /// <summary>解除应急状态（救援完成 / 演练结束）。</summary>
        public void ClearEmergency()
        {
            IsEmergencyActive = false;

            // TODO: 关闭火源与警报、复位场景
            GameManager.Instance.ChangeState(GameState.Ended);
            Debug.Log("[Emergency] 应急状态已解除");
        }
    }
}
