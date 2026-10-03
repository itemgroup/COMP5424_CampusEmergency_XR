using UnityEngine;

namespace CampusEmergency.Utilities
{
    /// <summary>
    /// 跨场景不销毁的泛型单例基类。继承它的管理器在场景切换后依然存在，
    /// 重复实例会被自动销毁。
    /// </summary>
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
