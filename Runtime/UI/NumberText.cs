using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zx.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class NumberText : MaskableGraphic
    {
        [SerializeField] private int m_Number;
        [SerializeField] private int m_FontSize;
        public NumberTextAsset numTexAsset;
        private Sprite usedSprite;
        [SerializeField] private float m_Space;
        [SerializeField] private float m_Alpha = 1.0f;

        public float alpha
        {
            get { return m_Alpha; }
            set
            {
                if (m_Alpha != value)
                {
                    m_Alpha = value;
                    canvasRenderer.SetAlpha(value);
                }
            }
        }

        public int number
        {
            get { return m_Number; }
            set
            {
                if (m_Number == value) return;
                m_Number = value;
                SetVerticesDirty();
            }
        }

        public int fontSize
        {
            get { return m_FontSize; }
            set
            {
                if (m_FontSize == value) return;
                fontSize = value;
                SetVerticesDirty();
            }
        }

        public float space
        {
            get => m_Space;
            set
            {
                if (m_Space != value)
                {
                    m_Space = value;
                    SetVerticesDirty();
                }
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            canvasRenderer.SetAlpha(m_Alpha);
        }

        private void SplitNumToList(int num, List<int> result)
        {
            var nextValue = num / 10;
            var value = num - nextValue * 10;
            result.Add(value);
            while (nextValue > 0)
            {
                num = nextValue;
                nextValue /= 10;
                result.Add(num - nextValue * 10);
            }

            result.Reverse();
        }

        public override Texture mainTexture
        {
            get
            {
                if (usedSprite == null)
                {
                    if (material != null && material.mainTexture != null)
                    {
                        return material.mainTexture;
                    }

                    return s_WhiteTexture;
                }

                return usedSprite.texture;
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            if (numTexAsset == null) return;
            // var list = PoolManager.instance.GetList<int>();
            var list = new List<int>(10);
            SplitNumToList(number, list);
            var triangleIndex = 0;
            var drawSize = new Vector2(fontSize * list.Count + m_Space * Mathf.Max(0, list.Count - 1), fontSize);
            var xStart = drawSize.x / -2;
            for (var index = 0; index < list.Count; index++)
            {
                var i = list[index];
                var info = numTexAsset.GetData(i);
                var r = new Rect(fontSize * (index + 0.5f) + m_Space * index + xStart, 0, fontSize, fontSize);
                GenerateSprite(info.sprite, vh, false, r, drawSize, ref triangleIndex);
                if (index == 0)
                {
                    usedSprite = info.sprite;
                }
            }
            // PoolManager.instance.Recycle(list);
        }

        private void GenerateSprite(Sprite activeSprite, VertexHelper vh, bool lPreserveAspect, Rect r,
            Vector2 drawingSize, ref int triangleIndex)
        {
            var spriteSize = new Vector2(activeSprite.rect.width, activeSprite.rect.height);

            // Covert sprite pivot into normalized space.
            var spritePivot = activeSprite.pivot / spriteSize;
            var rectPivot = rectTransform.pivot;
            //Rect r = GetPixelAdjustedRect();

            if (lPreserveAspect & spriteSize.sqrMagnitude > 0.0f)
            {
                PreserveSpriteAspectRatio(ref r, spriteSize);
            }

            //Vector2 drawingSize = new Vector2(r.width, r.height);
            var spriteBoundSize = activeSprite.bounds.size;

            // Calculate the drawing offset based on the difference between the two pivots.
            var drawOffset = (rectPivot - spritePivot) * drawingSize;

            var color32 = color;
            var vertices = activeSprite.vertices;
            var uvs = activeSprite.uv;
            for (var i = 0; i < vertices.Length; ++i)
            {
                vh.AddVert(
                    new Vector3(
                        vertices[i].x / spriteBoundSize.x * r.width - drawOffset.x + r.xMin,
                        vertices[i].y / spriteBoundSize.y * r.height - drawOffset.y),
                    color32,
                    new Vector2(uvs[i].x, uvs[i].y));
            }

            var triangles = activeSprite.triangles;
            for (var i = 0; i < triangles.Length; i += 3)
            {
                vh.AddTriangle(
                    triangles[i + 0] + triangleIndex,
                    triangles[i + 1] + triangleIndex,
                    triangles[i + 2] + triangleIndex
                );
            }

            triangleIndex += vertices.Length;
        }

        private void PreserveSpriteAspectRatio(ref Rect rect, Vector2 spriteSize)
        {
            var spriteRatio = spriteSize.x / spriteSize.y;
            var rectRatio = rect.width / rect.height;

            if (spriteRatio > rectRatio)
            {
                var oldHeight = rect.height;
                rect.height = rect.width * (1.0f / spriteRatio);
                rect.y += (oldHeight - rect.height) * rectTransform.pivot.y;
            }
            else
            {
                var oldWidth = rect.width;
                rect.width = rect.height * spriteRatio;
                rect.x += (oldWidth - rect.width) * rectTransform.pivot.x;
            }
        }
    }
}