using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] private bool isBullet;
    // Start is called before the first frame update
    void Start()
    {
        if (!isBullet)
            AudioManager.PlaySound(SoundType.Spell_Fireball, 0.2f);
        else
             AudioManager.PlaySound(SoundType.Spell_AKBullet, 0.2f);
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            IDamageable target = col.gameObject.GetComponent<IDamageable>();
            target.getDamage(damage);

            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }       
    }
}
