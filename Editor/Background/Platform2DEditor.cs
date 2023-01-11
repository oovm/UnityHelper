using UnityEditor;
using UnityEngine;
using Component = Zx.Background.Platform2D;

namespace Zx.Background
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Component))]
    public class Platform2DEditor : ValidateEditor<Component>
    {
        protected override void DrawInspectorGUI()
        {
            PropertyField(nameof(item.offset));
            PropertyField(nameof(item.random));
            PropertyField(nameof(item.layers));

            using (new EditorGUI.DisabledScope(true))
            {
            }

            if (GUILayout.Button("创建"))
            {
                item.CreateOne();
            }

            if (GUILayout.Button("销毁"))
            {
                item.DestroyAll();
            }
        }

        protected override void OnValidate()
        {
        }
    }
}