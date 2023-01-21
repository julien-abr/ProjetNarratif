using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Team02
{
    public class CrowdController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private int numberOfDialogs;
        private RectTransform rectTransform;
        private CanvasScaler canvasScaler;
        private float amountToMove;
        private float currentX;

        [Header("Options")]
        [SerializeField, Range(50f, 150f)] int offset = 100;
        [SerializeField, Range(0.5f, 3f)] private float moveDuration;
        [SerializeField] private Ease easeMode;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasScaler = canvas.GetComponent<CanvasScaler>();
        }

        public void LeftFighterWon() //Call when player won
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

        private void ResetData(int AmountOfDialogs) //Call between each battlephase
        {
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
            numberOfDialogs = numberOfDialogs ;
            amountToMove = ((canvasScaler.referenceResolution.x / 2) - offset) / numberOfDialogs;
        }
    }
}

