using UnityEngine;

namespace Team02
{
    public enum LINETYPE
    {
        CRITICAL,
        NEUTRAL,
        BAD
    }

    [System.Serializable]
    public class FightLine
    {
        public string name;
        private string _id;
        // TODO : privatise
        public string _textLine;
        private LINETYPE _damageType;
        private float _damage;

        public FightLine(string id, string textLine)
        {
            _id = id;
            _textLine = textLine;
            name = _textLine;
        }

        /// <summary>
        /// Updates the line text and its id if given.
        /// </summary>
        /// <param name="newLine">The new text line.</param>
        /// <param name="newID">The new id (optional).</param>
        public void UpdateLine(string newLine, string newID = null)
        {
            if (newID != null) _id = newID;
            if (newLine != null) _textLine = newLine;
            name = _textLine;
        }

        public string GetID => _id;
        public string GetText => _textLine;
        public LINETYPE DamageType { get { return _damageType; } set { _damageType = value; } }
        public float Damage { get { return _damage; } set { _damage = value; } }
    }

    [System.Serializable]
    public class FightDlg
    {
        public string name;
        private string _id;
        // TODO : privatise
        public FightLine[] _playerLines;
        // TODO : privatise
        public FightLine _enemyLine;

        public FightDlg(string id, FightLine[] playerLines, FightLine enemyLine)
        {
            _id = id;
            name = id;
            _playerLines = playerLines;
            _enemyLine = enemyLine;
        }

        public void UpdateID(string newID)
        {
            if (_id == null)
                return;

            _id = newID;
            name = _id;
        }

        public FightLine[] GetPlayerLines => _playerLines;
        public FightLine GetEnemyLine => _enemyLine;

    }

    [System.Serializable]
    public class RapBattle
    {
        public string name;
        private string _id;
        // TODO : privatise
        public FightDlg[] _fightDialogs;

        public RapBattle(string id, FightDlg[] fightDialogs)
        {
            _id = id;
            name = _id;
            _fightDialogs = fightDialogs;
        }

        public void UpdateID(string newID)
        {
            if (_id != null) _id = newID;
        }

        public FightDlg[] GetFightDialogs => _fightDialogs;
    }

    class CSV
    {
        private string _csvAsString;

        public CSV()
        {
            TextAsset csv = Resources.Load<TextAsset>("Team02/Fights");
            if (!csv)
            {
                Debug.LogError("There are no \"Fights.csv\" file in \"Resources/Team02\".");
                return;
            }
            _csvAsString = csv.text;
            Debug.Log("CSV as pure string :\n" + _csvAsString);
        }

        /// <summary>
        /// Returns an array of arrays of strings. Each array is a line of the csv and each line is an array of the csv columns.
        /// </summary>
        public string[][] ToStrings()
        {
            string[][] a = new string[4][];
            return a;
        }
    }
}