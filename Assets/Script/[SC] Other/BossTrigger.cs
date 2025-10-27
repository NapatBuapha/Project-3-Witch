using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private Boss_01_StateManager boss;
    [SerializeField] private GameObject Wall;

    void Start()
    {
        Wall.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            boss.Appearing();
            Wall.SetActive(true);
        }
    }
}
