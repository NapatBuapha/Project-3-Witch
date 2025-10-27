using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderExplosion : PlayerProjectile
{
    [SerializeField] private float damagedDelayed = 0.15f;
    [SerializeField] private float destroyDeley = 0.5f;
    [SerializeField] private float stunProb = 20f;
    Collider2D col;
    void Start()
    {
         AudioManager.PlaySound(SoundType.Spell_LightnighBolt, 0.5f);
        col = GetComponent<Collider2D>();
        col.enabled = false;

        Invoke("OpenHB", damagedDelayed);
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

            int rng = Random.Range(0, 101);
            if (rng <= stunProb)
            {
                Debug.Log("Enemy stun");
                B_and_DB_Manager dB_Manager = col.GetComponent<B_and_DB_Manager>();
                dB_Manager.FindDBB("902");
            }
        }
    }
    
    public void OpenHB()
    {
        col.enabled = true;
    }
}
