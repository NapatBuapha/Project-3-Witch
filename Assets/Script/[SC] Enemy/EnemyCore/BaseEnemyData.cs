using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyData : BaseMobData , IDamageable
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector
    [SerializeField] private int hp = 1;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        hp = maxHp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(hp <= 0)
        {
            Debug.Log($"{name_} has been defeated");
            Destroy(gameObject);
        }
    }
    
    public void getDamage(int damageValue)
    {
        hp -= damageValue;
    }
}
