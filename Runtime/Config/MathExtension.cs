using UnityEngine;

namespace Zx.Config
{
    public static class MathExtension
    {
        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector4 WithX(this Vector4 v, float x)
        {
            return new Vector4(x, v.y, v.z, v.w);
        }

        public static Vector4 WithY(this Vector4 v, float y)
        {
            return new Vector4(v.x, y, v.z, v.w);
        }

        public static Vector4 WithZ(this Vector4 v, float z)
        {
            return new Vector4(v.x, v.y, z, v.w);
        }

        public static Vector4 WithW(this Vector4 v, float w)
        {
            return new Vector4(v.x, v.y, v.z, w);
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        public static Color32 WithAlpha(this Color32 color, byte alpha)
        {
            color.a = alpha;
            return color;
        }

        public static Color Grey(float value)
        {
            return new Color(value, value, value);
        }
    }
}