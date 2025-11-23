using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;
using UnityEngine.Animations;

public class MushroomAnimationController : BaseEnemyAnimationController
{

    protected override void Start()
    {
        //Component Ref
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();

        //Variable Set
        currentDi = EnemyVerticalDirection.front;
        canWalk = false;

        ChangeAnimation("M_Hiding");
    }


    public void Spawn(float spawnStatesTime = 0.3f)
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canWalk = false;
            yield return new WaitForSeconds(spawnStatesTime);
            canWalk = true;
        }
        ChangeAnimation("M_Spawn", spawnStatesTime);
    }

    public void Hurt(float duration = 0.25f)
    {
        if(!canWalk) return;

        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            canWalk = false;

            switch (currentDi)
            {
                case EnemyVerticalDirection.front:
                    ChangeAnimation("M_Hurt_F");
                    break;

                default: //Case Back
                    ChangeAnimation("M_Hurt_B");
                    break;
            }



            yield return new WaitForSeconds(duration);

            canWalk = true;
        }
    }
    
    public void Explode(float statesTime = 0.5f)
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canWalk = false;
            ChangeAnimation("M_Explode",0f);
            yield return new WaitForSeconds(statesTime);
            canWalk = true;
        }

        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(canWalk)
        {
            UpdateDirection();
            CheckWalkingDi();
        }

        
    }
    
    void CheckWalkingDi()
    {
        
        switch (currentDi)
         {
            case EnemyVerticalDirection.front:
                ChangeAnimation("M_Chasing_F",0.2f);
                break;

            default:
                ChangeAnimation("M_Chasing_B",0.2f);
                break;
        }
        
    }
}
