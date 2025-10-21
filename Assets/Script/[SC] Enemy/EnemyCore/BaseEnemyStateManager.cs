using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class BaseEnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    //Input Each Basic State Here

    public Enemy_Idle idle_state { get; private set; } = new Enemy_Idle();
    public Enemy_Chasing enemy_Chasing { get; private set; } = new Enemy_Chasing();

    //Pathfinder ref
    public AIPath pathfinder { get; private set; }

    //Movement 
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    


    #region StateMachineZone

    protected virtual void Start()
    {
        #region component ref
        pathfinder = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        #endregion

        #region set variable
        pathfinder.canMove = false;
        #endregion


        SwitchState(idle_state);
        currentState.EnterState(this);
    }

    protected virtual void Update()
    {
        currentState.UpdateState(this);
    }


    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    #endregion
}
