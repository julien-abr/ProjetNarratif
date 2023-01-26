using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

        //public void LeftFighterWon() //Call when player won
        //{
        //    Vector2 newPos = new Vector2(currentX - amountToMove, rectTransform.anchoredPosition.y);
        //    rectTransform.DOAnchorPos(newPos, moveDuration).SetEase(easeMode);
        //    currentX -= amountToMove;
        //}
        //
        //public void RightFighterWon()
        //{
        //    Vector2 newPos = new Vector2(currentX + amountToMove, rectTransform.anchoredPosition.y);
        //    rectTransform.DOAnchorPos(newPos, moveDuration).SetEase(easeMode);
        //    currentX += amountToMove;
        //}

        public void MoveCrowd(float amountToMove)
        {
            Vector2 newPos = new Vector2(currentX - (this.amountToMove * amountToMove), rectTransform.anchoredPosition.y);
            rectTransform.DOAnchorPos(newPos, moveDuration).SetEase(easeMode);
            currentX -= this.amountToMove * amountToMove;
        }

        public void ResetData(int amountOfDialogs) //Call between each battlephase
        {
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
            numberOfDialogs = amountOfDialogs;
            amountToMove = ((canvasScaler.referenceResolution.x / 2) - offset) / numberOfDialogs;
        }
    }
}

