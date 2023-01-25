using System.Collections.Generic;
using UnityEngine;

namespace Team02
{
    public class DialoguesSO : ScriptableObject
    {
        [HideInInspector]
        public List<Dialogue> dialogues;
        [HideInInspector]
        public string[] languages;

        private const int DIALOGUE_ID_INDEX = 4;

        public void HardImportCSV(string name)
        {
            DialogueCSV csv = new(name);
            languages = csv.GetLanguages();
            string[,] importedStrings = csv.ToStrings();
            string dialogueNbSstring = importedStrings[importedStrings.GetLength(0) - 1, 0].Substring(DIALOGUE_ID_INDEX, 2);
            int dialogueNb = int.Parse(dialogueNbSstring) + 1;

            Debug.Log($"{name}CSV : " + languages.Length + " languages found in the csv.");
            Debug.Log($"{name}CSV : " + dialogueNb + " dialogues found in the csv.");

            dialogues = new();
            int currentDialogue = 0;
            dialogues.Add(new(importedStrings[0, 0][..6]));
            for (int i = 0; i < importedStrings.GetLength(0); i++)
            {
                if (int.Parse(importedStrings[i, 0].Substring(DIALOGUE_ID_INDEX, 2)) == currentDialogue)
                {
                    DialogueLine line = new(importedStrings[i, 0]);
                    string character = importedStrings[i, 1];
                    switch (character)
                    {
                        case "PLAYER":
                            line.character = CHARACTER.PLAYER;
                            break;
                        case "DRUNK":
                            line.character = CHARACTER.DRUNK;
                            break;
                        case "DOG":
                            line.character = CHARACTER.DOG;
                            break;
                        case "EX":
                            line.character = CHARACTER.EX;
                            break;
                    }
                    line.text = importedStrings[i, 2];
                    line.position = new Vector2Int(i, 2);
                    dialogues[currentDialogue].lines.Add(line);
                }
                else
                {
                    currentDialogue++;
                    dialogues.Add(new(importedStrings[i, 0][..6]));
                    i--;
                }
            }
        }

        public void ChangeLanguage(int lang, string name)
        {
            DialogueCSV csv = new(name);
            string[,] importedStrings = csv.ToStrings();

            for(int i = 0; i < dialogues.Count; i++)
            {
                for(int j = 0; j < dialogues[i].lines.Count; j++)
                {
                    dialogues[i].lines[j].text = importedStrings[dialogues[i].lines[j].position.x, dialogues[i].lines[j].position.y + lang];
                }
            }
        }
    }


}
