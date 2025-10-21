using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Broccoli_IdleStates : BroccoliBaseState
{
    public override void EnterState(Enemy_02_StateManager enemy)
    {
        enemy.pathfinder.canMove = false;
    }

    public override void FixedUpdateState(Enemy_02_StateManager enemy)
    {

    }

    public override void UpdateState(Enemy_02_StateManager enemy)
    {
        if(enemy.chaseCon)
        {
            enemy.Appearing();
        }
    }
}
