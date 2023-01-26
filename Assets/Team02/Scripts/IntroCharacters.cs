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

        private CHARACTER currentCharacter;
        private CharacterData currentCharacterData;

        /*Enemy -> Player -> Enemy
            -> Wanted (affiche) -> Wanted (descriptif)
            > Ecran de VS*/

        private void Start()
        {
            if (choiceManager == null)
            {
                choiceManager = FindObjectOfType<ChoiceManager>();
            }

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

            if (choiceManager.GetCurrentScore > 0 || currentFight <= 0)
            {
                currentFight++; 
            }

            currentCharacter = dialoguesSO.dialogues[currentFight - 1].lines[currentStageIntro].character;

            currentCharacterData = choiceManager.GetCharacterData.GetCharacterData((int)currentCharacter);

            choiceManager.SetCharacterSprite(currentCharacter, SPRITE_POSE.IDLE, choiceManager.enemyVisual);

            AdvancedIntro();
        }

        public void AdvancedIntro()
        {
            // Increase stage when click

            currentStageIntro++;

            FightLine firstSpeaker = choiceManager.GetCurrentFightDlg.GetEnemyLine;

            DialogueLine dialogIntro;

            switch (currentStageIntro)
            {
                case 1: // Enemy intro line 1
                case 2: // Player intro line 1
                case 3: // Enemy intro line 2
                    dialogIntro = dialoguesSO.dialogues[currentFight - 1].lines[currentStageIntro - 1];

                    nameSpeaker.text = dialogIntro.character.ToString();
                    lineSpeaker.text = dialogIntro.text;
                    break;
                case 4: // WANTED_FRONT
                    introPanels.SetActive(true);

                    if (currentCharacterData.GetSprite(SPRITE_POSE.WANTED_FRONT) != null)
                    {
                        introPanelsImg.sprite = currentCharacterData.GetSprite(SPRITE_POSE.WANTED_FRONT);
                    }
                    else
                    {
                        Debug.LogWarning($"{currentCharacter} doesn't have a SPRITE_POSE.WANTED_FRONT");
                    }
                    break;
                case 5: // WANTED_BACK
                    if (currentCharacterData.GetSprite(SPRITE_POSE.WANTED_BACK) != null)
                    {
                        introPanelsImg.sprite = currentCharacterData.GetSprite(SPRITE_POSE.WANTED_BACK);
                    }
                    else
                    {
                        Debug.LogWarning($"{currentCharacter} doesn't have a SPRITE_POSE.WANTED_BACK");
                    }
                    break;
                case 6: // Ecran de VS
                    if (currentCharacterData.GetSprite(SPRITE_POSE.VS) != null)
                    {
                        introPanelsImg.sprite = currentCharacterData.GetSprite(SPRITE_POSE.VS);
                    }
                    else
                    {
                        Debug.LogWarning($"{currentCharacter} doesn't have a SPRITE_POSE.VS");
                    }
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

