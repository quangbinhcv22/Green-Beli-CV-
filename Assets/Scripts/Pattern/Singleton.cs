﻿namespace Pattern
{
    using UnityEngine;
 
    /// <summary>
    /// Inherit from this base class to create a singleton.
    /// e.g. public class MyClassName : Singleton<MyClassName> {}
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Check to see if we're about to be destroyed.
        private static bool s_shuttingDown = false;
        private static readonly object m_Lock = new object();
        private static T s_instance;
 
        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_shuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }
 
                lock (m_Lock)
                {
                    if (s_instance == null)
                    {
                        // Search for existing instance.
                        s_instance = (T)FindObjectOfType(typeof(T));
 
                        // Create new instance if one doesn't already exist.
                        if (s_instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            s_instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
 
                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
 
                    return s_instance;
                }
            }
        }
 
 
        private void OnApplicationQuit()
        {
            s_shuttingDown = true;
        }
 
 
        private void OnDestroy()
        {
            s_shuttingDown = true;
        }
    }
}