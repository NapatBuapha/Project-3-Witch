using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chasing : EnemyBaseState
{
    public override void EnterState(BaseEnemyStateManager enemy)
    {
        enemy.pathfinder.canMove = true;
    }

    public override void FixedUpdateState(BaseEnemyStateManager enemy)
    {

    }

    public override void UpdateState(BaseEnemyStateManager enemy)
    {
    
    }
}
    
