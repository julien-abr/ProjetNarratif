using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team02
{
    public class IntroCharacters : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        ChoiceManager choiceManager;
        [SerializeField]
        private DialoguesSO dialoguesSO;

        [Header("Text Box Background Intro")]
        [SerializeField]
        private GameObject TextBoxBackgroundIntro;
        [SerializeField]
        private Text nameSpeaker;
        [SerializeField]
        private Text lineSpeaker;

        private const int stageIntro = 3 + 1 + 1 + 1;
        private int currentStageIntro = 0;
        private int currentFight; // Current opponent nbr

        [Header("Intro Panels")]
        [SerializeField]
        private GameObject introPanels;
        [SerializeField]
        private Image introPanelsImg;
        [SerializeField]
        private List<Sprite> allIntroPanel = new List<Sprite>();

        /*Enemy -> Player -> Enemy
            -> Wanted (affiche) -> Wanted (descriptif)
            > Ecran de VS*/

        private void Start()
        {
            if (choiceManager == null)
            {
                choiceManager = FindObjectOfType<ChoiceManager>();
            }

            choiceManager.enemyVisual.sprite = choiceManager.AllEnemySprites[0];

            introPanels.SetActive(false);
            TextBoxBackgroundIntro.SetActive(false); // need to be true

            choiceManager.onfightDlgStageChanged += () =>
            {
                RestartIntro();
            };

            RestartIntro();
        }

        public void RestartIntro()
        {
            TextBoxBackgroundIntro.SetActive(true);
            currentStageIntro = 0;
            choiceManager.TextBox_Player_Disabled();
            if (choiceManager.CurrentScore > 0 || currentFight <= 0)
            {
                currentFight++; 
            }

            AdvancedIntro();
        }

        public void AdvancedIntro()
        {
            // Increase stage when click

            currentStageIntro++;

            FightLine firstSpeaker = choiceManager.CurrentFightDlg.GetEnemyLine;

            switch (currentStageIntro)
            {
                case 1: // Enemy intro line 1
                case 2: // Player intro line 1
                case 3: // Enemy intro line 2
                    nameSpeaker.text = dialoguesSO.dialogues[currentFight - 1].lines[currentStageIntro - 1].character.ToString();
                    lineSpeaker.text = dialoguesSO.dialogues[currentFight - 1].lines[currentStageIntro - 1].text;
                    break;
                case 4: // Wanted (affiche)
                    introPanels.SetActive(true);
                    introPanelsImg.sprite = allIntroPanel[0];
                    break;
                case 5: // Wanted (descriptif)
                    introPanelsImg.sprite = allIntroPanel[1];
                    break;
                case 6: // Ecran de VS
                    introPanelsImg.sprite = allIntroPanel[2];
                    break;
                case 7:
                    introPanels.SetActive(false);
                    TextBoxBackgroundIntro.SetActive(false);
                    choiceManager.StartFight();
                    break;
                default:
                    break;
            }
        }
    }
}

