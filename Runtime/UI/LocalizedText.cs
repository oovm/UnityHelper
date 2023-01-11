using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Zx.UI
{
    [ExecuteInEditMode]
    public class LocalizedText : TextMeshProUGUI
    {
        [SerializeField] // language key
        private string m_TextKey;
        public string textKey
        {
            get => m_TextKey;
            set
            {
                if (m_TextKey != value)
                {
                    m_TextKey = value;
                    UpdateLanguageText();
                    SetVerticesDirty();
                }
            }
        }

        // [SerializeField] // 字体
        // private FontVariant m_FontVariants;


        // public bool needOutLine = false;
        [SerializeField] protected Color32 mOutlineColor = Color.black;
        [SerializeField] protected float mOutlineWidth = 0.1f;



        public override Color color
        {
            get => m_fontColor32;
            set
            {
                if (m_fontColor32 == value) return;
                m_havePropertiesChanged = true;
                m_fontColor32 = value;
                SetVerticesDirty();
                defaultColor = color;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateLanguageText();
            defaultColor = color;
            UpdateOutLine();
        }

        public void UpdateOutLine()
        {
            if (mOutlineWidth > 0)
            {
                SetOutlineColor(mOutlineColor);
                outlineWidth = mOutlineWidth;
            }
        }

        public void UpdateLanguageText()
        {
            if (!string.IsNullOrEmpty(textKey))
            {
                // var content = ConfigManager.instance.GetLanguage(textKey);
                text = textKey;
            }
        }

        private Color defaultColor;
        public void Grey(bool enable)
        {
            if (enable)
            {
                var c = defaultColor;
                var v = c.r * 0.22f + c.g * 0.707f + c.b * 0.071f;
                color = new Color(v, v, v, c.a);
            }
            else
            {
                color = defaultColor;
            }
        }
    }
}