using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliburExplosion : PlayerProjectile
{
    [SerializeField] private float destroyDeley = 0.5f;
    void Start()
    {
        AudioManager.PlaySound(SoundType.Spell_50Cal ,0.75f);
        Destroy(gameObject, destroyDeley);
    }
    // Start is called before the first frame update
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
