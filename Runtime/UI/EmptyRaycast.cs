using UnityEngine;
using UnityEngine.UI;

namespace Zx.UI
{
    /// <summary>
    /// 重设 UI 事件区域, 包括 点击, 拖拽, 滚动 等事件
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class EmptyRaycast : Graphic
    {
        protected EmptyRaycast()
        {
            useLegacyMeshGeneration = false;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}