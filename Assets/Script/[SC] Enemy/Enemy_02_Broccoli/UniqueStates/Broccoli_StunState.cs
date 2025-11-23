using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Broccoli_StunState : BroccoliBaseState
{

    public override void EnterState(Enemy_02_StateManager enemy)
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.pathfinder.canMove = false;
        enemy.aController.Hurt();
    }

    public override void FixedUpdateState(Enemy_02_StateManager enemy)
    {
    }

    public override void UpdateState(Enemy_02_StateManager enemy)
    {


    }
}
