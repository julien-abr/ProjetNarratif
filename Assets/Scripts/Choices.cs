using UnityEngine;
using UnityEngine.UI;

namespace Team02
{   
    public class Choices : MonoBehaviour
    {
        [SerializeField]
        private ChoiceManager choiceManager;

        public Text choiceText;
        private int idChoice;
        public int IDChoice => idChoice;

        public DialogEffectiveness effectiveness;
        private DialogEffectiveness Effectiveness => effectiveness;

        public void Start()
        {
            choiceText ??= this.gameObject.GetComponent<Text>();

            choiceManager ??= FindObjectOfType<ChoiceManager>();
        }

        public enum DialogEffectiveness
        {
            Ineffective,
            Normal,
            Critical
        }

        public void UpdateTextFight(string textLine)
        {
            choiceText.text = ($"- {textLine}");
        }

        public void Selected()
        {
            Debug.Log("Selected");
            choiceManager.GoNextStage();
        }
    }

}
