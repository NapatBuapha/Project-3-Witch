using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_02_Broccoli : BaseEnemyData
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector
    AIPath aIPath;

    //Stats for dash attack
    [Header("Sword Dash variable")]
    public float dashPower = 15f;
    public float dashStatesTime = 0.5f;
    public float startDashDistance = 5f;
    //DashAttackAdjustment
    public float delayed = 2f;

    [Header("Spawning state variable")]
     public float StartMoveDistance = 10f;
    public float spawnStatesTime = 0.5f;
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
