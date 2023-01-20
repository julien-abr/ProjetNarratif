using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Team02
{
    public class RapBattlesSO : ScriptableObject
    {
        [SerializeField, HideInInspector]
        private List<RapBattle> rapBattles;
        [SerializeField, HideInInspector]
        private string[] languages;
        private const int NB_OF_PICKABLE_LINES = 3;
        private const int LANGUAGE_LENGTH = NB_OF_PICKABLE_LINES + 2;
        private const int FIGHT_BATTLE_ID_INDEX = 6;
        private const int FIGHT_DLG_ID_INDEX = 9;

        public string[] GetLanguages => languages;
        public List<RapBattle> GetRapBattles => rapBattles;
        public List<RapBattle> SetRapBattles { set => rapBattles = value; }

        /// <summary>
        /// This imports the Fights CSV as new and destroys the current data.
        /// ONLY USE THIS IF YOU DON'T CARE ABOUT LOOSING THIS SO's DATA.
        /// </summary>
        public void HardImportCSV()
        {
            CSV csv = new();
            languages = csv.GetLanguages();
            string[,] importedStrings = csv.ToStrings();
            string rapBattlesNbSstring = importedStrings[importedStrings.GetLength(0) - 1, 0].Substring(FIGHT_BATTLE_ID_INDEX, 2);
            int rapBattlesNb = int.Parse(rapBattlesNbSstring);

            Debug.Log("CSV : " + languages.Length + " languages found in the csv.");
            Debug.Log("CSV : " + rapBattlesNb + " rap battles found in the csv.");

            rapBattles = new();
            int currentBattle = 1;
            rapBattles.Add(new(importedStrings[0, 0][..8], new()));
            for (int i = 0; i < importedStrings.GetLength(0); i++)
            {
                if(int.Parse(importedStrings[i, 0].Substring(FIGHT_BATTLE_ID_INDEX, 2)) == currentBattle)
                {
                    FightDlg fightDlg = new(importedStrings[i, 0], new(), new(importedStrings[i, 0] + "_OPPONENT", ""));

                    for(int j = 1; j <= NB_OF_PICKABLE_LINES; j++)
                    {
                        FightLine newLine = new(importedStrings[i, 0] + "_PLAYER_" + j.ToString("00"), importedStrings[i, j]);
                        newLine.Position = new(i, j);
                        fightDlg.GetPlayerLines.Add(newLine);
                    }

                    fightDlg.GetEnemyLine.UpdateLine(importedStrings[i, NB_OF_PICKABLE_LINES + 1]);
                    fightDlg.GetEnemyLine.Position = new(i, NB_OF_PICKABLE_LINES + 1);

                    rapBattles[currentBattle - 1].GetFightDialogs.Add(fightDlg);
                }
                else
                {
                    currentBattle++;
                    rapBattles.Add(new(importedStrings[i, 0][..8], new()));
                    i--;
                }
            }
        }

        public void DebugLog()
        {
            Debug.Log("There are " + rapBattles.Count + " rap battles in the list.");
            foreach (var battle in rapBattles)
            {
                Debug.LogError("There are " + battle.GetFightDialogs.Count + " fight dlgs in " + battle.GetID);
                foreach (var dlg in battle.GetFightDialogs)
                {
                    Debug.LogWarning("There are " + dlg.GetPlayerLines.Count + " player lines in " + dlg.GetID);

                    foreach (var line in dlg.GetPlayerLines)
                    {
                        Debug.Log(line.GetID + " || " + line.Position + " || " + line.GetText);
                    }

                    Debug.Log(dlg.GetEnemyLine.GetID + " || " + dlg.GetEnemyLine.Position + " || " + dlg.GetEnemyLine.GetText);
                }
            }
        }

        public void ChangeLanguage(int lang)
        {
            CSV csv = new();
            string[,] importedStrings = csv.ToStrings();

            foreach (var battle in rapBattles)
            {
                foreach(var dlg in battle.GetFightDialogs)
                {
                    foreach(var line in dlg.GetPlayerLines)
                    {
                        line.UpdateLine(importedStrings[line.Position.x, line.Position.y + (NB_OF_PICKABLE_LINES + 2) * lang]);
                    }

                    dlg.GetEnemyLine.UpdateLine(importedStrings[dlg.GetEnemyLine.Position.x, dlg.GetEnemyLine.Position.y + (NB_OF_PICKABLE_LINES + 2) * lang]);
                }
            }
        }
    }
}
