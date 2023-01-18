using UnityEngine;

namespace Team02
{
    [CreateAssetMenu(fileName = "RapBattles", menuName = "Team02/New RapBattles SO")]
    public class RapBattlesSO : ScriptableObject
    {
        public RapBattle[] rapBattles;

        /// <summary>
        /// This imports the Fights CSV as new and destroys the current data.
        /// ONLY USE THIS IF YOU DON'T CARE ABOUT LOOSING THIS SO's DATA.
        /// </summary>
        public void HardImportCSV()
        {
            string[][] _importedStrings = (new CSV()).ToStrings();
        }
    }
}
