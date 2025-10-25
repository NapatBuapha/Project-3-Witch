using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBoss_GroundAttack : MushroomBossBaseState
{
    public override void EnterState(Boss_01_StateManager enemy)
    {
        enemy.aController.FloorStomp();
    }

    public override void FixedUpdateState(Boss_01_StateManager enemy)
    {

    }

    public override void UpdateState(Boss_01_StateManager enemy)
    {

    }

}
