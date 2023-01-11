using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zx.Core;

namespace Zx.UI
{
    /// <summary>
    /// 点击该区域等价于按下键盘上的某个键
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class KeyboardRaycast : Graphic, IPointerDownHandler, IPointerUpHandler
    {
        public Key bindKey;

        protected KeyboardRaycast()
        {
            useLegacyMeshGeneration = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            MockDevice.Keyboard(bindKey, true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            MockDevice.Keyboard(bindKey, false);
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}