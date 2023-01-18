using UnityEngine;
using UnityEngine.UI;

public class Choices
{
    public Text choiceText;
    private int idChoice;
    public int IDChoice => idChoice;

    public DialogEffectiveness effectiveness;
    private DialogEffectiveness Effectiveness => effectiveness;

    public enum DialogEffectiveness
    {
        Ineffective,
        Normal,
        Critical
    }

    public void Configure(string textLine)
    {
        choiceText.text = textLine;
    }
}
