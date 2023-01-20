using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Team02
{
    public class CrowdController : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Canvas canvas;

        [SerializeField] private int numberOfDialogue;
        private CanvasScaler canvasScaler;
        private float amountToMove;
        private float currentX;

        [Header("Options")]
        [SerializeField, Range(50f, 150f)] int offset = 100;
        [SerializeField, Range(0.5f, 3f)] private float moveDuration;
        [SerializeField] private Ease easeMode;

        private void Start()
        {
            canvasScaler = canvas.GetComponent<CanvasScaler>();
            ResetData();
        }

        public void LeftFighterWon()
        {
            Vector2 newPos = new Vector2(currentX - amountToMove, rectTransform.anchoredPosition.y);
            rectTransform.DOAnchorPos(newPos, moveDuration).SetEase(easeMode);
            currentX -= amountToMove;
        }

        public void RightFighterWon()
        {
            Vector2 newPos = new Vector2(currentX + amountToMove, rectTransform.anchoredPosition.y);
            rectTransform.DOAnchorPos(newPos, moveDuration).SetEase(easeMode);
            currentX += amountToMove;
        }

        private void ResetData()
        {
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
            // numberOfDialogue = get dialogs ;
            amountToMove = ((canvasScaler.referenceResolution.x / 2) - offset) / numberOfDialogue;
        }

        private void WriteText()
        {
            string localizedText = "sentence";
            char[] _chars = localizedText.ToCharArray();
            string fullString = string.Empty;
            for (int i = 0; i < _chars.Length; i++)
            {
                fullString += _chars[i];
                //dialogueTxt.text = fullString;
                //yield return new WaitForSeconds(0.01f / GameOptions.readSpeed);
            }

        }
    }
}

