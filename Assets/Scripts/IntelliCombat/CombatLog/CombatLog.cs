using System.Collections.Generic;

[System.Serializable]
public class CombatLog
{
    public List<TurnLogEntry> turns = new();
    public string winner;
}