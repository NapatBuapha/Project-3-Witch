using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion_IdleState : OnionBaseStates
{
    public override void EnterState(Enemy_04_StateManager enemy)
    {
        enemy.pathfinder.canMove = false;
    }

    public override void FixedUpdateState(Enemy_04_StateManager enemy)
    {
        
    }

    public override void UpdateState(Enemy_04_StateManager enemy)
    {
        if(enemy.chaseCon)
        {
            enemy.Appearing();
        }
    }
}
