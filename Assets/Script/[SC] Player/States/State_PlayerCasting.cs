using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerCasting : PlayerBaseState
{
    Rigidbody2D rb;
    float stateTimes; 
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.stats.rb;
        stateTimes = player.castingDura;
        rb.velocity = Vector2.zero; //reset ค่า velocity กันไม่ให้ตัวละครขยับเเม้ไม่ได้กดอะไร
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

