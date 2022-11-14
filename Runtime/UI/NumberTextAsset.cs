using System;
using UnityEditor;
using UnityEngine;

namespace Zx.UI
{
    [Serializable]
    public class NumberTextData
    {
        public int number;
        public Sprite sprite;
    }

    public class NumberTextAsset : ScriptableObject
    {
        public float designFontSize;
        public NumberTextData[] data;

        public NumberTextData GetData(int i)
        {
            return data[i];
        }
    }

    public static class NumberTextMenu
    {
        [MenuItem("Assets/Create/UINumTexAsset", false, 0)]
        public static void CreateScriptableObject()
        {
            // var selection = Selection.activeObject;
            // var assetPath = AssetDatabase.GetAssetPath(selection);
            var path = $"{nameof(NumberTextAsset)}.asset";
            var texAsset = ScriptableObject.CreateInstance<NumberTextAsset>();
            texAsset.data = new NumberTextData[10];
            for (var i = 0; i < 10; i++)
            {
                texAsset.data[i] = new NumberTextData
                {
                    number = i
                };
            }
            ProjectWindowUtil.CreateAsset(texAsset, path);
        }
    }
}