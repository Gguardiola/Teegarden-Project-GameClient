using System.Collections.Generic;
using System.Linq;

public class CombatLogManager
{
    public CombatLog log = new();
    private int currentTurn = 0;
    
    public void SetEnemyInitialAbilities(List<AbilityData> abilities)
    {
        log.enemyInitialAbilities = abilities.Select(a => ToWrapper(a)).ToList();
    }

    public void RecordDiscoveredPlayerAbility(AbilityData ability)
    {
        if (log.discoveredPlayerAbilities.All(a => a.abilityName != ability.abilityName))
        {
            log.discoveredPlayerAbilities.Add(ToWrapper(ability));
        }
    }

    public void RecordTurn(string actorName, string abilityUsed, float abilityDamage, float healAmount, float abilityEnergyCost, float actorHP, float actorEnergy,
        float opponentHP, float opponentEnergy, bool wasEffective, bool isShieldAction = false, bool isSkippedAction = false, bool isHealingAbility = false)
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
            isSkippedAction = isSkippedAction,
            isHealingAbility = isHealingAbility,
            wasEffective = wasEffective
            
        });
    }

    public void SetWinner(string winnerName)
    {
        log.winner = winnerName;
    }

    public CombatLog GetFinalLog() => log;
    
    private AbilityDataWrapper ToWrapper(AbilityData a)
    {
        return new AbilityDataWrapper
        {
            abilityName = a.abilityName,
            abilityDescription = a.abilityDescription,
            cost = a.cost,
            damage = a.damage,
            healAmount = a.healAmount,
            isHealAbility = a.isHealAbility,
            isShieldIgnored = a.isShieldIgnored,
            isPoisonLike = a.isPoisonLike,
        };
    }
}