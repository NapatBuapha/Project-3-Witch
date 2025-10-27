using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class MushroomBomb : MonoBehaviour
{
    [SerializeField] private bool isFriendlyFire = false;
    [SerializeField] protected int damage;
    [SerializeField] private float destroyDeley = 0.5f;
    public CinemachineImpulseSource impulseSource { get; private set; }
    void Start()
    {
        AudioManager.PlaySound(SoundType.Explosive , 0.2f);
        impulseSource = GetComponent<CinemachineImpulseSource>();
        CameraShakeManager.instance.CameraShake(impulseSource);
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
