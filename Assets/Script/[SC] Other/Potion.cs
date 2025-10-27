using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private int healValue;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHpManager>().GainHealth(healValue);
            Destroy(gameObject);
        }
    }
}
