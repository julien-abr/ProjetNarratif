using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Team02
{
    public class FightDialogueTool : EditorWindow
    {
        static RapBattlesSO rapBattlesSO;
        static RapBattle selectedRapBattle;
        private Vector2 scrollPos;

        [MenuItem("Tools/Rap battles tool")]
        public static FightDialogueTool ShowWindow()
        {
            var window = GetWindow<FightDialogueTool>();
            window.titleContent = new GUIContent("Rap battles tool");
            window.Show();
            rapBattlesSO = Resources.Load<RapBattlesSO>("Team02/RapBattles");
            if (!rapBattlesSO)
                Debug.LogWarning("No RapBattles ScriptableObject found in Resources/Team02/ !");
            else
                Debug.Log($"Found RapBattles ! It contains {rapBattlesSO.GetRapBattles.Count} rap battles.");

            return window;
        }

        public void OnDestroy()
        {
            EditorUtility.SetDirty(rapBattlesSO);
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
            if (!rapBattlesSO)
            {
                if (GUILayout.Button(new GUIContent("New Rap Battles List", "Create a new RapBattles ScriptableObject."), bigbuttonStyle))
                {
                    rapBattlesSO = ScriptableObject.CreateInstance<RapBattlesSO>();
                    AssetDatabase.CreateAsset(rapBattlesSO, "Assets/Resources/Team02/RapBattles.asset");
                    EditorUtility.SetDirty(rapBattlesSO);
                    AssetDatabase.SaveAssets();
                }
                return;
            }

            if (rapBattlesSO.GetRapBattles == null)
                rapBattlesSO.SetRapBattles = new List<RapBattle>();

            if (rapBattlesSO.GetRapBattles.Count > 0)
                selectedRapBattle ??= rapBattlesSO.GetRapBattles[0];

            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    foreach (var battle in rapBattlesSO.GetRapBattles)
                    {
                        if (GUILayout.Button(new GUIContent(battle.GetID, $"Access {battle.GetID}\'s data."), smallbuttonStyle))
                        {
                            selectedRapBattle = battle;
                        }
                    }

                    GUILayout.Space(12);
                    GUILayout.Label("===== Tools");
                    GUILayout.Space(12);

                    if (GUILayout.Button(new GUIContent("Hard Import CSV", "Resets everything and imports the csv."), bigbuttonStyle))
                    {
                        rapBattlesSO.HardImportCSV();
                    }

                    if(rapBattlesSO.GetRapBattles.Count > 0)
                    {
                        if (GUILayout.Button("(Do not touch)\nDebug RapBattles ScriptableObject.", bigbuttonStyle))
                        {
                            rapBattlesSO.DebugLog();
                        }
                    }

                    GUILayout.Space(12);
                    GUILayout.Label("===== Languages");
                    GUILayout.Space(12);

                    if(rapBattlesSO.GetLanguages != null)
                    {
                        int i = 0;
                        foreach (var lang in rapBattlesSO.GetLanguages)
                        {
                            if (GUILayout.Button(lang, smallbuttonStyle))
                            {
                                rapBattlesSO.ChangeLanguage(i);
                            }

                            i++;
                        }
                    }
                }
                GUILayout.EndVertical();

                GUILayout.Space(10);

                if (selectedRapBattle != null)
                {
                    scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
                    GUILayout.BeginVertical();
                    {
                        EditorGUI.BeginChangeCheck();
                        GUILayout.Label(selectedRapBattle.GetID, rapLineStyle);

                        /*GUILayout.BeginHorizontal();
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                GUILayout.BeginVertical();
                                {
                                    GUILayout.Label(i == 0 ? "Player Prefab: " : "Opponent Prefab: ");
                                    selectedRapBattle.GetFighters[i] = (GameObject)EditorGUILayout.ObjectField(selectedRapBattle.GetFighters[i], typeof(GameObject), false);
                                }
                                GUILayout.EndVertical();
                            }
                        }
                        GUILayout.EndHorizontal();*/

                        GUILayout.Label("====================================");

                        foreach (var dlg in selectedRapBattle.GetFightDialogs)
                        {
                            GUILayout.Label(dlg.GetID, rapLineStyle);
                            GUILayout.Space(10);
                            foreach (var line in dlg.GetPlayerLines)
                            {
                                GUILayout.Label(line.GetID, EditorStyles.whiteLabel);
                                GUILayout.Label(line.GetText, rapLineStyle);

                                line.DamageType = (LINETYPE)EditorGUILayout.EnumPopup("Damage Type", line.DamageType);

                                GUILayout.Space(15);
                            }

                            GUILayout.Label(dlg.GetEnemyLine.GetID, EditorStyles.whiteLabel);
                            GUILayout.Label(dlg.GetEnemyLine.GetText, rapLineStyle);

                            dlg.GetEnemyLine.DamageType = (LINETYPE)EditorGUILayout.EnumPopup("Damage Type", dlg.GetEnemyLine.DamageType);

                            GUILayout.Space(15);
                            GUILayout.Label("====================================");
                        }


                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(rapBattlesSO);
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