using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration;
    public float magnitude;
    private RectTransform _rectTransform;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    [ContextMenu("Shake1")]
    public void Shake1()
    {
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        Vector2 orignalPosition = _rectTransform.anchoredPosition;
        Debug.Log(orignalPosition);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-100f, 100f) * magnitude;
            float y = Random.Range(-100f, 100f) * magnitude;
            _rectTransform.anchoredPosition = new Vector2(x, y);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        _rectTransform.anchoredPosition = orignalPosition;
    }
}
