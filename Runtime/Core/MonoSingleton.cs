using UnityEngine;

namespace Zx.Core
{
    /// <summary>
    /// 带 Inspector 的单例类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // ReSharper disable InvertIf
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
#pragma warning disable CS8618
        private static T _instance;
#pragma warning restore CS8618

        protected static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objects = FindObjectsOfType(typeof(T)) as T[];
                    if (objects is {Length: > 0})
                    {
                        _instance = objects[0];
                    }

                    if (objects is {Length: > 1})
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    
                    if (_instance == null)
                    {
                        var obj = new GameObject
                        {
                            hideFlags = HideFlags.HideAndDontSave
                        };
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected abstract void Initialize();
        protected abstract void Clear();
    }
}