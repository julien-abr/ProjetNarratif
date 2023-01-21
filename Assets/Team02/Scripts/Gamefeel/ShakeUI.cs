using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team02
{
    public class ShakeUI : MonoBehaviour
    {
        [Header("ShakeOptions")]
        [SerializeField, Range(0.25f, 1.5f)] private float duration;
        [SerializeField, Range(0f, 0.5f)] private  float magnitude;
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        [ContextMenu("Shake1")] //To test
        public void Shake1()
        {
            if (rectTransform == null)
            {
                Debug.LogWarning("Cant shake if your not playing");
                return;
            }
            StartCoroutine(Shake());
        }

        public IEnumerator Shake() //Call this to shake, call with parameter to change shake duration and magnitude
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

