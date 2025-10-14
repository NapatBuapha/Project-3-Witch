using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDebuffSlow", menuName = "Debuff and Buff/Slow Debuff")]
public class Debuff_Slow : Debuff_Buff_Base
{
    //ถ้าเป็น debuff id ต้องเริ่มต้นด้วย 9 เช่น 901 902 903
    //สำหรับ buff id ต้องเริ่มต้นด้วย 8 เช่น 801 802 803

    #region ค่า spell base
    [Header("Base Varaible")]
    [SerializeField] private string buffId_ = "901"; 
    [SerializeField] private string buffName_ = "Slow";
    [SerializeField] private float maxDura_ = 5f;

    #endregion

    #region ค่าเฉพาะของ buff
    [Header("Slow Debuff Varaible")]
    //ค่าเป็น % ห้ามใส่เกิน 1
    [SerializeField] private float slowness = 0.99f;
    private float baseSpeed;
    private PlayerStatsData stats;
    
    #endregion

    void OnEnable()
    {
        Init(buffId_, buffName_, maxDura_);
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        stats = player.GetComponent<PlayerStatsData>();
        //เก็บค่า speed ก่อนติด slow ไว้ใน playerBaseSpeed
        baseSpeed = stats.baseW_Speed;

        //Slow player
        stats.baseW_Speed *= slowness;
    }

    public override void OnEffectEnd()
    {
        base.OnEffectEnd();
        //จบการทำงานของ debuff
        stats.baseW_Speed = baseSpeed;
    }

}
