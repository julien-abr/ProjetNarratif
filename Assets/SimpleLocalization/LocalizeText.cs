using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using JetBrains.Annotations;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    public static LocalizeText Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        LocalizationManager.Read();  
    }

    public void SetLanguage(string language)
    {
        Debug.Log("Language : " + language);
        LocalizationManager.Language = language;
    }


}
