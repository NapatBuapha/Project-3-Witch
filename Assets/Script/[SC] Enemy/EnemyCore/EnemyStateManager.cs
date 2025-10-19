using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    //Input Each State Here

    public Enemy_Idle idle_state { get; private set; } = new Enemy_Idle();
    public Enemy_Chasing enemy_Chasing { get; private set; } = new Enemy_Chasing();

    //Pathfinder ref
    public AIPath pathfinder { get; private set; }

    //Movement 
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    


    #region StateMachineZone

    void Start()
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

    void Update()
    {
        #region Normal update code
        #endregion


        #region StateCondition

        #endregion

        #region NonStateCondition

        #endregion

        currentState.UpdateState(this);
    }


    void FixedUpdate()
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
