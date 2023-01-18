using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team02
{
    enum LINETYPE
    {
        CRITICAL,
        NEUTRAL,
        BAD
    }

    class FightLine
    {
        private string id;
        private string textLine;
        LINETYPE damageType;
        float damage;
    }

    class FightDlg
    {
        public string id;
        FightLine[] playerLines;
        FightLine enemyLine;
    }

    class RapBattle
    {
        string id;
        FightDlg[] fightDialogs;
    }

    [CreateAssetMenu(fileName = "RapBattles", menuName = "Team02/New RapBattles SO")]
    class RapBattlesSO : ScriptableObject
    {

    }
}
