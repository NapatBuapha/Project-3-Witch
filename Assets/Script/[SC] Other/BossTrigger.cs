using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private Boss_01_StateManager boss;
    [SerializeField] private GameObject Wall;
    [SerializeField] private AudioClip battleBGM;
    [SerializeField] private AudioSource bGManager;

    void Start()
    {
        Wall.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            bGManager.clip = battleBGM;
            bGManager.Play();
            boss.Appearing();
            Wall.SetActive(true);
            Destroy(gameObject);
        }
    }
}
