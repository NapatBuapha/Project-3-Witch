using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mushroom_Gauntlet : MonoBehaviour
{
    Boss_01_StateManager mushroomBoss;
    [SerializeField] private GameObject[] waveVariant;
    GameObject currentWave;

    bool isStart;

    void Start()
    {
        mushroomBoss = GetComponent<Boss_01_StateManager>();
        isStart = false;
    }

    public void StartWave()
    {
        currentWave = Instantiate(waveVariant[UnityEngine.Random.Range(0, waveVariant.Length)], transform.position, quaternion.identity);
        MonsterGroupManager gm = currentWave.GetComponent<MonsterGroupManager>();

        gm.StartWave();
        isStart = true;
    }

    void Update()
    {
        if (!isStart)
        {
            return;
        }

        if(currentWave.transform.childCount <= 0)
        {
            EndWave();
            isStart = false;
        }
    }
    
    void EndWave()
    {
        mushroomBoss.ReAppearing();
    }

}
