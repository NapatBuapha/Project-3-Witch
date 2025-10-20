using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy_01 : BaseEnemyData
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector
    AIPath aIPath;
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
