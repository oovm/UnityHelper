using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Zx.UI
{
    public static class VectorExtension
    {
        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }
    }

    public static class ColorExtension
    {
        /// <summary>
        /// 灰度颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color Grey(float value)
        {
            return new Color(value, value, value);
        }

        /// <summary>
        /// 重设 Alpha 值, 0-1
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}