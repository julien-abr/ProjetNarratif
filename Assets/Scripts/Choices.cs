using UnityEngine;
using UnityEngine.UI;

namespace Team02
{   
    public class Choices : MonoBehaviour
    {
        public Text choiceText;
        private int idChoice;
        public int IDChoice => idChoice;

        public DialogEffectiveness effectiveness;
        private DialogEffectiveness Effectiveness => effectiveness;

        public void Start()
        {
            choiceText ??= this.gameObject.GetComponent<Text>();
        }

        public enum DialogEffectiveness
        {
            Ineffective,
            Normal,
            Critical
        }

        public void Configure(string textLine)
        {
            choiceText.text = ($"- {textLine}.");
        }

        public void Selected()
        {
            Debug.Log(name);
        }
    }

}
