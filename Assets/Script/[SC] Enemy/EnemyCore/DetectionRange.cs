using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [SerializeField] private EnemyStateManager enemyStateManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            enemyStateManager.SwitchState(enemyStateManager.enemy_Chasing);
        }
    }
}
