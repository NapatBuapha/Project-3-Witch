using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chasing : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.pathfinder.canMove = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
    
    }
}
    
