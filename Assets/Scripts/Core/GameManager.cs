using System;
using CampusEmergency.Utilities;
using UnityEngine;

namespace CampusEmergency.Core
{
    /// <summary>
    /// 游戏全局状态。按校园应急流程划分，后续可随需求扩展。
    /// </summary>
    public enum GameState
    {
        Boot,
        Menu,
        Preparing,
        Emergency,
        Evacuating,
        Paused,
        Ended
    }

    /// <summary>
    /// 全局管理器：游戏状态机入口，负责子系统初始化与状态切换广播。
    /// 挂在场景中一个常驻 GameObject 上（建议命名 _GameManager）。
    /// </summary>
    public class GameManager : PersistentSingleton<GameManager>
    {
        public GameState CurrentState { get; private set; } = GameState.Boot;

        /// <summary>状态切换事件：参数依次为 oldState, newState。</summary>
        public event Action<GameState, GameState> StateChanged;

        protected override void Awake()
        {
            base.Awake();

            // TODO: 初始化事件系统、音频、存档等子系统
        }

        /// <summary>切换游戏状态，状态相同则忽略，并向所有监听者广播。</summary>
        public void ChangeState(GameState nextState)
        {
            if (CurrentState == nextState)
            {
                return;
            }

            GameState previousState = CurrentState;
            CurrentState = nextState;
            StateChanged?.Invoke(previousState, nextState);
            Debug.Log($"[GameManager] {previousState} -> {nextState}");
        }
    }
}
