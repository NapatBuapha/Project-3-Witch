using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class MushroomBomb : MonoBehaviour
{
    [SerializeField] protected int damage;
   [SerializeField] private float destroyDeley = 0.5f;
    void Start()
    {
        Destroy(gameObject, destroyDeley);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable targets = col.GetComponent<IDamageable>();
        if (targets != null)
        {
            targets.getDamage(damage);
        }
    }
}
