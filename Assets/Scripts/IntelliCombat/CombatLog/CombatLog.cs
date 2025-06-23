using System.Collections.Generic;

[System.Serializable]
public class CombatLog
{
    
    public List<AbilityDataWrapper> enemyInitialAbilities = new();
    public List<AbilityDataWrapper> discoveredPlayerAbilities = new();
    
    public List<TurnLogEntry> turns = new();
    public string winner;
}