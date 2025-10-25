using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class MushroomBomb : MonoBehaviour
{
    [SerializeField] private bool isFriendlyFire = false;
    [SerializeField] protected int damage;
    [SerializeField] private float destroyDeley = 0.5f;
    void Start()
    {
        Destroy(gameObject, destroyDeley);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (isFriendlyFire)
        {
            IDamageable targets = col.GetComponent<IDamageable>();
            if (targets != null)
            {
                targets.getDamage(damage);
            }
        }
        else
        {
            if (col.CompareTag("Player"))
            {
                IDamageable targets = col.GetComponent<IDamageable>();
                targets.getDamage(damage);
            }
        }

    }
}
