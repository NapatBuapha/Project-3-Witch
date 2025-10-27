using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_IdleState : MushroomBaseState
{
    bool isPlaySound;
    public override void EnterState(Enemy_03_StateManager enemy)
    {
        enemy.rb.isKinematic = true;
       enemy.pathfinder.canMove = false;
    }

    public override void FixedUpdateState(Enemy_03_StateManager enemy)
    {
        enemy.rb.velocity = Vector2.zero;
    }

    public override void UpdateState(Enemy_03_StateManager enemy)
    {
        if(enemy.chaseCon)
        {
             if(!isPlaySound)
            {
                AudioManager.PlaySound(SoundType.Enemy_Spawn, 0.5f);
                isPlaySound = true;
            }
            enemy.Appearing();
        }
    }
}
