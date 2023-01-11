using System.Collections.Generic;
using UnityEngine;

namespace Zx.Background
{
    public class ParallaxCompose : MonoBehaviour
    {
        public Vector2 offset;
        public List<ParallaxBackground> renderers;
        
        public void Reset()
        {
            foreach (var render in renderers)
            {
                var pos = render.transform.position;
                pos.x = offset.x * (1 + render.factor.x);
                pos.y = offset.y * (1 + render.factor.y);
                render.transform.position = -pos;
            }
        }
    }
}