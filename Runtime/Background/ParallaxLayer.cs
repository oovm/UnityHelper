using UnityEngine;

namespace Zx.Background
{
    [RequireComponent(typeof(RectTransform))]
    public class ParallaxLayer : MonoBehaviour
    {
        public RectTransform rect;
        public float width => rect.rect.width;
    }
}