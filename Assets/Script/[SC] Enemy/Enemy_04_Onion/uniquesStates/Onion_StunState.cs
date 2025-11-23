using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Onion_StunState : OnionBaseStates
{
    public override void EnterState(Enemy_04_StateManager enemy)
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.pathfinder.canMove = false;
        enemy.aController.Hurt(enemy.stats.spawnStatesTime);
    }

    public override void FixedUpdateState(Enemy_04_StateManager enemy)
    {
        
    }

    public override void UpdateState(Enemy_04_StateManager enemy)
    {

    }
}
