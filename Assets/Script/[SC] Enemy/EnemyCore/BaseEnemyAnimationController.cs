using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseEnemyAnimationController : MonoBehaviour
{
    protected enum EnemyVerticalDirection
    {
        front,
        back,
    }

     [SerializeField] protected Animator animator; //ดึง sprite จาก Game object sprite มาใส่ใน inspector
    protected string currentAnimation; //ชื่อ animation ปัจจุบัน
    protected Rigidbody2D rb;
    public AIPath aIPath;
    public bool canWalk;
    
    protected EnemyVerticalDirection currentDi;

    protected virtual void Start()
    {
        //Component Ref
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();

        //Variable Set
        currentDi = EnemyVerticalDirection.front;
        canWalk = false;
    }
    protected virtual void Update()
    {

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
                    animator.CrossFade(animation, crossfade);
                }
            }
        }
    }
    
    protected void UpdateDirection()
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
