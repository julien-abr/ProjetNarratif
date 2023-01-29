using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Team02
{
    public class ChoiceManager : MonoBehaviour
    {
        #region References
        [Header("Scripts & Data References")]
        [SerializeField] private RapBattlesSO rapBattlesSO;
        private List<RapBattle> RapBattles;
        [SerializeField] private CharacterDataSO characterDataSO;
        public CharacterDataSO GetCharacterData => characterDataSO;
        [SerializeField] private CrowdController crowdController;
        [SerializeField] private DialoguesSO dialoguesSO;

        [Header("Player References")]
        public Image playerVisual;
        [SerializeField] private GameObject TextBox_Player;
        public void TextBox_Player_Disabled() { TextBox_Player.SetActive(false); }
        [SerializeField] private GameObject finalChoice;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private List<Choices> choicesPlayer = new List<Choices>();

        [Header("Enemy References")]
        [SerializeField] private Text nameEnemy;
        [SerializeField] private GameObject TextBox_Enemy;
        [SerializeField] private Choices choiceEnemy;
        public Image enemyVisual;
        [SerializeField] private Image finalChoiceEnemyImg;
        [SerializeField] private List<Sprite> allEnemySprites = new List<Sprite>();
        public List<Sprite> GetAllEnemySprites => allEnemySprites;


        [SerializeField] private GameObject finishDialog;
        [SerializeField] private Image imgFinish;
        [SerializeField] private Text speakerNameFinish;
        [SerializeField] private Text speakerTextFinish;

        [Header("Musics")]
        public AudioSource ambient;
        public AudioSource fight;

        #endregion

        #region Variables 

        private CHARACTER currentCharacter;
        private CharacterData currentCharacterData;

        private float currentScore;
        public float GetCurrentScore => currentScore;
        private void SetCurrentScore(float score)
        {
            if (score == 0)
            {
                // reset 
                currentScore = 0;
                crowdController.ResetData(numberOfFightsInBattle);
            }
            else
            {
                currentScore += score;
                crowdController.MoveCrowd(score);
            }
        }

        private int RAP_BATTLE_STAGE = 1; // Doesn't start From 0 to represente fight 1-2-3 so use RapBattleStage below
        private int RapBattleStage 
        { 
            get => RAP_BATTLE_STAGE - 1; // -1 for list 
            set {RAP_BATTLE_STAGE = value; } 
        } 

        private int FIGHT_DLG_STAGE = 1; // Is set to 1 in Start
        public int FightDlgStage
        {
            get => FIGHT_DLG_STAGE;
            set { FIGHT_DLG_STAGE = value; }
        }

        public List<FightDlg> CurrentFightDialogs 
        {
            get => RapBattles[RapBattleStage]?.GetFightDialogs;
            set { CurrentFightDialogs = value; }
        }

        private int numberOfFightsInBattle;

        private FightDlg currentFightDlg;
        public FightDlg GetCurrentFightDlg => currentFightDlg;

        private bool END;

        public event Action onfightDlgStageChanged;
        public void EndCurrentFightDlg()
        {
            if (onfightDlgStageChanged != null)
                onfightDlgStageChanged();
        }
        #endregion

        private void Awake()
        {
            if (TextBox_Player == null) TextBox_Player = this.gameObject.transform.GetChild(0).gameObject; 
            if (TextBox_Enemy == null) TextBox_Enemy = this.gameObject.transform.GetChild(1).gameObject;

            RapBattles = rapBattlesSO?.GetRapBattles;

            currentFightDlg = CurrentFightDialogs[FightDlgStage - 1];
        }

        private void Start()
        {
            if (rapBattlesSO == null)
            {
                Debug.LogError("Need rapBattlesSO in the ChoiceManager !");
            }

            TextBox_Player.SetActive(false);
            TextBox_Enemy.SetActive(false);
            finalChoice.SetActive(false);

            onfightDlgStageChanged += () =>
            {
                TextBox_Player.SetActive(false);
                finalChoice.SetActive(false);
                finishDialog.SetActive(false);
                SetPlayerSprite(SPRITE_POSE.IDLE);
            };

            SetPlayerSprite(SPRITE_POSE.IDLE);
            ambient.Play();
        }

        public void StartFight()
        {
            TextBox_Player.SetActive(true);

            SetCurrentScore(0);

            currentFightDlg = CurrentFightDialogs[FightDlgStage - 1];

            SetLinePlayer(currentFightDlg);

            SetEnemySprite(SPRITE_POSE.IDLE);

            numberOfFightsInBattle = CurrentFightDialogs.Count - 1;

            crowdController.ResetData(numberOfFightsInBattle + 1);

            ambient.Stop();
            fight.Play();
        }

        private void RestartCurrentFight()
        {
            FightDlgStage = 1;

            currentFightDlg = CurrentFightDialogs[FightDlgStage - 1];
            SetLinePlayer(currentFightDlg);
            EndCurrentFightDlg();
            SetCurrentScore(0);

            crowdController.ResetData(numberOfFightsInBattle);

            fight.Stop();
            ambient.Play();
        }

        private void GoNextFightStage()
        {
            int nextfightDlgStage = FIGHT_DLG_STAGE + 1;
            bool fightLeft = nextfightDlgStage <= numberOfFightsInBattle;

            if (fightLeft)
            {
                FightDlgStage++;
                return;
            }
            else if (currentScore < 0)
            {
                // Lose, Restart fight

                RestartCurrentFight();
                return;
            }

            bool rapBattleLeft = RAP_BATTLE_STAGE < rapBattlesSO.GetRapBattles.Count;

            if (fightLeft == false && rapBattleLeft)
            {
                // WIN ! Go next BATTLE Stage

                finalChoice.SetActive(true);
                finalChoiceEnemyImg.sprite = allEnemySprites[RapBattleStage];

                SetEnemySprite(SPRITE_POSE.HURT);
            }
            if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= numberOfFightsInBattle)
            {
                finalChoice.SetActive(true);
                finalChoiceEnemyImg.sprite = allEnemySprites[RapBattleStage];
            }
        }

        bool wasFinish;

        // No difference between finish or not

        public void DisplayFinishDialog(bool isFinish)
        {
            wasFinish = isFinish;
            finishDialog.SetActive(true);

            speakerNameFinish.text = CurrentFightDialogs[numberOfFightsInBattle].GetPlayerLines[0].Character.ToString();
            imgFinish.sprite = playerVisual.sprite;

            if (isFinish)
            {
                speakerTextFinish.text = CurrentFightDialogs[numberOfFightsInBattle].GetPlayerLines[0].GetText;
            }
            else
            {
                speakerTextFinish.text = CurrentFightDialogs[numberOfFightsInBattle].GetPlayerLines[1].GetText;
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("MainMenu");
        }

        int stageFinish = 0;
        int offset;

        public void AdvanceFinish()
        {
            if (wasFinish)
            {
                offset = 0;
                //imgFinish.sprite = characterDataSO.GetCharacterData(RAP_BATTLE_STAGE).GetSprite(SPRITE_POSE.IDLE);
                speakerNameFinish.text = dialoguesSO.dialogues[RAP_BATTLE_STAGE - 1].lines[stageFinish + offset].character.ToString();
                speakerTextFinish.text = dialoguesSO.dialogues[RAP_BATTLE_STAGE - 1].lines[stageFinish + offset].text;

                if (stageFinish >= 1)
                {
                    imgFinish.sprite = enemyVisual.sprite;
                    Debug.Log("dzqd");
                }

                if (stageFinish > 1)
                {
                    stageFinish = 0;
                    ChangeBattle();
                }
            }
            else
            {
                offset = 2;
                speakerNameFinish.text = dialoguesSO.dialogues[RAP_BATTLE_STAGE - 1].lines[stageFinish + offset].character.ToString();
                speakerTextFinish.text = dialoguesSO.dialogues[RAP_BATTLE_STAGE - 1].lines[stageFinish + offset].text;

                if (stageFinish + offset >= (stageFinish + offset + 1))
                {
                    imgFinish.sprite = enemyVisual.sprite;
                    Debug.Log("dzqd");
                }

                if (stageFinish > 3 )
                {
                    stageFinish = 0;
                    ChangeBattle();
                }
            }

            stageFinish++;
        }

        public void ChangeBattle() // For finish button 
        {
            if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= numberOfFightsInBattle)
            {
                // WIN !! End of the narrative game

                END = true;
                endPanel.SetActive(true);
                Debug.Log("End");
                TextBox_Enemy.SetActive(false);
                TextBox_Player.SetActive(false);
                return;
            }

            RAP_BATTLE_STAGE++;

            enemyVisual.sprite = allEnemySprites[RapBattleStage];

            SetEnemySprite(SPRITE_POSE.IDLE);

            FightDlgStage = 1;

            EndCurrentFightDlg();

            fight.Stop();
            ambient.Play();
        }

        private void SetLinePlayer(FightDlg _fightDlg)
        {
            for (int i = 0; i < choicesPlayer.Count; i++)
            {
                choicesPlayer[i].effectiveness = _fightDlg.GetPlayerLines[i].DamageType;
                choicesPlayer[i].idChoice = _fightDlg.GetPlayerLines[i].GetID;
                choicesPlayer[i].UpdateTextFight(_fightDlg.GetPlayerLines[i].GetText);
            }

            SetPlayerSprite(SPRITE_POSE.ATTACK);
            SetEnemySprite(SPRITE_POSE.HURT);
        }

        private void SetLineEnemy(FightDlg _fightDlg)
        {
            choiceEnemy.UpdateTextFight(_fightDlg.GetEnemyLine.GetText);
            SetEnemySprite(SPRITE_POSE.ATTACK);
            SetPlayerSprite(SPRITE_POSE.HURT);
        }
        private void SetPlayerSprite(SPRITE_POSE spritePose)
        {
            var player = currentFightDlg.GetPlayerLines[0].Character;
            var playerData = GetCharacterData.GetCharacterData((int)player);
            var spriteToChange = playerVisual;

            if (playerData.GetSprite(spritePose) != null)
            {
                spriteToChange.sprite = playerData.GetSprite(spritePose);
            }
            else
            {
                Debug.LogWarning($"{playerData} doesn't have a SPRITE_POSE.{spritePose}");
            }
        }
        public void SetEnemySprite(SPRITE_POSE spritePose)
        {
            var enemy = currentFightDlg.GetEnemyLine.Character;
            var enemyData = GetCharacterData.GetCharacterData((int)enemy);
            var spriteToChange = enemyVisual;

            if (enemyData.GetSprite(spritePose) != null)
            {
                spriteToChange.sprite = enemyData.GetSprite(spritePose);
            }
            else
            {
                Debug.LogWarning($"{enemy} doesn't have a SPRITE_POSE.{spritePose}");
            }
        }

        public void SetCharacterSprite(CHARACTER character, SPRITE_POSE spritePose, Image spriteToChange )
        {
            // Simpler with SetEnemySprite and SetPlayerSprite
            var characterData = GetCharacterData.GetCharacterData((int)character);

            if (characterData.GetSprite(spritePose) != null)
            {
                spriteToChange.sprite = characterData.GetSprite(spritePose);
            }
            else
            {
                Debug.LogWarning($"{character} doesn't have a SPRITE_POSE.{spritePose}");
            }
        }

        public void SwitchSpeaker(float score)
        {
            if (END)
            {
                return;
            }

            SetCurrentScore(score);

            if (TextBox_Player.activeSelf) // When clicked on Player line (Enemy is clashing)
            {
                // Switch to enemy line (doesn't change fight)
                TextBox_Player.SetActive(false);
                TextBox_Enemy.SetActive(true);

                currentCharacter = currentFightDlg.GetEnemyLine.Character;
                currentCharacterData = GetCharacterData.GetCharacterData((int)currentCharacter);

                SetLineEnemy(currentFightDlg);
                nameEnemy.text = currentFightDlg.GetEnemyLine.Character.ToString();
            }
            else if(TextBox_Enemy.activeSelf) // When clicked on Enemy line (Player is clashing)
            {
                currentFightDlg = CurrentFightDialogs[FightDlgStage];

                // Switch back to the player lines (change fight)
                TextBox_Enemy.SetActive(false);
                TextBox_Player.SetActive(true);

                currentCharacter = currentFightDlg.GetPlayerLines[0].Character;
                currentCharacterData = GetCharacterData.GetCharacterData((int)currentCharacter);

                if (FightDlgStage >= numberOfFightsInBattle)
                {
                    GoNextFightStage();
                    return;
                }

                SetLinePlayer(currentFightDlg);
                GoNextFightStage();
            }
        }
    }
}