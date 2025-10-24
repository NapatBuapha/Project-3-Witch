using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_04_Onion : BaseEnemyData
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector
    AIPath aIPath;
    

    //Stats for dash attack
    [Header("Thrown variable")]
    public float startThrownDistance = 0.5f;
    public float statesTime = 0.5f;
    public GameObject bombPrefab;

    public float delayed = 2f;

    [Header("Spawning state variable")]
    public float spawnStatesTime = 0.5f;

    [Header("Other variable")]
    public float startFleeDistance = 5f;
    protected override void Awake()
    {
        base.Awake();
        aIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        aIPath.maxSpeed = base_Speed;
        base.Update();
    }
}
