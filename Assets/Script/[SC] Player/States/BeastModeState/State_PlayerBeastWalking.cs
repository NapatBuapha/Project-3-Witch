using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerBeastWalking : PlayerBaseState
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.stats.rb;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        rb.velocity = new Vector2
        (player.w_speed * player.player_HInput //Horizontal
        , player.w_speed *  player.player_VInput); //Vertical
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.isWalking)
        {
            player.SwitchState(player.state_PlayerBeastIdle);
        }
        
        if(player.AttackCon)
        {
            player.SwitchState(player.state_PlayerBeastAttack);
        }
    }
}

