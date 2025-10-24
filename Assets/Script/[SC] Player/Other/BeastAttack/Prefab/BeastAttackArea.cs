using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastAttackArea : PlayerProjectile
{
    [SerializeField] private Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        Destroy(gameObject, 1);
    }

    public void OpenCol()
    {
        col.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            IDamageable target = col.gameObject.GetComponent<IDamageable>();
            target.getDamage(damage);
        }     
    }
    
}
