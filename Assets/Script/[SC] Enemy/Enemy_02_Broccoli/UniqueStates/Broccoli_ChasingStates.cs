using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broccoli_ChasingStates : BroccoliBaseState
{
    public override void EnterState(Enemy_02_StateManager enemy)
    {
        enemy.aController.Attack();
        enemy.pathfinder.canMove = true;
    }

    public override void FixedUpdateState(Enemy_02_StateManager enemy)
    {

    }

    public override void UpdateState(Enemy_02_StateManager enemy)
    {
        if (enemy.dashStateCon)
        {
            enemy.SwitchState(enemy.states_DashAttack);
        }

    }
}
