using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Broccoli_DashAttack : BroccoliBaseState
{
    Rigidbody2D rb;
    float currentStateTime;
    public override void EnterState(Enemy_02_StateManager enemy)
    {
        currentStateTime = 0;
        rb = enemy.rb;
        enemy.pathfinder.canMove = false;

        rb.velocity = Vector2.zero;
        rb.AddForce(enemy.pDirection * enemy.stats.dashPower, ForceMode2D.Impulse);
    }

    public override void FixedUpdateState(Enemy_02_StateManager enemy)
    {

    }

    public override void UpdateState(Enemy_02_StateManager enemy)
    {
        if (currentStateTime < enemy.stats.dashStatesTime)
        {
            currentStateTime += Time.deltaTime;
        }
        else
        {
            enemy.StartTimeDelay();
            enemy.SwitchState(enemy.states_Chasing);
        }
        
    }
}
