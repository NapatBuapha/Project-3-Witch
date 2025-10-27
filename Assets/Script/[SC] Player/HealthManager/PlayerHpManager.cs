using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHpManager : MonoBehaviour , IDamageable
{
    public int hp { get; private set; }
    public BasePlayerData stats{ get; private set; }

    //ช่วงเวลาอมตะ
    [SerializeField] float inviTime = 2f;
    bool isInvi;

    //variable สำหรับระบบ player เดินทะลุหลังโดนตี
    [SerializeField] LayerMask enemyLayer;


    //For on hit effect
    [SerializeField] private Animator animator;
    HealthUIManager hpUi;


    void Start()
    {
        //component ref
        stats = GetComponent<BasePlayerData>();
        hpUi = FindAnyObjectByType<HealthUIManager>();

        //set variable
        hp = stats.maxHp;
        isInvi = false;
        hpUi.UpdateHP();
    }


    public void getDamage(int damageValue)
    {
        //ถ้าเข้า beast mode ไมรับดาเมจ
        if(stats.isBeastMode)
        {
            return;
        }
        //ถ้าเป็นอมตะอยู่ ไม่รันcodeที่เหลือ
        if(isInvi)
        {
            return;
        }

        Debug.Log("Player Take Damage: " + damageValue);
        animator.SetTrigger("Hit");
        hp--;
        hpUi.UpdateHP();
        stats.filter.Hit();
        AudioManager.PlaySound(SoundType.Hit , 0.5f);
        CameraShakeManager.instance.CameraShake(stats.impulseSource);
        
        StartCoroutine(Invincible(inviTime));

        if (hp <= 0)
        {
            //Player Death
            Destroy(gameObject);
        }
    }


    public void PayHealth(int value)
    {
        // การจ่ายเลือด
        /*if (hp - value < 0)
        {
            hp = 1;
        }
        else
        {
            hp -= value;
        }*/

        Debug.Log("not doing anything cause i am lazy");
    }

    public void GainHealth(int value)
    {
        if (hp + value > stats.maxHp)
        {
            hp = stats.maxHp;
        }
        else
        {
            hp += value;
        }

        hpUi.UpdateHP();
    }

    IEnumerator Invincible(float inviTime)
    {
        Debug.Log("StartInvi");
        isInvi = true;
        animator.SetBool("IsIframe", true);
        //สั่งให้ collision ระหว่าง player กับ enemy ไม่ทำงาน
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Player"),
        LayerMask.NameToLayer("Enemy"),
        true);

        yield return new WaitForSeconds(inviTime);

        isInvi = false;
        animator.SetBool("IsIframe", false);
        //คืนค่าให้กลับมาทำงานอีกครั้ง
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Player"),
        LayerMask.NameToLayer("Enemy"),
        false);

        Debug.Log("EndInvi");
    }
    
    public void BeastPenalty()
    {
        stats.maxHp -= 1;
        hpUi.DestroyLastHearth();
        if (hp > stats.maxHp)
        {
            int dif = hp - stats.maxHp;
            hp -= dif;
            hpUi.UpdateHP();
        }
    }
}
