using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour , IDamageable
{
    private string eName;
    private int maxHp;
    public float speed { get; private set;}
    [SerializeField] private int hp;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        gameObject.tag = "Enemy";
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

    protected void Init(string eName, int maxHp, float speed)
    {
        this.eName = eName;
        this.maxHp = maxHp;
        this.speed = speed;
    }
    
    public void getDamage(int damageValue)
    {
        hp -= damageValue;
    }
}
