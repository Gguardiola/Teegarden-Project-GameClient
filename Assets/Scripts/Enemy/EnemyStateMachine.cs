using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;

    public void Initialize()
    {
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
