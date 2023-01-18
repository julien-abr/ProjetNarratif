using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Team02
{
    [CustomEditor(typeof(RapBattlesSO))]
    public class RapBattlesSO_Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            RapBattlesSO rbSO = (RapBattlesSO)target;

            GUILayout.BeginVertical();
            {
                if (GUILayout.Button(new GUIContent("Hard Import CSV", "This imports the Fights CSV as new and destroys the current data.\nONLY USE THIS IF YOU DON'T CARE ABOUT LOOSING THIS SO's DATA.")))
                {
                    rbSO.HardImportCSV();
                }
            }
            GUILayout.EndVertical();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button(new GUIContent("French", "Switch the texts to french.")))
                {
                    Debug.Log("Hard imported the csv !");
                }
                if (GUILayout.Button(new GUIContent("English", "Switch the texts to english.")))
                {
                    Debug.Log("Hard imported the csv !");
                }
            }
            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }
}
