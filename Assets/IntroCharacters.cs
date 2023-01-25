using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team02
{
    public class IntroCharacters : MonoBehaviour
    {
        [SerializeField]
        ChoiceManager choiceManager;

        [SerializeField]
        private Text nameSpeaker;
        [SerializeField]
        private Text lineSpeaker;

        private void Start()
        {
            if (choiceManager == null)
            {
                choiceManager = FindObjectOfType<ChoiceManager>();
            }

            choiceManager.enemyVisual.sprite = choiceManager.AllEnemySprites[0];

            // First speaker 
            FightLine firstSpeaker = choiceManager.CurrentFightDlg.GetEnemyLine;
            nameSpeaker.text = firstSpeaker.GetID;
            lineSpeaker.text = firstSpeaker.GetText;
        }
    }
}

