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

            //=====================================================================
            if (rbSO.GetLanguages == null)
                return;

            GUILayout.BeginVertical();
            {
                foreach(var language in rbSO.GetLanguages)
                {
                    if (GUILayout.Button(new GUIContent(language, "Switch to " + language)))
                    {
                        Debug.Log("Switched to " + language);
                    }
                }
            }
            GUILayout.EndVertical();

            base.OnInspectorGUI();
        }
    }
}
