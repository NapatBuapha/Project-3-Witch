using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Broccoli_DashAttack : BroccoliBaseState
{
    Rigidbody2D rb;
    float currentStateTime;
    bool isPlaySound;
    public override void EnterState(Enemy_02_StateManager enemy)
    {
        
        isPlaySound = false;
        currentStateTime = 0;
        rb = enemy.rb;
        enemy.pathfinder.canMove = false;

        enemy.aController.Attack();
        rb.velocity = Vector2.zero;
        rb.AddForce(enemy.pDirection * enemy.stats.dashPower, ForceMode2D.Impulse);
        
        enemy.StartTimeDelay();
        enemy.canDash = false;
    }

    public override void FixedUpdateState(Enemy_02_StateManager enemy)
    {

    }

    public override void UpdateState(Enemy_02_StateManager enemy)
    {
        if(!isPlaySound)
        {
            AudioManager.PlaySound(SoundType.Enemy_Broc_Slash, 0.5f);
            isPlaySound = true;
        }

        if (currentStateTime < enemy.stats.dashStatesTime)
        {
            currentStateTime += Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.states_Chasing);
        }
        
    }
}
