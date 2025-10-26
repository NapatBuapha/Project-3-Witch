using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BeastAttackArea : PlayerProjectile
{
    [SerializeField] private Collider2D col;
        public CinemachineImpulseSource impulseSource { get; private set; }
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        CameraShakeManager.instance.CameraShake(impulseSource);
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
