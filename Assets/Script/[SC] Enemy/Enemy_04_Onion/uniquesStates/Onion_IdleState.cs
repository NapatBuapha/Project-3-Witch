using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion_IdleState : OnionBaseStates
{
    bool isPlaySound;
    public override void EnterState(Enemy_04_StateManager enemy)
    {
        
        enemy.rb.isKinematic = true;
        enemy.pathfinder.canMove = false;
    }

    public override void FixedUpdateState(Enemy_04_StateManager enemy)
    {

    }

    public override void UpdateState(Enemy_04_StateManager enemy)
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
