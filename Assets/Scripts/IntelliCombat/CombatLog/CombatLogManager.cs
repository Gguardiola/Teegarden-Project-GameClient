public class CombatLogManager
{
    public CombatLog log = new();
    private int currentTurn = 0;

    public void RecordTurn(string actorName, string abilityUsed, float abilityDamage, float healAmount, float abilityEnergyCost, float actorHP, float actorEnergy,
        float opponentHP, float opponentEnergy, bool isShieldAction = false, bool isSkippedAction = false, bool isHealingAbility = false)
    {
        log.turns.Add(new TurnLogEntry
        {
            turnNumber = currentTurn++,
            actor = actorName,
            abilityUsed = abilityUsed,
            abilityDamage = abilityDamage,
            healAmount = healAmount,
            abilityEnergyCost = abilityEnergyCost,
            actorHP = actorHP,
            actorEnergy = actorEnergy,
            opponentHP = opponentHP,
            opponentEnergy = opponentEnergy,
            isShieldAction = isShieldAction,
            isSKippedAction = isSkippedAction,
            isHealingAbility = isHealingAbility
        });
    }

    public void SetWinner(string winnerName)
    {
        log.winner = winnerName;
    }

    public CombatLog GetFinalLog() => log;
}