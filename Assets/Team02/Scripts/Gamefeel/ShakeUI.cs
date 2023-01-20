using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team02
{
    public class ShakeUI : MonoBehaviour
    {
        //METTRE CE SCRIPT SUR UN COMPONENT UI AVEC RECT TRANSFORM PARENT DE TOUS L'UI
        public float duration = 1f;
        public float magnitude = 0.3f;
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        [ContextMenu("Shake1")]
        public void Shake1()
        {
            StartCoroutine(Shake());
        }
        public IEnumerator Shake()
        {
            Vector2 orignalPosition = rectTransform.anchoredPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-100f, 100f) * magnitude;
                float y = Random.Range(-100f, 100f) * magnitude;
                rectTransform.anchoredPosition = new Vector3(x, y);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            rectTransform.anchoredPosition = orignalPosition;
        }
    }
}

