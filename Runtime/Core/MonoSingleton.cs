using System;
using UnityEngine;

namespace Zx.Core
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> lazy = new(LazyCreate());
        public static T Instance => lazy.Value;
        protected abstract void Initialize();
        public abstract void Clear();
        private static T LazyCreate()
        {
            var owner = new GameObject($"Create Singleton {typeof(T).Name}");
            var instance = owner.AddComponent<T>();
            DontDestroyOnLoad(owner);
            return instance;
        }
    }
}