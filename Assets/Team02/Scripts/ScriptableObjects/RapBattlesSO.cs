using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System;

namespace Team02
{
    [CreateAssetMenu(fileName = "RapBattles", menuName = "Team02/New RapBattles SO")]
    public class RapBattlesSO : ScriptableObject
    {
        public RapBattle[] rapBattles;
        private string[] languages;
        private const int NB_OF_PICKABLE_LINES = 3;
        private const int LANGUAGE_LENGTH = NB_OF_PICKABLE_LINES + 2;
        private const int FIGHT_BATTLE_ID_INDEX = 6;
        private const int FIGHT_DLG_ID_INDEX = 9;

        public string[] GetLanguages => languages;

        /// <summary>
        /// This imports the Fights CSV as new and destroys the current data.
        /// ONLY USE THIS IF YOU DON'T CARE ABOUT LOOSING THIS SO's DATA.
        /// </summary>
        public void HardImportCSV()
        {
            CSV csv = new CSV();
            languages = csv.GetLanguages();
            string[,] importedStrings = csv.ToStrings();
            string highestIDSstring = importedStrings[importedStrings.GetLength(0) - 1, 0].Substring(FIGHT_BATTLE_ID_INDEX, 2);
            int highestID = int.Parse(highestIDSstring);

            Debug.Log("CSV : " + languages.Length + " languages found in the csv.");
            Debug.Log("CSV : " + highestID + " rap battles found in the csv.");

            //rapBattles = new RapBattle[highestID];
            //for(int rapBattlesNb = 0; rapBattlesNb < highestID; rapBattlesNb++)
            //{
            //    string[] fightDialogues = Array.FindLast<string>(importedStrings, )
            //
            //
            //    for (int fightDialoguesNb = ; fightDialoguesNb < ; fightDialoguesNb++)
            //    {
            //
            //    }
            //}
        }
    }
}
