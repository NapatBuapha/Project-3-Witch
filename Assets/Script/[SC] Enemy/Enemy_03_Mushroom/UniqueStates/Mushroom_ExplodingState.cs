using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_ExplodingState : MushroomBaseState
{
    public override void EnterState(Enemy_03_StateManager enemy)
    {
        enemy.pathfinder.canMove = true;
        Debug.Log("Exploded");
        enemy.Explode();


    }

    public override void FixedUpdateState(Enemy_03_StateManager enemy)
    {
        
    }

    public override void UpdateState(Enemy_03_StateManager enemy)
    {
        
    }
}
