using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerBeastAttack : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("attack");
        player.BeastAttack();

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

    
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
}

