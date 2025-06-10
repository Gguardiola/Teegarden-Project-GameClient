using System.Collections.Generic;
using IntelliCombat.Actions;
using IntelliCombat.MenuButtons;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackAction : AttackAction
{
    public GameObject AttackAbilities;
    public TextMeshProUGUI abilityDescription;
    public List<GameObject> SelectAbilityButtons;
    public ActionControlUI actionControlUI;
    
    public void Start()
    {
        AttackAbilities.SetActive(false);
        LoadAttackAbilities();
    }

    public override void CancelAction()
    {
        base.CancelAction();
        actionControlUI.ShowActionButtons(isActionInProgress);
        HideAttackAbilities();
        UnSelectAbilityButton();
    }

    public override void PerformAction()
    {
        if (isActionInProgress) return;
        isActionInProgress = true;
        actionControlUI.HideActionButtons(isActionInProgress);
        ShowAttackAbilities();
    }
    
    public override bool EndTurnAction()
    {
        if (isAbilitySelected)
        {
            isActionInProgress = false;
            HideAttackAbilities();     
            UnSelectAbilityButton();
            selectedAbility = null;
            return true;
        }

        return false;
    }

    public override void FirstReactorEvent<T>(T eventData)
    {
        if (eventData is AbilityData ability)
        {
            SetSelectedAbilityButton(ability);
        }
    }

    private void SelectAbility(AbilityData ability)
    {
        selectedAbility = ability;
    }

    public void UnSelectAbilityButton()
    {
        isAbilitySelected = false;
        foreach (GameObject abilityButton in SelectAbilityButtons)
        {
            abilityButton.GetComponent<SelectAbilityButton>().UnsetSelected();
        }
    }
    private void SetSelectedAbilityButton(AbilityData ability)
    {
        foreach (GameObject abilityButton in SelectAbilityButtons)
        {
            SelectAbilityButton currentAbilityButton = abilityButton.GetComponent<SelectAbilityButton>();
            if (currentAbilityButton.abilityData.abilityName == ability.abilityName)
            {
                currentAbilityButton.SetSelected();
                isAbilitySelected = true;
                SelectAbility(ability);
            }
            else
            {
                currentAbilityButton.UnsetSelected();
            }
        }
    }

    private void LoadAttackAbilities()
    {
        if (AttackAbilities != null)
        {
            int count = 0;
            foreach (Transform child in AttackAbilities.transform)
            {
                
                GameObject currentAbility = child.gameObject;
                if (currentAbility.CompareTag("AttackAbility"))
                {
                    
                    foreach (Transform label in currentAbility.transform)
                    {
                        GameObject abilityLabel = label.gameObject;
                        if(SelectAbilityButtons[count] != null)
                        {
                            SelectAbilityButtons[count].GetComponent<SelectAbilityButton>().SetAbilityData(abilities[count]);
                        }
                        if (abilityLabel.GetComponent<TextMeshProUGUI>() && abilityLabel.name == "UI_AttackAbilityName")
                        {
                            abilityLabel.GetComponent<TextMeshProUGUI>().text = abilities[count].abilityName;
                            abilityDescription.text += "- " + abilities[count].abilityName;
                            abilityDescription.text += "\n" + "\n";
                            abilityDescription.text += "Description:\n";
                            abilityDescription.text += abilities[count].abilityDescription;
                            abilityDescription.text += "\n";
                            abilityDescription.text += "--------------------------\n";
                        }
                        if (abilityLabel.GetComponent<TextMeshProUGUI>() && abilityLabel.name == "UI_AttackAbilityDamageLabel")
                        {
                            abilityLabel.GetComponent<TextMeshProUGUI>().text = "Damage: " + abilities[count].damage.ToString() + "p";
                            if (abilities[count].isHealAbility)
                            {
                                abilityLabel.SetActive(false);
                            }
                        }

                        if (abilities[count].isHealAbility && abilityLabel.GetComponent<TextMeshProUGUI>() && abilityLabel.name == "UI_AttackAbilityHealLabel")
                        {
                            abilityLabel.SetActive(true);
                            abilityLabel.GetComponent<TextMeshProUGUI>().text = "Heal: " + abilities[count].healAmount.ToString() + "p";
                        }
                        if (abilityLabel.GetComponent<TextMeshProUGUI>() && abilityLabel.name == "UI_AttackAbilityEnergyCostLabel")
                        {
                            abilityLabel.GetComponent<TextMeshProUGUI>().text = "Cost: " + abilities[count].cost.ToString() + "p";
                            count++;
                        }

                    }
                    
                }
            }           
        }

    }

    private void HideAttackAbilities()
    {
        AttackAbilities.SetActive(false);
    }
    private void ShowAttackAbilities()
    {
        // Logic to show attack abilities
        AttackAbilities.SetActive(true);
        
    }                    
}
