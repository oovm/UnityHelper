using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Zx.UI
{
    /// <summary>
    /// 编译期扩展
    /// </summary>
    public static class UIExtension
    {
        public static Transform FindRecursive(this Transform parent, string name)
        {
            var t = parent.Find(name);
            return t != null
                ? t
                : parent.Cast<Transform>().Select(child => child.FindRecursive(name)).FirstOrDefault(s => s != null);
        }

        public static void RemoveChildrenRaycast(this Graphic graphic, bool self)
        {
            foreach (var raycast in graphic.GetComponentsInChildren<Graphic>(true))
            {
                raycast.raycastTarget = false;
            }

            graphic.raycastTarget = self;
        }
    }
}