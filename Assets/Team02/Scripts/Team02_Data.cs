using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Team02
{
    [System.Serializable]
    public enum LINETYPE
    {
        EFFECTIVE = 2,
        NORMAL = 1,
        INEFFECTIVE = -1
    }

    public enum CHARACTER
    {
        PLAYER = 0,
        DRUNK = 1,
        DOG = 2,
        EX = 3
    }

    [System.Serializable]
    public class DialogueLine
    {
        public string id;
        public CHARACTER character;
        public SPRITE_POSE pose;
        public string text;
        public Vector2Int position;

        public DialogueLine(string id)
        {
            this.id = id;
        }
    }

    [System.Serializable]
    public class Dialogue
    {
        public string id;
        public List<DialogueLine> lines = new List<DialogueLine>();

        public Dialogue(string id)
        {
            this.id = id;
        }
    }

    [System.Serializable]
    public class FightLine
    {
        [SerializeField, HideInInspector]
        private CHARACTER _character;
        [SerializeField, HideInInspector]
        private string _id;
        [SerializeField, HideInInspector]
        private string _textLine;
        [SerializeField, HideInInspector]
        private LINETYPE _damageType;
        [SerializeField, HideInInspector]
        private Vector2Int _position;

        public FightLine(string id, string textLine)
        {
            _id = id;
            _textLine = textLine;
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
        }

        public string GetID => _id;
        public string GetText => _textLine;
        public LINETYPE DamageType { get { return _damageType; } set { _damageType = value; } }
        public Vector2Int Position { get { return _position ; } set { _position = value; } }
        public CHARACTER Character { get { return _character; } set { _character = value; } }
    }

    [System.Serializable]
    public class FightDlg
    {
        [SerializeField, HideInInspector]
        private string _id;
        [SerializeField, HideInInspector]
        private List<FightLine> _playerLines = new List<FightLine>();
        [SerializeField, HideInInspector]
        private FightLine _enemyLine;

        public FightDlg(string id, List<FightLine> playerLines, FightLine enemyLine)
        {
            _id = id;
            _playerLines = playerLines;
            _enemyLine = enemyLine;
        }

        public void UpdateID(string newID)
        {
            if (_id == null)
                return;

            _id = newID;
        }

        public string GetID => _id;
        public List<FightLine> GetPlayerLines => _playerLines;
        public FightLine GetEnemyLine => _enemyLine;
    }

    [System.Serializable]
    public class RapBattle
    {
        [SerializeField, HideInInspector]
        private string _id;
        [SerializeField, HideInInspector]
        private List<FightDlg> _fightDialogs = new List<FightDlg>();
        [SerializeField, HideInInspector]
        private GameObject[] _fighters;

        public RapBattle(string id, List<FightDlg> fightDialogs)
        {
            _id = id;
            _fightDialogs = fightDialogs;
            _fighters = new GameObject[2];
        }

        public void UpdateID(string newID)
        {
            if (_id != null) _id = newID;
        }

        public string GetID => _id;
        public List<FightDlg> GetFightDialogs => _fightDialogs;
        public GameObject[] GetFighters => _fighters;
    }

    [System.Serializable]
    class FightCSV
    {
        private string[] _csvAsStrings;

        public FightCSV()
        {
            TextAsset csv = Resources.Load<TextAsset>("Team02/Fights");
            if (!csv)
            {
                Debug.LogError("There are no \"Fights.csv\" file in \"Resources/Team02\".");
                return;
            }
            _csvAsStrings = csv.text.Split('\n');
        }

        /// <summary>
        /// Returns an array of languages found in the csv.
        /// </summary>
        /// <returns></returns>
        public string[] GetLanguages()
        {
            MatchCollection matches = Regex.Matches(_csvAsStrings[0], @"[^;\n\r]+");
            string[] a = new string[matches.Count];

            for(int i = 0; i < matches.Count; i++)
            {
                a[i] = matches[i].Value;
            }

            return a;
        }

        /// <summary>
        /// Returns an array of arrays of strings. Each array is a line of the csv and each line is an array of the csv columns.
        /// </summary>
        public string[,] ToStrings()
        {
            string[,] a;
            int n = (_csvAsStrings[2].Split(';')).Length - 1;
            a = new string[_csvAsStrings.Length - 3, n];

            for(int i = 2; i < _csvAsStrings.Length - 1; i++)
            {
                string[] d = _csvAsStrings[i].Split(';');
                for (int j = 1; j < d.Length; j++)
                {
                    a[i - 2, j - 1] = d[j];
                }
            }

            return a;
        }
    }

    [System.Serializable]
    class DialogueCSV
    {
        private string[] _csvAsStrings;

        /// <summary> name = the name of the file in the Resources folder. </summary>
        public DialogueCSV(string name)
        {
            TextAsset csv = Resources.Load<TextAsset>("Team02/" + name);
            if (!csv)
            {
                Debug.LogError($"There are no \"{name}.csv\" file in \"Resources/Team02\".");
                return;
            }
            _csvAsStrings = csv.text.Split('\n');
        }

        /// <summary> Returns an array of languages found in the csv. </summary>
        public string[] GetLanguages()
        {
            MatchCollection matches = Regex.Matches(_csvAsStrings[0], @"[^;\n\r]+");
            string[] a = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                a[i] = matches[i].Value;
            }

            return a;
        }

        /// <summary>
        /// Returns an array of arrays of strings. Each array is a line of the csv and each line is an array of the csv columns.
        /// </summary>
        public string[,] ToStrings()
        {
            string[,] a;
            int n = (_csvAsStrings[2].Split(';')).Length - 1;
            a = new string[_csvAsStrings.Length - 3, n];

            for (int i = 2; i < _csvAsStrings.Length - 1; i++)
            {
                string[] d = _csvAsStrings[i].Split(';');
                for (int j = 1; j < d.Length; j++)
                {
                    a[i - 2, j - 1] = d[j];
                }
            }

            return a;
        }
    }

    public enum SPRITE_POSE
    {
        IDLE = 0,
        WEAK = 1,
        ATTACK = 2,
        HURT = 3
    }

    [System.Serializable]
    public class CharacterData
    {
        public CHARACTER _character;
        [SerializeField]
        private SpriteData[] _sprites;
        [SerializeField, TextArea]
        private string _description;
        [SerializeField]
        private string[] _strongTraits;
        [SerializeField]
        private string[] _weakTraits;
        //[SerializeField]
        //private AudioClip[] _audioClips;

        public Sprite GetSprite(SPRITE_POSE pose) => _sprites[(int)pose].GetSprite;
        //public AudioClip GetAudioClip(int index) => _audioClips[index];
    }

    [System.Serializable]
    public struct SpriteData
    {
        public SPRITE_POSE _pose;
        [SerializeField]
        private Sprite _sprite;

        public Sprite GetSprite => _sprite;
    }
}