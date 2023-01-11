using UnityEditor;
using UnityEngine;
using Component = Zx.Background.ParallaxLayer;

namespace Zx.Background
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Component))]
    public class ParallaxLayerEditor : ValidateEditor<Component>
    {
        protected override void DrawInspectorGUI()
        {
            // PropertyField(nameof(item.random));

            using (new EditorGUI.DisabledScope(true))
            {
            }
        }

        protected override void OnValidate()
        {
            item.rect = item.GetComponent<RectTransform>();
        }
    }
}