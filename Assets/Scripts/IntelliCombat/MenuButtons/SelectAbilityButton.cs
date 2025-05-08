using System;
using TMPro;
using UnityEngine;

namespace IntelliCombat.MenuButtons
{
    public class SelectAbilityButton : MenuButton
    {
        public override string Name { get; } = "SelectAbilityButton";
        public AbilityData abilityData { get; set; }
        public bool isAbilitySelected = false;

        public void Awake()
        {
           abilityData = ScriptableObject.CreateInstance<AbilityData>();
        }

        public void SetAbilityData(AbilityData data)
        {
            abilityData = data;
        }

        public void SetSelected()
        {
            if (!isAbilitySelected)
            {
                isAbilitySelected = true;
                GetComponent<TextMeshProUGUI>().text = "Selected";
                GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            }
        }

        public void UnsetSelected()
        {
            if (isAbilitySelected)
            {
                isAbilitySelected = false;
                GetComponent<TextMeshProUGUI>().text = "Select";
                GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
            }
        }
    }
}