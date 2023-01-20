using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabChoice;

    private List<string> allLines = new List<string>() { 
        "Tu me plaisais pas en même temps",
        "Ouais bas déso",
        "T'as tué ma grand mère putain",
        "Sale chienne !",
        "C'est vraiment pas sympa"};

    private List<Choices> allChoices = new List<Choices>();

    [SerializeField, Range(1, 5)]
    private int numberOfChoices;

    private void OnValidate()
    {
        Debug.Log("Something changed");

        //UpdateAllChoices();
    }

    private void Start()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            // desactiver les games objects inutiles au lieu de les supprimer
        }
    }

    private void UpdateAllChoices()
    {
        if (numberOfChoices != this.gameObject.transform.childCount)
        {
            foreach (Transform child in this.gameObject.transform)
            {
                if (Application.isEditor)
                    Object.DestroyImmediate(child.gameObject);
                else
                    Object.Destroy(child.gameObject);
            }

            for (int i = 0; i < numberOfChoices; i++)
            {
                GameObject line = Instantiate(prefabChoice, this.gameObject.transform);
                line.GetComponent<Choices>().Configure(allLines[i]);
            }
        }
    }
}
