using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerDeTransform : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.stats.rb.velocity = Vector2.zero;
        player.BeastDeTransform();
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
}

