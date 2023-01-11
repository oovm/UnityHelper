using UnityEngine;

namespace Zx.Extensions
{
    public static class TransformExtension
    {
        
        
        public static void DestroyChildren(this Transform parent)
        {
#if UNITY_EDITOR
            for (var i = 0; i < 5; i++)
            {
                foreach (Transform child in parent)
                {
                    UnityEngine.Object.DestroyImmediate(child.gameObject);
                }
            }
#else
            foreach (Transform child in parent)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
#endif
        }
    }
}