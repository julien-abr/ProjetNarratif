using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Team02
{
    public class Choices : MonoBehaviour
    {
        [SerializeField]
        private ChoiceManager choiceManager;

        public Text choiceText;
        public string idChoice;
        public string IDChoice => idChoice;

        [SerializeField]
        private bool enemy;

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
            return;

            #if UNITY_EDITOR
            if (effectiveness == LINETYPE.EFFECTIVE)
            {
                choiceText.color = Color.green;
            }
            else if (effectiveness == LINETYPE.INEFFECTIVE)
            {
                choiceText.color = Color.red;
            }
            else
            {
                choiceText.color = Color.white;
            }
            #endif
        }

        private IEnumerator WriteText(string textLine)
        {
            choiceText.color = Color.white;

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
            float score = 0;

            if (!enemy)
            {
                switch (effectiveness)
                {
                    case LINETYPE.EFFECTIVE:
                        score = 2;
                        choiceText.color = Color.green;
                        break;
                    case LINETYPE.NORMAL:
                        score = 1;
                        choiceText.color = Color.white;
                        break;
                    case LINETYPE.INEFFECTIVE:
                        score = -1;
                        choiceText.color = Color.red;
                        break;
                    default:
                        break;
                }
            }

            choiceManager.StartCoroutine(choiceManager.SwitchSpeaker(score, effectiveness));
        }
    }

}
