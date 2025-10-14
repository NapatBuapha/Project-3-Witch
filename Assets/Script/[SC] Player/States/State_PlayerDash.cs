using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerDash : PlayerBaseState
{
    Rigidbody2D rb;
    float stateTimes; 
    public override void EnterState(PlayerStateManager player)
    {
        player.stats.Stamina -= player.stats.dashSta_Consume;
        rb = player.stats.rb;
        stateTimes = player.stats.dashStatesTime;

        rb.AddForce(new Vector2
        (player.dashPower * player.player_HInput //Horizontal
        ,player.dashPower * player.player_VInput) //Vertical)
        ,ForceMode2D.Impulse); 
        
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

