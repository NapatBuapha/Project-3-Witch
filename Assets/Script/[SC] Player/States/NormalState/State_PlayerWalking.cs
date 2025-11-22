using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerWalking : PlayerBaseState
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.stats.rb;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        Vector2 direction = new Vector2(player.player_HInput,player.player_VInput).normalized;
        rb.velocity = player.w_speed * direction;

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.isWalking)
        {
            player.SwitchState(player.state_PlayerIdle);
        }
        if(player.dashInput)
        {
            player.SwitchState(player.state_PlayerDash);
        }
    }
}

