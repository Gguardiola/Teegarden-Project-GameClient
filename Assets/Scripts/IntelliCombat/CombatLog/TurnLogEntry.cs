[System.Serializable]
public class TurnLogEntry
{
    public int turnNumber;
    public string actor;
    public string abilityUsed;
    public float healAmount;
    public float abilityDamage;
    public float abilityEnergyCost;
    public float actorHP;
    public float actorEnergy;
    public float opponentHP;
    public float opponentEnergy;
    public bool isShieldAction;
    public bool isSKippedAction;
    public bool isHealingAbility;
}