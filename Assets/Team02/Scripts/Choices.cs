using System.Collections;
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
            if(GameOptions.isInstantText)
                choiceText.text = ($"- {textLine}");
            else
                StartCoroutine(WriteText(textLine));
        }

        private IEnumerator WriteText(string textLine)
        {
            var localizedText = ($"- {textLine}");
            var chars = localizedText.ToCharArray();
            var fullString = string.Empty;
            foreach (var t in chars)
            {
                fullString += t;
                choiceText.text = fullString;
                yield return new WaitForSeconds(0.01f / GameOptions.readSpeed);
            }
        }

        public void Selected()
        {
            Debug.Log("Selected");
            choiceManager.GoNextFightStage();
        }
    }

}
