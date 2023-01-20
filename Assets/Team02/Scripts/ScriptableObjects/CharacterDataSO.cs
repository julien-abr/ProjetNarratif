using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team02
{
    [CreateAssetMenu(fileName = "Characters Data", menuName = "Team02/ScriptableObjects/Characters Data", order = 0)]
    public class CharacterDataSO : ScriptableObject
    {
        [SerializeField]
        private CharacterData[] _characters;

        public CharacterData GetCharacterData(int index) => _characters[index];
    }
}
