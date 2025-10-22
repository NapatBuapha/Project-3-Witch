using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;
using UnityEngine.Animations;

public class BroccoliAnimationController : BaseEnemyAnimationController
{

    protected override void Start()
    {
        //Component Ref
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();

        //Variable Set
        currentDi = EnemyVerticalDirection.front;
        canWalk = false;

        ChangeAnimation("B_Hide");
    }


    public void Spawn(float spawnStatesTime = 0.3f, int spawnVariation = 1)
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canWalk = false;
            yield return new WaitForSeconds(spawnStatesTime);
            canWalk = true;
        }

        switch (spawnVariation)
        {
            case 1:
                ChangeAnimation("B_Spawn", spawnStatesTime);
                break;
            default:
                ChangeAnimation("B_SpecialSpawn", spawnStatesTime);
                break;
        }

    }
    
    public void Attack(float statesTime = 0.5f)
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canWalk = false;
            yield return new WaitForSeconds(statesTime);
            canWalk = true;
        }

        switch (currentDi)
        {
            case EnemyVerticalDirection.front:
                ChangeAnimation("B_Attack_F", 0.5f);
                break;
            default:
                ChangeAnimation("B_Attack_B", 0.5f);
                break;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(canWalk)
        {
            CheckIdle();
            UpdateDirection();
        }

        
    }
    
    void CheckIdle()
    {
        if (!aIPath.canMove)
        {
            switch (currentDi)
            {
                case EnemyVerticalDirection.front:
                    ChangeAnimation("B_Idle_F");
                    break;

                default:
                    ChangeAnimation("B_Idle_B");
                    break;
            }
        }
        else
        {
            switch (currentDi)
            {
                case EnemyVerticalDirection.front:
                    ChangeAnimation("B_Walking_F");
                    break;

                default:
                    ChangeAnimation("B_Walking_B");
                    break;
            }
        }
    }
}
