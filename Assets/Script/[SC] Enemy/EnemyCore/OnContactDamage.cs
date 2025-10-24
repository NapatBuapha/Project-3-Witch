using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OnContactDamage : MonoBehaviour
{
    [SerializeField] private int damageValue;
    public bool isHiding;


    void OnCollisionEnter2D(Collision2D col)
    {
        if(isHiding)
        {
            return;
        }

        if (col.collider.CompareTag("Player"))
        {
            IDamageable damageable = col.collider.GetComponent<IDamageable>();
            damageable.getDamage(damageValue);

        }
    }
}
