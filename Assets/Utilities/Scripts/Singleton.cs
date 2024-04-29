using UnityEngine;

namespace JSM.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance != null && this.gameObject != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
            }
        }
    }
}