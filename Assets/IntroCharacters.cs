using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team02
{
    public class IntroCharacters : MonoBehaviour
    {
        [SerializeField]
        ChoiceManager choiceManager;

        [Header("Text Box Background Intro")]
        [SerializeField]
        private GameObject TextBoxBackgroundIntro;
        [SerializeField]
        private Text nameSpeaker;
        [SerializeField]
        private Text lineSpeaker;

        private const int stageIntro = 3 + 1 + 1 + 1;
        private int currentStageIntro = 0;

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
                    // Display name and dialogue enemy
                    /*nameSpeaker.text = firstSpeaker.GetID;
                    lineSpeaker.text = firstSpeaker.GetText;*/
                    nameSpeaker.text = "Enemy";
                    lineSpeaker.text = "First Dialog Enemy";
                    break;
                case 2: // Player intro line 1
                    nameSpeaker.text = "Player";
                    lineSpeaker.text = "First Dialog Player";
                    break;
                case 3: // Enemy intro line 2
                    nameSpeaker.text = "Enemy";
                    lineSpeaker.text = "First Dialog Enemy";
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

