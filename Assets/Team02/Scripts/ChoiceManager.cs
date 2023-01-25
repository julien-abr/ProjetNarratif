using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team02
{
    public class ChoiceManager : MonoBehaviour
    {
        [SerializeField]
        private RapBattlesSO rapBattlesSO;

        private List<RapBattle> RapBattles;

        [SerializeField] private Text nameEnemy;
        [SerializeField] private GameObject TextBox_Player;
        public void TextBox_Player_Disabled() { TextBox_Player.SetActive(false); }

        [SerializeField] private GameObject TextBox_Enemy;

        public Image enemyVisual;
        [SerializeField] private List<Sprite> allEnemySprites = new List<Sprite>();
        public List<Sprite> AllEnemySprites => allEnemySprites;

        [SerializeField] private List<Choices> choicesPlayer = new List<Choices>();

        [SerializeField] private Choices choiceEnemy;

        private float currentScore;
        public float CurrentScore => currentScore;

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

        private FightDlg currentFightDlg;
        public FightDlg CurrentFightDlg => currentFightDlg;

        private bool END;

        public event Action onfightDlgStageChanged;
        public void EndCurrentFightDlg()
        {
            if (onfightDlgStageChanged != null)
                onfightDlgStageChanged();
        }

        private void Awake()
        {
            if (TextBox_Player == null) TextBox_Player = this.gameObject.transform.GetChild(0).gameObject; 
            if (TextBox_Enemy == null) TextBox_Enemy = this.gameObject.transform.GetChild(1).gameObject;

            RapBattles = rapBattlesSO?.GetRapBattles;

            enemyVisual.sprite = allEnemySprites[0];

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

            onfightDlgStageChanged += () =>
            {
                TextBox_Player.SetActive(false);
            };
        }

        public void StartFight()
        {
            TextBox_Player.SetActive(true);
            currentScore = 0;

            currentFightDlg = CurrentFightDialogs[FightDlgStage - 1];

            SetLinePlayer(currentFightDlg);
        }

        private void RestartCurrentFight()
        {
            //TextBox_Player.SetActive(true);

            FightDlgStage = 1;

            currentFightDlg = CurrentFightDialogs[FightDlgStage - 1];

            SetLinePlayer(currentFightDlg);

            EndCurrentFightDlg();
            currentScore = 0;
        }

        private void GoNextFightStage()
        {
            int nextfightDlgStage = FIGHT_DLG_STAGE + 1;

            if (nextfightDlgStage > CurrentFightDialogs.Count - 1 // - 1 because the last in the list are kill dialog
                && RAP_BATTLE_STAGE < rapBattlesSO.GetRapBattles.Count)
            {
                if (currentScore <= 0)
                {
                    // Lose, Restart fight

                    RestartCurrentFight();
                }
                else
                {
                    // WIN ! Go next BATTLE Stage
                    RAP_BATTLE_STAGE++;

                    enemyVisual.sprite = allEnemySprites[RapBattleStage];

                    FightDlgStage = 1;

                    // before end do the epargner here

                    EndCurrentFightDlg();
                }
            }
            else if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= (CurrentFightDialogs.Count - 1))
            {
                END = true;
                Debug.Log("End");
                TextBox_Enemy.SetActive(false);
                TextBox_Player.SetActive(false);
                return;
            }
            else
            {
                FightDlgStage++;
            }
        }


        private void SetLinePlayer(FightDlg _fightDlg)
        {
            for (int i = 0; i < choicesPlayer.Count; i++)
            {
                choicesPlayer[i].effectiveness = _fightDlg.GetPlayerLines[i].DamageType;
                choicesPlayer[i].idChoice = _fightDlg.GetPlayerLines[i].GetID;
                choicesPlayer[i].UpdateTextFight(_fightDlg.GetPlayerLines[i].GetText);
            }
        }
        private void SetLineEnemy(FightDlg _fightDlg)
        {
            choiceEnemy.UpdateTextFight(_fightDlg.GetEnemyLine.GetText);
        }

        public void SwitchSpeaker(float score)
        {
            if (END)
            {
                return;
            }

            Debug.Log($"currentScore : {currentScore} += score({score})");

            currentScore += score;

            if (TextBox_Player.activeSelf) // When clicked on Player line
            {
                
                // Switch to enemy line (doesn't change fight)
                TextBox_Player.SetActive(false);
                TextBox_Enemy.SetActive(true);
                choiceEnemy.UpdateTextFight(currentFightDlg.GetEnemyLine.GetText);
                nameEnemy.text = currentFightDlg.GetEnemyLine.GetID;
            }
            else if(TextBox_Enemy.activeSelf) // When clicked on Enemy line
            {
                currentFightDlg = CurrentFightDialogs[FightDlgStage];

                // Switch back to the player lines (change fight)
                TextBox_Enemy.SetActive(false);
                TextBox_Player.SetActive(true);

                if (FightDlgStage >= CurrentFightDialogs.Count - 1)
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