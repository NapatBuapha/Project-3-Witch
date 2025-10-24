using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseEnemyData : BaseMobData , IDamageable
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector
    AIDestinationSetter aiFinder;
    [SerializeField] private int hp = 1;
    public float startMoveDistance = 10f;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        aiFinder = GetComponent<AIDestinationSetter>();
        aiFinder.target = GameObject.FindWithTag("Player").transform;

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
