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

        List<RapBattle> RapBattles;

        [SerializeField] private Text nameEnemy;
        [SerializeField] private GameObject TextBox_Player;
        [SerializeField] private GameObject TextBox_Enemy;

        [SerializeField] private List<Choices> choicesPlayer = new List<Choices>();

        [SerializeField] private Choices choiceEnemy;

        private int RAP_BATTLE_STAGE = 1; // Doesn't start From 0 to represente fight 1-2-3 so use RapBattleStage below
        private int RapBattleStage { get => RAP_BATTLE_STAGE - 1; set { RAP_BATTLE_STAGE = value; } } // -1 for list 

        private bool END;

        private int FIGHT_DLG_STAGE = 1; // Is set to 1 in Start
        public int FightDlgStage
        {
            get => FIGHT_DLG_STAGE;
            set
            {
                FIGHT_DLG_STAGE = value;
                UpdateFightDlg();
            }
        }

        public event Action onfightDlgStageChanged;
        public void UpdateFightDlg()
        {
            if (onfightDlgStageChanged != null)
                onfightDlgStageChanged();
        }

        private void Awake()
        {
            if (TextBox_Player == null)
            {
                TextBox_Player = this.gameObject.transform.GetChild(0).gameObject;
            }
            if (TextBox_Enemy == null)
            {
                TextBox_Enemy = this.gameObject.transform.GetChild(1).gameObject;
            }

            /*// Get the choice of the Player and the Enemy via their child 
            choicesPlayer.Clear();

            for (int i = 0; i < TextBox_Player.gameObject.transform.childCount; i++)
            {
                choicesPlayer.Add(TextBox_Player.gameObject.transform.GetChild(i).GetComponent<Choices>());
            }*/

            //choiceEnemy = TextBox_Enemy.gameObject.transform.GetChild(0).GetComponent<Choices>();
        }

        private void Start()
        {
            if (rapBattlesSO == null)
            {
                Debug.LogError("Need rapBattlesSO in the ChoiceManager !");
            }

            TextBox_Player.SetActive(true);
            TextBox_Enemy.SetActive(false);

            RapBattles = rapBattlesSO?.GetRapBattles;

            var fightDlg = RapBattles[RapBattleStage]?.GetFightDialogs;

            SetLinePlayer(fightDlg[FightDlgStage - 1]);

            onfightDlgStageChanged += () =>
            {
                //SwitchSpeaker();
                //var fightDlg = RapBattles[RapBattleStage]?.GetFightDialogs;

                /*SetLinePlayer(fightDlg[FightDlgStage - 1]);
                SetLineEnemy(fightDlg[FightDlgStage - 1]);*/
            };

            //FightDlgStage = 1;
        }

        public void GoNextFightStage()
        {
            int nextfightDlgStage = FIGHT_DLG_STAGE + 1;

            if (nextfightDlgStage > rapBattlesSO.GetRapBattles[RapBattleStage].GetFightDialogs.Count
                && RAP_BATTLE_STAGE < rapBattlesSO.GetRapBattles.Count)
            {
                // Go next BATTLE Stage
                RAP_BATTLE_STAGE++;
                Debug.Log("Change Enemy");
                FightDlgStage = 1;
            }
            else if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= 3)
            {
                END = true;
                Debug.Log("End");
                return;
            }
            else
            {
                FightDlgStage++;
            }
        }


        void SetLinePlayer(FightDlg _fightDlg)
        {
            for (int i = 0; i < choicesPlayer.Count; i++)
            {
                choicesPlayer[i].effectiveness = _fightDlg.GetPlayerLines[i].DamageType;
                choicesPlayer[i].idChoice = _fightDlg.GetPlayerLines[i].GetID;
                choicesPlayer[i].UpdateTextFight(_fightDlg.GetPlayerLines[i].GetText);
            }
        }
        void SetLineEnemy(FightDlg _fightDlg)
        {
            choiceEnemy.UpdateTextFight(_fightDlg.GetEnemyLine.GetText);
        }

        public void SwitchSpeaker()
        {
            if (END)
            {
                return;
            }
                
            var fightsDlg = RapBattles[RapBattleStage]?.GetFightDialogs;
            var fightDlg = fightsDlg[FightDlgStage - 1];

            TextBox_Player.SetActive(!TextBox_Player.activeSelf);
            TextBox_Enemy.SetActive(!TextBox_Enemy.activeSelf);

            if (TextBox_Enemy.activeSelf)
            {
                choiceEnemy.UpdateTextFight(fightDlg.GetEnemyLine.GetText);
                nameEnemy.text = fightDlg.GetEnemyLine.GetID;
            }
            else if (TextBox_Player.activeSelf)
            {
                GoNextFightStage();

                for (int i = 0; i < choicesPlayer.Count; i++)
                {
                    choicesPlayer[i].effectiveness = fightDlg.GetPlayerLines[i].DamageType;
                    choicesPlayer[i].idChoice = fightDlg.GetPlayerLines[i].GetID;
                    choicesPlayer[i].UpdateTextFight(fightDlg.GetPlayerLines[i].GetText);
                }
            }
        }
    }
}