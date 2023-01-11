using System;
using UnityEditor;
using UnityEngine;
using Component = Zx.Background.ParallaxCompose;

namespace Zx.Background
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Component))]
    public class ParallaxComposeEditor : ValidateEditor<Component>
    {
        protected override void DrawInspectorGUI()
        {
            PropertyField(nameof(item.offset));
            using (new EditorGUI.DisabledScope(true))
            {
                PropertyField(nameof(item.renderers));
            }
        }
        
        protected override void OnValidate()
        {
            item.renderers.Clear();
            foreach (Transform child in item.transform)
            {
                var group = child.GetComponent<ParallaxBackground>();
                if (group != null)
                {
                    item.renderers.Add(group);
                }
            }

            item.Reset();
        }
    }
}