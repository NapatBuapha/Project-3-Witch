using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpManager : MonoBehaviour , IDamageable
{
    public int hp { get; private set; }
    BasePlayerData stats;

    //ช่วงเวลาอมตะ
    [SerializeField] float inviTime = 2f;
    bool isInvi;

    //variable สำหรับระบบ player เดินทะลุหลังโดนตี
    [SerializeField] LayerMask enemyLayer;

    void Start()
    {
        //component ref
        stats = GetComponent<BasePlayerData>();

        //set variable
        hp = stats.maxHp;
        isInvi = false;
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
        hp -= damageValue;
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
        if (hp - value < 0)
        {
            hp = 1;
        }
        else
        {
            hp -= value;
        }
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
    }
    
    IEnumerator Invincible (float inviTime)
    {
        Debug.Log("StartInvi");
        isInvi = true;
        //สั่งให้ collision ระหว่าง player กับ enemy ไม่ทำงาน
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Player"),
        LayerMask.NameToLayer("Enemy"),
        true);

        yield return new WaitForSeconds(inviTime);

        isInvi = false;
        //คืนค่าให้กลับมาทำงานอีกครั้ง
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Player"),
        LayerMask.NameToLayer("Enemy"),
        false);

         Debug.Log("EndInvi");
    }
}
