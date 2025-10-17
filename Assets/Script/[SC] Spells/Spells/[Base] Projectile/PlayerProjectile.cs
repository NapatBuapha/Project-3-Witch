using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected int damage;
    // Start is called before the first frame update
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            IDamageable target = col.gameObject.GetComponent<IDamageable>();
            target.getDamage(damage);

            Destroy(gameObject);
        }

        if (!col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }       
    }
}
