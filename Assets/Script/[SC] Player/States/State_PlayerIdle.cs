using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerIdle : PlayerBaseState
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.stats.rb;
        rb.velocity = Vector2.zero; //reset ค่า velocity กันไม่ให้ตัวละครขยับเเม้ไม่ได้กดอะไร
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(player.isWalking)
        {
            player.SwitchState(player.state_PlayerWalking);
        }
    }
}

