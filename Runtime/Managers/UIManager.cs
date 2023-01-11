using System;
using UnityEngine;
using Zx.Core;

namespace Zx.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public RectTransform UIRoot;
        public string UIDirectory = "Assets/ArtData/UI/";

        protected override void Initialize()
        {
        }

        // ReSharper disable InvertIf
        private T LoadPrefab<T>(string path, string? rename = null) where T : UnityEngine.Object
        {
            var prefab = Resources.Load<T>(UIDirectory + path);
            if (prefab != null)
            {
                var go = Instantiate(prefab, Instance.UIRoot);
                if (rename != null)
                {
                    go.name = rename;
                }

                return go;
            }

            throw new Exception($"UIManager.OpenPanel<{typeof(T).Name}>: `{path}` not found");
        }

        public static GameObject OpenPanel(string path)
        {
            var go = Instance.UIRoot.Find(path).gameObject;
            if (go != null)
            {
                go.SetActive(true);
            }
            else
            {
                go = Instance.LoadPrefab<GameObject>(path, path);
            }

            return go;
        }

        public static T OpenPanel<T>(string path) where T : Component
        {
            var go = Instance.UIRoot.Find(path).GetComponent<T>();
            if (go != null)
            {
                go.gameObject.SetActive(true);
            }
            else
            {
                go = Instance.LoadPrefab<T>(path, path);
            }

            return go;
        }


        public static void HidePanel(string path)
        {
            var go = Instance.UIRoot.Find(path);
            if (go != null)
            {
                go.gameObject.SetActive(false);
            }
        }

        public static void ClosePanel(string path)
        {
            var go = Instance.UIRoot.Find(path);
            if (go != null)
            {
                Destroy(go.gameObject);
            }
        }


        protected override void Clear()
        {
        }
    }
}