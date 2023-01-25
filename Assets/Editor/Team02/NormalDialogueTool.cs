using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Team02
{
    public class NormalDialogueTool : EditorWindow
    {
        static DialoguesSO beginDialoguesSO;
        static DialoguesSO endDialoguesSO;
        static Dialogue selectedDialogue;
        private Vector2 scrollPos;

        [MenuItem("Tools/Normal dialogues tool")]
        public static NormalDialogueTool ShowWindow()
        {
            var window = GetWindow<NormalDialogueTool>();
            window.titleContent = new GUIContent("Normal dialogues tool");
            window.Show();
            beginDialoguesSO = Resources.Load<DialoguesSO>("Team02/BeginDialogues");
            endDialoguesSO = Resources.Load<DialoguesSO>("Team02/EndDialogues");
            if (!beginDialoguesSO)
                Debug.LogWarning("No BeginDialogues ScriptableObject found in Resources/Team02/ !");
            else
                Debug.Log($"Found BeginDialogues ! It contains {beginDialoguesSO.dialogues.Count} dialogues.");

            if (!endDialoguesSO)
                Debug.LogWarning("No EndDialogues ScriptableObject found in Resources/Team02/ !");
            else
                Debug.Log($"Found EndDialogues ! It contains {endDialoguesSO.dialogues.Count} dialogues.");

            return window;
        }

        public void OnDestroy()
        {
            if(beginDialoguesSO)
                EditorUtility.SetDirty(beginDialoguesSO);
            if(endDialoguesSO)
                EditorUtility.SetDirty(endDialoguesSO);
            AssetDatabase.SaveAssets();
        }

        public void OnGUI()
        {
            GUIStyle bigbuttonStyle = new(GUI.skin.button);
            bigbuttonStyle.fixedHeight = 50;
            bigbuttonStyle.fixedWidth = 200;
            GUIStyle smallbuttonStyle = new(GUI.skin.button);
            smallbuttonStyle.fixedHeight = 25;
            smallbuttonStyle.fixedWidth = 200;
            GUIStyle rapLineStyle = new(GUI.skin.label);
            rapLineStyle.fontSize = 16;
            rapLineStyle.fontStyle = FontStyle.Bold;
            rapLineStyle.wordWrap = true;

            if (!beginDialoguesSO)
            {
                if (GUILayout.Button(new GUIContent("New BeginDialogues List", "Create a new BeginDialogues ScriptableObject."), bigbuttonStyle))
                {
                    beginDialoguesSO = ScriptableObject.CreateInstance<DialoguesSO>();
                    AssetDatabase.CreateAsset(beginDialoguesSO, "Assets/Resources/Team02/BeginDialogues.asset");
                    EditorUtility.SetDirty(beginDialoguesSO);
                    AssetDatabase.SaveAssets();
                }
                return;
            }

            if (!endDialoguesSO)
            {
                if (GUILayout.Button(new GUIContent("New EndDialogues List", "Create a new EndDialogues ScriptableObject."), bigbuttonStyle))
                {
                    endDialoguesSO = ScriptableObject.CreateInstance<DialoguesSO>();
                    AssetDatabase.CreateAsset(endDialoguesSO, "Assets/Resources/Team02/EndDialogues.asset");
                    EditorUtility.SetDirty(endDialoguesSO);
                    AssetDatabase.SaveAssets();
                }
                return;
            }

            beginDialoguesSO.dialogues ??= new List<Dialogue>();
            endDialoguesSO.dialogues ??= new List<Dialogue>();

            if (beginDialoguesSO.dialogues.Count > 0)
                selectedDialogue ??= beginDialoguesSO.dialogues[0];
            else if (beginDialoguesSO.dialogues.Count > 0)
                selectedDialogue ??= beginDialoguesSO.dialogues[0];

            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Space(12);
                    GUILayout.Label("===== Begin dialogues");
                    foreach (var dialogue in beginDialoguesSO.dialogues)
                    {
                        if (GUILayout.Button(new GUIContent(dialogue.id, $"Access {dialogue.id}\'s data."), smallbuttonStyle))
                        {
                            selectedDialogue = dialogue;
                        }
                    }

                    GUILayout.Space(12);
                    GUILayout.Label("===== End dialogues");
                    foreach (var dialogue in endDialoguesSO.dialogues)
                    {
                        if (GUILayout.Button(new GUIContent(dialogue.id, $"Access {dialogue.id}\'s data."), smallbuttonStyle))
                        {
                            selectedDialogue = dialogue;
                        }
                    }

                    GUILayout.Space(12);
                    GUILayout.Label("===== Tools");
                    GUILayout.Space(12);

                    if(beginDialoguesSO.dialogues.Count > 0)
                    {
                        if (GUILayout.Button(new GUIContent("Soft Import BeginCSV", "Update the texts to the one in the csv. The csv must be the same apart from the changes in text."), bigbuttonStyle))
                        {
                            beginDialoguesSO.SoftImportCSV("Begin");
                        }
                    }

                    if (endDialoguesSO.dialogues.Count > 0)
                    {
                        if (GUILayout.Button(new GUIContent("Soft Import EndCSV", "Update the texts to the one in the csv. The csv must be the same apart from the changes in text."), bigbuttonStyle))
                        {
                            endDialoguesSO.SoftImportCSV("End");
                        }
                    }

                    if (GUILayout.Button(new GUIContent("Hard Import BeginCSV", "Resets everything and imports the csv."), bigbuttonStyle))
                    {
                        beginDialoguesSO.HardImportCSV("Begin");
                    }

                    if (GUILayout.Button(new GUIContent("Hard Import EndCSV", "Resets everything and imports the csv."), bigbuttonStyle))
                    {
                        endDialoguesSO.HardImportCSV("End");
                    }

                    GUILayout.Space(12);
                    GUILayout.Label("===== Languages");
                    GUILayout.Space(12);

                    if(beginDialoguesSO.dialogues != null)
                    {
                        if(beginDialoguesSO.languages != null)
                        {
                            int i = 0;
                            foreach (var lang in beginDialoguesSO.languages)
                            {
                                if (GUILayout.Button(lang, smallbuttonStyle))
                                {
                                    beginDialoguesSO.ChangeLanguage(i, "Begin");
                                    endDialoguesSO.ChangeLanguage(i, "End");
                                }

                                i++;
                            }
                        }
                    }
                }
                GUILayout.EndVertical();

                GUILayout.Space(10);

                if (selectedDialogue != null)
                {
                    scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
                    GUILayout.BeginVertical();
                    {
                        EditorGUI.BeginChangeCheck();
                        GUILayout.Label(selectedDialogue.id, rapLineStyle);

                        GUILayout.Label("====================================");

                        foreach (var line in selectedDialogue.lines)
                        {
                            GUILayout.Label(line.id, EditorStyles.whiteLabel);
                            GUILayout.Space(10);
                            GUILayout.Label(line.text, rapLineStyle);

                            GUILayout.BeginHorizontal();
                            line.character = (CHARACTER)EditorGUILayout.EnumPopup("Character", line.character);
                            line.pose = (SPRITE_POSE)EditorGUILayout.EnumPopup("Pose", line.pose);
                            GUILayout.EndHorizontal();

                            GUILayout.Space(15);
                            GUILayout.Label("====================================");
                        }

                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(beginDialoguesSO);
                            EditorUtility.SetDirty(endDialoguesSO);
                            AssetDatabase.SaveAssets();
                        }
                    }

                    GUILayout.EndVertical();
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}