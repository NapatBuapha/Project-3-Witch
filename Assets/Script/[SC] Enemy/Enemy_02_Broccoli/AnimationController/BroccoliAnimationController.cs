using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;
using UnityEngine.Animations;

public class BroccoliAnimationController : MonoBehaviour
{
    private enum EnemyVerticalDirection
    {
        front,
        back,
    }
    [SerializeField] private Animator animator; //ดึง sprite จาก Game object sprite มาใส่ใน inspector
    string currentAnimation; //ชื่อ animation ปัจจุบัน
    Rigidbody2D rb;
    public AIPath aIPath;
    public bool canWalk;

    EnemyVerticalDirection currentDi;



    void Start()
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

        switch(spawnVariation)
        {
            case 1:
            ChangeAnimation("B_Spawn" , spawnStatesTime);
            break;
            default:
            ChangeAnimation("B_SpecialSpawn" , spawnStatesTime);
            break;
        }

        
        
    }
    
    public void ChangeAnimation(string animation, float crossfade = 0.2f, float time = 0)
    {
        //ในทุกๆ animation state จะตั้ง behabiour ไว้ให้เรียก method นี้พร้อมใส่เวลาของ State มา
        //เเละเมื่อเวลาหมด หรือ state นั้นมีเวลาน้อยมากๆถึงจะสั่งให้เปลี่ยน animation ได้
        if (time > 0) StartCoroutine(Wait());
        else validate();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time - crossfade);
            validate();
        }
        //ชื่อ animation ต้องตรงกับ animation ใน animator เท่านั้น
        void validate()
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                if (currentAnimation == "")
                {
                    UpdateDirection();
                }
                else
                {
                    animator.CrossFade(animation,crossfade);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
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

    void UpdateDirection()
    {
        if (aIPath.desiredVelocity.y >= 0.06f)
        {
            currentDi = EnemyVerticalDirection.back;
        }
        else if (aIPath.desiredVelocity.y <= 0.5)
        {
            currentDi = EnemyVerticalDirection.front;
        }
    }
    
}
