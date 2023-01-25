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

        public string[] GetLanguages => languages;
        public List<RapBattle> GetRapBattles => rapBattles;
        public List<RapBattle> SetRapBattles { set => rapBattles = value; }

        /// <summary> Return the requested line. Leave lineIndex null if you want the enemy line to be returned. </summary>
        public FightLine GetLine(int battleIndex, int dialogueIndex, int? lineIndex = null)
        {
            if(rapBattles == null || rapBattles.Count == 0)
            {
                Debug.LogError($"rapBattles is either null or empty.");
                return null;
            }

            if(battleIndex < 0 || battleIndex >= rapBattles.Count)
            {
                Debug.LogError($"battleIndex out of range. (battleIndex = {battleIndex}, rapBattles.Count = {rapBattles.Count}.)");
                return null;
            }

            if (rapBattles[battleIndex].GetFightDialogs == null || rapBattles[battleIndex].GetFightDialogs.Count == 0)
            {
                Debug.LogError($"_fightDialogs of {rapBattles[battleIndex].GetID} is either null or empty.");
                return null;
            }

            FightDlg[] fightDialogs = (rapBattles[battleIndex].GetFightDialogs).ToArray();


            if (dialogueIndex < 0 || dialogueIndex >= fightDialogs.Length)
            {
                Debug.LogError($"dialogueIndex out of range. (dialogueIndex = {battleIndex}, _fightDialogs.Count = {fightDialogs.Length}.)");
                return null;
            }

            if(lineIndex == null)
            {
                if(fightDialogs[dialogueIndex].GetEnemyLine == null)
                {
                    Debug.LogError($"{fightDialogs[dialogueIndex].GetID} has no enemy text.");
                    return null;
                }

                return fightDialogs[dialogueIndex].GetEnemyLine;
            }

            if (fightDialogs[dialogueIndex].GetPlayerLines == null || fightDialogs[dialogueIndex].GetPlayerLines.Count == 0)
            {
                Debug.LogError($"_playerLines of {fightDialogs[dialogueIndex].GetID} is either null or empty.");
                return null;
            }

            FightLine[] playerLines = (fightDialogs[dialogueIndex].GetPlayerLines).ToArray();

            if (lineIndex < 0 || lineIndex >= playerLines.Length)
            {
                Debug.LogError($"lineIndex out of range. (lineIndex = {lineIndex}, _playerLines.Count = {playerLines.Length}.)");
                return null;
            }

            return playerLines[lineIndex.Value];
        }
        public FightLine GetLine(string lineID)
        {
            int battleIndex = int.Parse(lineID.Substring(FIGHT_BATTLE_ID_INDEX, 2)) - 1;
            int dlgIndex = int.Parse(lineID.Substring(FIGHT_BATTLE_ID_INDEX + 3, 2)) - 1;

            if(lineID.Substring(FIGHT_BATTLE_ID_INDEX + 6, 6) == "PLAYER")
            {
                int lineIndex = int.Parse(lineID.Substring(19, 2));
                return GetLine(battleIndex, dlgIndex, lineIndex);
            }

            return GetLine(battleIndex, dlgIndex);
        }

        /// <summary>
        /// This imports the Fights CSV as new and destroys the current data.
        /// ONLY USE THIS IF YOU DON'T CARE ABOUT LOOSING THIS SO's DATA.
        /// </summary>
        public void HardImportCSV()
        {
            FightCSV csv = new();
            languages = csv.GetLanguages();
            string[,] importedStrings = csv.ToStrings();
            string rapBattlesNbSstring = importedStrings[importedStrings.GetLength(0) - 1, 0].Substring(FIGHT_BATTLE_ID_INDEX, 2);
            int rapBattlesNb = int.Parse(rapBattlesNbSstring);

            Debug.Log("FightCSV : " + languages.Length + " languages found in the csv.");
            Debug.Log("FightCSV : " + rapBattlesNb + " rap battles found in the csv.");

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

        public void SoftImportCSV()
        {
            FightCSV csv = new();
            string[,] importedStrings = csv.ToStrings();

            for (int i = 0; i < importedStrings.GetLength(0); i++)
            {
                for(int j = 1; j <= NB_OF_PICKABLE_LINES; j++)
                {
                    GetLine(importedStrings[i, 0] + "_PLAYER_" + (j - 1).ToString("00")).UpdateLine(importedStrings[i, j]);
                }
                GetLine(importedStrings[i, 0] + "_OPPONENT").UpdateLine(importedStrings[i, NB_OF_PICKABLE_LINES + 1]);
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
            FightCSV csv = new();
            string[,] importedStrings = csv.ToStrings();

            foreach (var battle in rapBattles)
            {
                foreach(var dlg in battle.GetFightDialogs)
                {
                    foreach(var line in dlg.GetPlayerLines)
                    {
                        line.UpdateLine(importedStrings[line.Position.x, line.Position.y + LANGUAGE_LENGTH * lang]);
                    }

                    dlg.GetEnemyLine.UpdateLine(importedStrings[dlg.GetEnemyLine.Position.x, dlg.GetEnemyLine.Position.y + LANGUAGE_LENGTH * lang]);
                }
            }
        }
    }
}
