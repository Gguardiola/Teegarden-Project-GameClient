using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public BaseState activeState;
    public void Initialize(BaseState newState)
    { 
        ChangeState(newState);
    }

    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }

        activeState = newState;
        if (activeState != null)
        {
            activeState.enemyStateMachine = this;
            activeState.enemyAI = GetComponent<EnemyAI>();
            activeState.Enter();
        }
    }
}
