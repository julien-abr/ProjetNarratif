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

        [SerializeField]
        private GameObject TextBox_Player;
        private List<Choices> choicesPlayer = new List<Choices>();
        [SerializeField]
        private GameObject TextBox_Enemy;
        private Choices choiceEnemy;

        private int RAP_BATTLE_STAGE = 1; // Doesn't start From 0 to represente fight 1-2-3 so use RapBattleStage below
        private int RapBattleStage { get => RAP_BATTLE_STAGE - 1; set { RAP_BATTLE_STAGE = value; } } // -1 for list 


        private int FIGHT_DLG_STAGE = 0; // Is set to 1 in Start
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

            // Get the choice of the Player and the Enemy via their child 
            choicesPlayer.Clear();

            for (int i = 0; i < TextBox_Player.gameObject.transform.childCount; i++)
            {
                choicesPlayer.Add(TextBox_Player.gameObject.transform.GetChild(i).GetComponent<Choices>());
            }

            choiceEnemy = TextBox_Enemy.gameObject.transform.GetChild(0).GetComponent<Choices>();
        }

        private void Start()
        {
            if (rapBattlesSO == null)
            {
                Debug.LogError("Need rapBattlesSO in the ChoiceManager !");
            }

            var RapBattles = rapBattlesSO?.GetRapBattles;

            onfightDlgStageChanged += () => 
            {
                var fightDlg = RapBattles[RapBattleStage]?.GetFightDialogs;
                
                SetLinePlayer(fightDlg[FightDlgStage - 1]);
                SetLineEnemy(fightDlg[FightDlgStage - 1]);
            };

            FightDlgStage = 1;
        }

        public void GoNextFightStage()
        {
            int nextfightDlgStage = FIGHT_DLG_STAGE + 1;

            if (nextfightDlgStage > rapBattlesSO.GetRapBattles[RapBattleStage].GetFightDialogs.Count
                && RAP_BATTLE_STAGE < rapBattlesSO.GetRapBattles.Count)
            {
                // Go next BATTLE Stage
                RAP_BATTLE_STAGE++;
                FightDlgStage = 1;
            }
            else if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= 3)
            {
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
                choicesPlayer[i].UpdateTextFight(_fightDlg.GetPlayerLines[i].GetText);
            }
        }
        void SetLineEnemy(FightDlg _fightDlg)
        {
            choiceEnemy.UpdateTextFight(_fightDlg.GetEnemyLine.GetText);
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
/*
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

        *//*private List<string> allLines = new List<string>() {
        "Tu me plaisais pas en m�me temps",
        "Ouais bas d�so",
        "T'as tu� ma grand m�re putain",
        "Sale chienne !",
        "C'est vraiment pas sympa"};*//*

        [SerializeField]
        private RapBattlesSO rapBattlesSO;

        private List<Choices> allChoices = new List<Choices>();

        [SerializeField]
        private GameObject TextBox_Player;
        [SerializeField]
        private GameObject TextBox_Enemy;

        private int RAP_BATTLE_STAGE = 1; // Doesn't start From 0 to represente fight 1-2-3
        *//*private int RapBattleStage 
        { 
            get => RAP_BATTLE_STAGE - 1; // -1 for list 
            set 
            { 
                RAP_BATTLE_STAGE = value; 
            } 
        }
*//*
        private int RapBattleStage { get => RAP_BATTLE_STAGE - 1; set { RAP_BATTLE_STAGE = value; } } // -1 for list 


        private int FIGHT_DLG_STAGE = 0; // Is set to 1 in Start
        public int FightDlgStage
        {
            get => FIGHT_DLG_STAGE - 1; // -1 for list 
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

        *//*[SerializeField, Range(1, 5)]
        private int numberOfChoices;*/

        /*private void OnValidate()
        {
            Debug.Log("Something changed");

            //UpdateAllChoices();
        }*//*

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
            var fightDlg = RapBattles[RapBattleStage]?.GetFightDialogs;


            onfightDlgStageChanged += () =>
            {
                SetLinePlayer(fightDlg[FightDlgStage]);
                SetLineEnemy(fightDlg[FightDlgStage]);
            };

            FightDlgStage = 1;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("GoNextStage");

                int nextfightDlgStage = FIGHT_DLG_STAGE + 1;

                if (nextfightDlgStage > rapBattlesSO.GetRapBattles[RapBattleStage].GetFightDialogs.Count
                    && RAP_BATTLE_STAGE < rapBattlesSO.GetRapBattles.Count)
                {
                    Debug.Log("1");

                    RAP_BATTLE_STAGE++;
                    FightDlgStage = 1;
                }
                else if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= 3)
                {
                    Debug.Log("End");
                    return;
                }
                else
                {
                    Debug.Log("1 ++ ");
                    FightDlgStage++;
                }
            }

        }

        public void GoNextStage()
        {
            Debug.Log("GoNextStage");

            int nextfightDlgStage = FIGHT_DLG_STAGE + 1;

            if (nextfightDlgStage > rapBattlesSO.GetRapBattles[RapBattleStage].GetFightDialogs.Count
                && RAP_BATTLE_STAGE < rapBattlesSO.GetRapBattles.Count)
            {
                Debug.Log("1");

                RAP_BATTLE_STAGE++;
                FightDlgStage = 1;
            }
            else if (RAP_BATTLE_STAGE >= rapBattlesSO.GetRapBattles.Count && FightDlgStage >= 3)
            {
                Debug.Log("End");
                return;
            }
            else
            {
                Debug.Log("1 ++ ");
                FightDlgStage++;
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

        *//*private void UpdateAllChoices()
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
        }*//*
    }
}

*/