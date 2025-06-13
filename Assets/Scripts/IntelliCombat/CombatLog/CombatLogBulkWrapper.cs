using System.Collections.Generic;

[System.Serializable]
public class CombatLogBulkWrapper
{
    public List<CombatLog> logs;

    public CombatLogBulkWrapper(List<CombatLog> logs)
    {
        this.logs = logs;
    }
}