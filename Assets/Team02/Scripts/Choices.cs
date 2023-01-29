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

        [Header("Sons")]
        public AudioSource boo;
        public AudioSource yay;
        public AudioSource HOOO;

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
            if (enemy)
            {
                score = -0.75f;
            }
            else
            {
                switch (effectiveness)
                {
                    case LINETYPE.EFFECTIVE:
                        HOOO.Play();
                        score = 2;
                        break;
                    case LINETYPE.NORMAL:
                        yay.Play();
                        score = 1;
                        break;
                    case LINETYPE.INEFFECTIVE:
                        boo.Play();
                        score = -1;
                        break;
                    default:
                        break;
                }
            }
            
            choiceManager.SwitchSpeaker(score);
        }
    }

}
