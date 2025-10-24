using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_ChasingState : MushroomBaseState
{
    public override void EnterState(Enemy_03_StateManager enemy)
    {
        enemy.col.isTrigger = false;
        enemy.rb.isKinematic = false;
       enemy.pathfinder.canMove = true;
    }

    public override void FixedUpdateState(Enemy_03_StateManager enemy)
    {
        
    }

    public override void UpdateState(Enemy_03_StateManager enemy)
    {
        if(enemy.ExplodingCon)
        {
            enemy.SwitchState(enemy.state_Exploding);
        }
    }
}
