using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroupManager : MonoBehaviour
{
    [SerializeField] private BaseEnemyData[] enemies;

    void Awake()
    {
        enemies = GetComponentsInChildren<BaseEnemyData>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].startMoveDistance = 0;
        }
    }
    
    public void StartWave()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].startMoveDistance = 100;
        }
    }
}
