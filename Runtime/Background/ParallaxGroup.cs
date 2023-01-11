using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zx.Extensions;

namespace Zx.Background
{
    public class ParallaxGroup : MonoBehaviour
    {
        public bool random = true;
        public ParallaxLayer[] layers;

        private IEnumerator<ParallaxLayer> _layers;
        private float _leftSide;

        private void FixedUpdate()
        {
        }

        public void CreateOne()
        {
            var next = NextLayer();
            var go = Instantiate(next, transform);
            go.transform.localPosition = new Vector3(_leftSide + go.width / 2.0f, 0, 0);
            _leftSide += go.width;
        }

        public void DestroyOne()
        {
            var child = transform.GetChild(0);
            Destroy(child.gameObject);
        }

        public void DestroyAll()
        {
            _layers = null;
            _leftSide = 0;
            transform.DestroyChildren();
        }

        public ParallaxLayer NextLayer()
        {
            _layers ??= random ? NextRandom().GetEnumerator() : NextRepeat().GetEnumerator();
            _layers.MoveNext();
            return _layers.Current;
        }

        // ReSharper disable IteratorNeverReturns
        private IEnumerable<ParallaxLayer> NextRandom()
        {
            var first = layers.First();
            _leftSide = -first.width / 2.0f;
            yield return first;
            while (true)
            {
                yield return layers[UnityEngine.Random.Range(0, layers.Length)];
            }
        }

        // ReSharper disable IteratorNeverReturns
        private IEnumerable<ParallaxLayer> NextRepeat()
        {
            var first = layers.First();
            _leftSide = -first.width / 2.0f;
            yield return first;
            while (true)
            {
                foreach (var template in layers)
                {
                    yield return template;
                }
            }
        }
    }
}