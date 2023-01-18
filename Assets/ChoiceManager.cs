using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ChoiceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabChoice;

    private List<string> allLines = new List<string>() { 
        "- Tu me plaisais pas en m�me temps.",
        "- Ouais bas d�so.",
        "- T'as tu� ma grand m�re putain.",
        "- Tu me plaisais pas en m�me temps.",
        "- Ouais bas d�so."};

    private List<Choices> allChoices = new List<Choices>();

    [SerializeField, Range(1, 5)]
    private int numberOfChoices;

    private void Start()
    {
        if (numberOfChoices != this.gameObject.transform.childCount)
        {
            foreach (Transform child in this.gameObject.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < numberOfChoices; i++)
            {
                GameObject line = Instantiate(prefabChoice, this.gameObject.transform);
                line.GetComponent<Choices>().Configure(allLines[i]);
            }
        }
        
        for (int i = 0; i < numberOfChoices; i++)
        {

        }
    }
}
