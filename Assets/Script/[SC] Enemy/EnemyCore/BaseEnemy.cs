using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour , IDamageable
{
    private string eName;
    private int maxHp;
    [SerializeField] private int hp;
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
            Debug.Log($"{eName} has been defeated");
            Destroy(gameObject);
        }
    }

    protected void Init(string eName, int maxHp)
    {
        this.eName = eName;
        this.maxHp = maxHp;
    }
    
    public void getDamage(int damageValue)
    {
        hp -= damageValue;
    }
}
