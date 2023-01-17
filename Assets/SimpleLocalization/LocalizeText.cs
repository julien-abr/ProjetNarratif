using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    void Awake()
    {
        LocalizationManager.Read();

        LocalizationManager.Language = "English";
        
    }


}
