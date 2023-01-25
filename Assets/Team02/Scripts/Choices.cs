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
        public string idChoice;
        public string IDChoice => idChoice;

        public LINETYPE effectiveness;
        public LINETYPE Effectiveness => effectiveness;

        public void Start()
        {
            choiceText ??= this.gameObject.GetComponent<Text>();

            choiceManager ??= FindObjectOfType<ChoiceManager>();
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
            choiceManager.SwitchSpeaker();
        }
    }

}
