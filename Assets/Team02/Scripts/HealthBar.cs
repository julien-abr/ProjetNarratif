using UnityEngine;
using UnityEngine.UI;
using System;

namespace Team02
{
    public class HealthBar : MonoBehaviour
    {
        public int maximum;
        public int current;
        public Image mask;

        public event Action onDamage;
        public void TakeDamage()
        {
            if (onDamage != null)
            {
                onDamage();
            }
        }

        public void Start()
        {
            onDamage += UpdateCurrentFill;

            onDamage += () =>
            {
                float fillAmount = (float)current / (float)maximum;
                mask.fillAmount = fillAmount;
            };
        }

        private void OnValidate()
        {
            UpdateCurrentFill();
        }

        void UpdateCurrentFill()
        {
            float fillAmount = (float)current / (float)maximum;
            mask.fillAmount = fillAmount;
        }
    }
}

