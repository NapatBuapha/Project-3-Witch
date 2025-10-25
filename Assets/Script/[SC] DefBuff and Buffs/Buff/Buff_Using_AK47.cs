using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewUsing_AK47", menuName = "Debuff and Buff/Using_AK47 buff")]
public class Buff_Using_AK47 : Debuff_Buff_Base
{
    //ถ้าเป็น debuff id ต้องเริ่มต้นด้วย 9 เช่น 901 902 903
    //สำหรับ buff id ต้องเริ่มต้นด้วย 8 เช่น 801 802 803

    #region ค่า Bebuff base
    [Header("Base Varaible")]
    [SerializeField] private string buffId_ = "01";
    [SerializeField] private string buffName_ = "AK47";
    [SerializeField] private float maxDura_ = 12f;
    #endregion


    #region ค่าเฉพาะของ buff
    [Header("AK47 Debuff Varaible")]
    //ค่าเป็น % ห้ามใส่เกิน 1
    [SerializeField] private float slowness = 0.5f;
    private float baseSpeed;
    //ref สำหรับเวทย์
    [SerializeField] private PlayerSpellSlot pSpellSlot;
    [SerializeField] private SpellBase spellAK47ref;
    [SerializeField] private SpellBase gunSpellRef;
    private int baseSpellIndex;
    protected GameObject player;

    #endregion



    void OnEnable()
    {
        Init(buffId_, buffName_, maxDura_);
    }

    public override void ApplyEffect(BaseMobData mob)
    {
        base.ApplyEffect(mob);
        player = mob.gameObject;
        //เก็บค่า speed ก่อนติด slow ไว้ใน playerBaseSpeed
        baseSpeed = mob.base_Speed;

        //Slow player
        mob.base_Speed *= slowness;


        //เปลี่ยน spell เรียก ak47 เป็นการยิงกระสุนปืนจากนั้นสั่งล็อคไม่ให้ใช้ spell อื่น
        pSpellSlot = player.GetComponent<PlayerSpellSlot>();
        for (int i = 0; i < pSpellSlot.spellslot.Length; i++)
        {
            if (pSpellSlot.spellslot[i] == spellAK47ref)
            {

                Debug.Log("found spell");
                baseSpellIndex = i;
                pSpellSlot.canChangeSpell = false;
                pSpellSlot.spellslot[i] = gunSpellRef;
                pSpellSlot.slotCD[i] = 0;
            }
        }
    }

    public override void OnEffectEnd(BaseMobData mob)
    {
        base.OnEffectEnd(mob);
        //จบการทำงานของ debuff
        SpellBook spellBook = FindAnyObjectByType<SpellBook>();
        spellBook.ChangeState(0);

        mob.base_Speed = baseSpeed;
        pSpellSlot.canChangeSpell = true;
        pSpellSlot.slotCD[baseSpellIndex] = spellAK47ref.maxCD;
        pSpellSlot.spellslot[baseSpellIndex] = spellAK47ref;
    }

}
