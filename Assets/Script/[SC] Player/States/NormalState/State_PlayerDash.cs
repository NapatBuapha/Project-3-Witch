using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerDash : PlayerBaseState
{
    Rigidbody2D rb;
    float stateTimes; 
    public override void EnterState(PlayerStateManager player)
    {
        player.dashDisplayer.DashCooldown();
        rb = player.stats.rb;
        stateTimes = player.stats.dashStatesTime;

        player.animaCon.DashAnim(stateTimes);
        Vector2 direction = new Vector2(player.player_HInput,player.player_VInput).normalized;

        rb.AddForce(player.dashPower * direction, ForceMode2D.Impulse); 
        
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (stateTimes > 0)
        {
            stateTimes -= Time.deltaTime;
        }
        else
        {
            player.StartCoroutine(player.SetDashCoolDown());

            if (player.isWalking)
            {
                player.SwitchState(player.state_PlayerWalking);
            }
            else if(!player.isWalking)
            {
                player.SwitchState(player.state_PlayerIdle);
            }
        }

    }
}

