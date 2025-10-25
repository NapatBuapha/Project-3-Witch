using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float repeatRate = 0.5f;
    [SerializeField] private GameObject prefab;
    void Start()
    {
        Destroy(gameObject, 1f);
        InvokeRepeating("CreateExplosive" ,repeatRate,repeatRate);
    }

    void CreateExplosive()
    {
        Instantiate(prefab, transform.position, quaternion.identity);
    }
}

