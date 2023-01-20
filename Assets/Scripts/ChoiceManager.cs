using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team02
{
    public class ChoiceManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabChoice;

        /*private List<string> allLines = new List<string>() {
        "Tu me plaisais pas en même temps",
        "Ouais bas déso",
        "T'as tué ma grand mère putain",
        "Sale chienne !",
        "C'est vraiment pas sympa"};*/

        [SerializeField]
        private RapBattlesSO rapBattlesSO;

        private List<Choices> allChoices = new List<Choices>();

        [SerializeField]
        private GameObject TextBox_Player;
        [SerializeField]
        private GameObject TextBox_Enemy;

        private int rapBattleStage = 1;

        private int fightDlgStage = 0;
        public int FightDlgStage
        {
            get => fightDlgStage;
            set
            {
                fightDlgStage = value;
                UpdateFightDlg();
            }
        }

        public event Action onfightDlgStageChanged;
        public void UpdateFightDlg()
        {
            if (onfightDlgStageChanged != null)
                onfightDlgStageChanged();
        }

        /*[SerializeField, Range(1, 5)]
        private int numberOfChoices;*/

        /*private void OnValidate()
        {
            Debug.Log("Something changed");

            //UpdateAllChoices();
        }*/

        private void Awake()
        {
            TextBox_Player = this.gameObject.transform.GetChild(0).gameObject;
            TextBox_Enemy = this.gameObject.transform.GetChild(1).gameObject;
        }

        private void Start()
        {
            if (rapBattlesSO == null)
            {
                Debug.LogError("Need rapBattlesSO in the ChoiceManager !");
            }

            var RapBattles = rapBattlesSO?.GetRapBattles;
            var fightDlg = RapBattles[rapBattleStage - 1]?.GetFightDialogs;


            onfightDlgStageChanged += () => 
            {
                SetLinePlayer(fightDlg[FightDlgStage - 1]);
                SetLineEnemy(fightDlg[FightDlgStage - 1]);
            };

            FightDlgStage = 1;
            /*for (int i = 0; i < numberOfChoices; i++)
            {
                GameObject line = Instantiate(prefabChoice, this.gameObject.transform);
                line.GetComponent<Choices>().Configure(fightDlg[0].GetPlayerLines[i].GetText);
            }*/
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                int currentfightDlgStage = fightDlgStage + 1;

                if (currentfightDlgStage > rapBattlesSO.GetRapBattles[rapBattleStage].GetFightDialogs.Count)
                {
                    rapBattleStage++;
                    FightDlgStage = 1;
                }
                else
                {
                    FightDlgStage++;
                }
            }
        }

        void SetLinePlayer(FightDlg _fightDlg)
        {
            for (int i = 0; i < TextBox_Player.gameObject.transform.childCount; i++)
            {
                TextBox_Player.gameObject.transform.GetChild(i).GetComponent<Text>().text = $"- {_fightDlg.GetPlayerLines[i].GetText}";
            }
        }
        void SetLineEnemy(FightDlg _fightDlg)
        {
            TextBox_Enemy.gameObject.transform.GetChild(0).GetComponent<Text>().text = $"- {_fightDlg.GetEnemyLine.GetText}";
        }

        /*private void UpdateAllChoices()
        {
            if (numberOfChoices != this.gameObject.transform.childCount)
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    if (Application.isEditor)
                        Object.DestroyImmediate(child.gameObject);
                    else
                        Object.Destroy(child.gameObject);
                }

                for (int i = 0; i < numberOfChoices; i++)
                {
                    GameObject line = Instantiate(prefabChoice, this.gameObject.transform);
                    line.GetComponent<Choices>().Configure(allLines[i]);
                }
            }
        }*/
    }
}

