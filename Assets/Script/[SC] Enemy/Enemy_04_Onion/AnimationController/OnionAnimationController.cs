using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class OnionAnimationController : BaseEnemyAnimationController
{
    protected override void Start()
    {
        //Component Ref
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();

        //Variable Set
        currentDi = EnemyVerticalDirection.front;
        canWalk = false;

        ChangeAnimation("O_Hiding");
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
        ChangeAnimation("O_Spawn", spawnStatesTime);
    }
    
    public void Thrown(float statesTime = 0.5f)
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canWalk = false;
            ChangeAnimation("O_Thrown",0f);
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
            FlipSprite();
        }

        
    }

    void CheckWalkingDi()
    {

        switch (currentDi)
        {
            case EnemyVerticalDirection.front:
                ChangeAnimation("O_Walk_F", 0.2f);
                break;

            default:
                ChangeAnimation("O_Walk_B", 0.2f);
                break;
        }

    }
    
    void FlipSprite()
    {
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            animator.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(aIPath.desiredVelocity.x <= -0.01f)
        {
            animator.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
