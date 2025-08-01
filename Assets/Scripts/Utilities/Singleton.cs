using UnityEngine;

namespace BBS.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        protected virtual bool dontDestroyOnLoad => false;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
                OnSingletonAwake();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected abstract void OnSingletonAwake();

        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}
