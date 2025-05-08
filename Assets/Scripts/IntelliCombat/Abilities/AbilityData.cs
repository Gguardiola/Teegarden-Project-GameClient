using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityData", menuName = "Ability/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;

    [Header("Ability Stats")] 
    public float damage;
    public float cost;
    public bool isHealAbility;
    public float healAmount;
    public bool isShieldIgnored;
    public bool isPoisonLike;

}