using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion_RunAwayState : OnionBaseStates
{
    public override void EnterState(Enemy_04_StateManager enemy)
    {
        enemy.aitarget.target = enemy.fleePoint;
        enemy.pathfinder.canMove = true;
    }

    public override void FixedUpdateState(Enemy_04_StateManager enemy)
    {
        
    }

    public override void UpdateState(Enemy_04_StateManager enemy)
    {
        if (enemy.thrownCon)
        {
            enemy.SwitchState(enemy.state_Thrown);
        }
        else if (!enemy.fleeCon)
        {
            enemy.SwitchState(enemy.state_Chasing);
        }
        
    }
}
