public abstract class BaseState
{
    public EnemyAI enemyAI;
    public EnemyStateMachine enemyStateMachine;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();

    public void CheckHealth()
    {
        enemyAI.CheckHealth();
    }

    protected void PlayStateSFX()
    {
            enemyAI.SwapSFX();
    }
}