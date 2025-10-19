using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : BaseEnemy
{
    [SerializeField] private string eName_;
    [SerializeField] private int maxHp_;
    protected override void Awake()
    {
        Init(eName_, maxHp_);
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
