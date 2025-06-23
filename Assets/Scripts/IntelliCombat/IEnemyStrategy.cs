using IntelliCombat.Actions;

public interface IEnemyStrategy
{
    void Decide(System.Action<Action> onDecisionMade);
}