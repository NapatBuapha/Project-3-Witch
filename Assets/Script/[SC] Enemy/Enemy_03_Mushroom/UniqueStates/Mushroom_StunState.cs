using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_StunState : MushroomBaseState
{
    bool isPlaySound;
    public override void EnterState(Enemy_03_StateManager enemy)
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.pathfinder.canMove = false;
        enemy.aController.Hurt();
    }

    public override void FixedUpdateState(Enemy_03_StateManager enemy)
    {
        
    }

    public override void UpdateState(Enemy_03_StateManager enemy)
    {

    }
}
