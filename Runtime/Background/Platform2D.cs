using UnityEngine;

namespace Zx.Background
{
    public class Platform2D : ParallaxGroup
    {
        public float offset;

        public void Reset()
        {
            transform.localPosition = new Vector3(-offset, 0, 0);
        }
    }
}