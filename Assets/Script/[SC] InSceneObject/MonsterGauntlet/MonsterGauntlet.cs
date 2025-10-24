using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGauntlet : MonoBehaviour
{
    [SerializeField] private GameObject[] bushwWall;
    [SerializeField] private MonsterGroupManager[] waves;

    private bool isStart;
    [SerializeField] private int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < bushwWall.Length; i++)
        {
            bushwWall[i].SetActive(false);
        }
        currentWave = 0;
        isStart = false;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isStart)
            return;

        if(col.CompareTag("Player"))
        {
            for(int i = 0; i < bushwWall.Length; i++)
            {
                isStart = true;
                bushwWall[i].SetActive(true);
                StartNextWave();
            }
        }
        
    }

    void Update()
    {
        if(waves[currentWave].transform.childCount == 0)
        {
            currentWave++;
            if (currentWave < waves.Length)
            {
                StartNextWave();
            }
            else
            {
                EndGauntlet();
            }
        }
    }

    void StartNextWave()
    {
        waves[currentWave].StartWave();
    }
    
    void EndGauntlet()
    {
        for (int i = 0; i < bushwWall.Length; i++)
        {
            bushwWall[i].SetActive(false);
        }
        Destroy(gameObject);
    }
}
