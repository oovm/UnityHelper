using UnityEditor;
using UnityEngine;
using Zx.Config;
using Component = Zx.UI.EmptyRaycast;

namespace Zx.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Component))]
    public class EmptyRaycastEditor : ValidateEditor<Component>
    {
        protected override void DrawInspectorGUI()
        {
            PropertyField("m_Color");
            PropertyField("m_RaycastTarget");
            InfoBox("重设点击区域, 并无效化所有子节点的点击响应");
            if (GUILayout.Button("强制无效化子节点"))
            {
                OnValidate();
            }
        }

        protected override void OnValidate()
        {
            item.RemoveChildrenRaycast(item.raycastTarget);
        }

        private void OnSceneGUI()
        {
            if (item.raycastTarget)
            {
                var verts = new Vector3[4];
                item.rectTransform.GetWorldCorners(verts);
                var face = item.color.WithAlpha(0.02f);
                var outline = item.color.WithAlpha(0.5f);
                Handles.DrawSolidRectangleWithOutline(verts, face, outline);
            }
        }
    }
}