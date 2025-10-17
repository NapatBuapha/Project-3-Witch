using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chasing : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {

    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.rb.AddForce(enemy.force);
    }
}
    
