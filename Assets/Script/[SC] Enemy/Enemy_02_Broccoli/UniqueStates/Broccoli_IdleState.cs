using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Broccoli_IdleState : BroccoliBaseState
{
    bool isPlaySound;

    public override void EnterState(Enemy_02_StateManager enemy)
    {
        enemy.rb.isKinematic = true;
        enemy.pathfinder.canMove = false;
    }

    public override void FixedUpdateState(Enemy_02_StateManager enemy)
    {
    }

    public override void UpdateState(Enemy_02_StateManager enemy)
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
