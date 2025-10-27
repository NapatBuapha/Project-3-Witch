using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;
using UnityEngine.Animations;

public class MushroomBossAnimationController : MonoBehaviour
{
    [SerializeField] protected Animator animator; //ดึง sprite จาก Game object sprite มาใส่ใน inspector
    protected string currentAnimation; //ชื่อ animation ปัจจุบัน


    bool canChageState;
    void Start()
    {
        //Component Ref

        //Variable Set
        ChangeAnimation("B_Hiding");
    }


    public void Spawn(float spawnStatesTime = 0.3f)
    {
        ChangeAnimation("B_Spawn");
    }

    public void Scream(float statesTime = 0.5f)
    {
        AudioManager.PlaySound(SoundType.Enemy_Boss_Roar);
        ChangeAnimation("B_Scream");
    }

    public void FloorStomp(float statesTime = 0.5f)
    {
        ChangeAnimation("B_FloorSmashing");
    }

    public void idle(float statesTime = 0.5f)
    {
        ChangeAnimation("B_Idle");
    }
    
    public void Hiding(float statesTime = 0.5f)
    {
        ChangeAnimation("B_Hiding2");
    }

    void Update()
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
                    
                }
                else
                {
                    animator.CrossFade(animation, crossfade);
                }
            }
        }
    }
    
}
