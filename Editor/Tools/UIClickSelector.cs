using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zx.Tools
{
    /// <summary>
    /// 鼠标中键选择 UI 图层
    /// </summary>
    public class UIMiddleClickSelector : MonoBehaviour
    {
        public bool IgnoreButton = true;
        public bool IgnoreImage = false;
        public bool IgnoreText = false;
        public bool IgnoreToggle = true;
        public bool IgnoreInput = true;
#if UNITY_EDITOR
        private EventSystem eventSystem;

        private void GetObjectFormClick()
        {
            if (eventSystem == null)
            {
                //获取场景中EventSystem
                eventSystem = FindObjectOfType<EventSystem>();
            }

            if (eventSystem == null)
            {
                throw new Exception("EventSystem is Null");
            }

            var eventData = new PointerEventData(eventSystem)
            {
                //PointerEventData 由press开始,表示按下坐标
                pressPosition = Input.mousePosition,
                //抬起坐标
                position = Input.mousePosition
            };
            //获取场景中所有GraphicRaycaster，该组件挂载在各个Canvas上面
            var graphicRaycast = Resources.FindObjectsOfTypeAll<GraphicRaycaster>();
            foreach (var t in graphicRaycast)
            {
                var results = new List<RaycastResult>();
                t.Raycast(eventData, results);
                foreach (var result in results)
                {
                    if (TryPingButton(result.gameObject)) return;
                    if (TryPingText(result.gameObject)) return;
                    if (TryPingImage(result.gameObject)) return;
                }
            }
        }

        private static void PingObject(GameObject go)
        {
            EditorGUIUtility.PingObject(go);
            Selection.activeObject = go;
            // Debug.Log(go.name, go);
        }

        private bool TryPingButton(GameObject go)
        {
            if (IgnoreButton) return false;
            if (go.GetComponent<Button>() == null) return false;
            PingObject(go);
            return true;
        }

        private bool TryPingText(GameObject go)
        {
            if (IgnoreText) return false;
            if (go.GetComponent<Text>() == null) return false;
            PingObject(go);
            return true;
        }

        private bool TryPingImage(GameObject go)
        {
            if (IgnoreImage) return false;
            if (go.GetComponent<Image>() == null && go.GetComponent<RawImage>() == null) return false;
            PingObject(go);
            return true;
        }

        private void Update()
        {
            //鼠标中键
            if (Input.GetMouseButtonDown(2))
            {
                GetObjectFormClick();
            }
        }
#endif
    }
}