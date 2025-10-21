using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; //ดึง sprite จาก Game object sprite มาใส่ใน inspector
    string currentAnimation; //ชื่อ animation ปัจจุบัน
    Rigidbody2D rb;

    private enum PlayerVerticalDirection
    {
        front,
        back,
    }

    bool canWalk;

    PlayerVerticalDirection verticalDi;

    void Start()
    {
        
        ChangeAnimation("F_Dash");
        //Component Ref
        rb = GetComponent<Rigidbody2D>();

        //Variable Set
        verticalDi = PlayerVerticalDirection.back;
        canWalk = true;
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
                    UpdatePlayerDirection();
                }
                else
                {
                    animator.CrossFade(animation,crossfade);
                }
            }
        }
        
    }


    void Update()
    {
        if(canWalk)
        {
            UpdatePlayerDirection();
        }
        
        CheckIdle();

    }

    public void DashAnim(float dashDuration = 0.3f)
    {
        //ป้องกันการเปลี่ยน state ระหว่าง dash
        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            canWalk = false;
            yield return new WaitForSeconds(dashDuration);
            canWalk = true;
        }
        
        //ซ้าย เเละ ขวา
        if (MathF.Abs(rb.velocity.x) >= 0.01f)
        {
            switch (verticalDi)
            {
                case PlayerVerticalDirection.front:
                    ChangeAnimation("FS_Dash" , dashDuration);
                    break;

                default: //Case Back
                    ChangeAnimation("BS_Dash" , dashDuration);
                    break;
            }

        }

        if (rb.velocity.x == 0 && rb.velocity.y >= 0.01f)
        {
            //บน
            ChangeAnimation("B_Dash");
        }
        else if (rb.velocity.x == 0 && rb.velocity.y <= -0.01f)
        {
            //ล่าง
            ChangeAnimation("F_Dash");
        }
    }
    
    public void CastingAnim()
    {
        
    }

    void CheckIdle()
    {
        if(rb.velocity == Vector2.zero)
        {
            switch (verticalDi)
            {
                case PlayerVerticalDirection.front:
                    ChangeAnimation("F_Idle");
                    break;

                default: //Case Back
                    ChangeAnimation("B_Idle");
                    break;
            }
        }
    }

    void UpdatePlayerDirection()
    {
        //Check Horizontal
        if (rb.velocity.x >= 0.01f)
        {
            //ขวา
            animator.gameObject.transform.localScale = new Vector3(1, 1, 1); 
            CheckVertical();
        }
        else if (rb.velocity.x <= -0.01f)
        {
            //ซ้าย
            animator.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            CheckVertical();
        }

        if (rb.velocity.x == 0 && rb.velocity.y >= 0.01f)
        {
            //บน
            verticalDi = PlayerVerticalDirection.back;
            ChangeAnimation("B_Walking");
        }
        else if (rb.velocity.x == 0 && rb.velocity.y <= -0.01f)
        {
            //ล่าง
            verticalDi = PlayerVerticalDirection.front;
            ChangeAnimation("F_Walking");
        }
    }

    void CheckVertical()
    {
        //เช็คการเดินขึ้นลงอีกรอบ
        if (rb.velocity.y >= 0.01f)
        {
            //บน
            verticalDi = PlayerVerticalDirection.back;
        }
        else if (rb.velocity.y <= 0)
        {
            //ล่าง
            verticalDi = PlayerVerticalDirection.front;
        }

        switch(verticalDi)
        {
            case PlayerVerticalDirection.front:
            ChangeAnimation("FS_Walking");
            break;

            default: //Case Back
            ChangeAnimation("BS_Walking");
            break;
        }
        
    }
}

