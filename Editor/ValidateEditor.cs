using UnityEditor;
using UnityEngine;
using Zx.UI;

namespace Zx
{
    public abstract class ValidateEditor<I> : Editor where I : MonoBehaviour
    {
        protected I item => target as MonoBehaviour as I;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            using (new EditorGUI.DisabledScope(true))
            {
                PropertyField("m_Script", includeChildren: true);
            }

            DrawInspectorGUI();
            EditorApplication.delayCall += delegate
            {
                if (item == null) return;
                OnValidate();
            };
            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void DrawInspectorGUI();

        protected abstract void OnValidate();

        protected void InfoBox(string message, bool wide = true)
        {
            EditorGUILayout.HelpBox(message, MessageType.Info, wide);
        }

        protected void PropertyField(string property, string label = null, bool includeChildren = true)
        {
            var field = serializedObject.FindProperty(property);
            if (label == null)
            {
                EditorGUILayout.PropertyField(field, includeChildren);
            }
        }


        protected T FindName<T>(string o)
        {
            return item.transform.FindRecursive(o).GetComponent<T>();
        }

        protected T FindPath<T>(string path = null)
        {
            return string.IsNullOrEmpty(path) ? item.GetComponent<T>() : item.transform.Find(path).GetComponent<T>();
        }
    }
}