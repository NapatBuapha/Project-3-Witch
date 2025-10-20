using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "New50_Calibur", menuName = "Spells/50_Calibur")]
public class Spell_50_Calibur : SpellBase
{
    #region ค่าทั่วไป
    [Header("Spell Value")]
    [SerializeField] private string spellID_ = "02";
    [SerializeField] private string thisName_ = "50_Calibur";
    [SerializeField] private int manaCost_ = 5;
    [SerializeField] private float maxCD_ = 10f;
    [SerializeField] private float castingDura_ = 1f;
    [SerializeField] private Sprite icon_;
    [SerializeField] [TextArea] private string desc_;


    #endregion

    #region ค่าสำหรับลูกระเบิด
    [Header("Bomb Effect Value")]
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private int hpCost;
    [SerializeField] private bool isSlow = true; //For thunderbolt Variation
    #endregion



    private void OnEnable()
    {
        init(spellID_, thisName_, manaCost_, maxCD_,castingDura_,icon_ ,desc_);
    }

    public override void UseSpell()
    {
        base.UseSpell();
        //หาตำเเหน่ง player กับ mouse
        Transform playerPos = player.transform;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        //สร้างระเบิด
        Instantiate(spellPrefab, mouseWorldPos, quaternion.identity);
        Penalty();
    }

    public override void Penalty()
    {
        base.Penalty();
        PlayerHpManager p_HP = player.GetComponent<PlayerHpManager>();

        if (p_HP.hp > hpCost)
            p_HP.PayHealth(hpCost);
        if(isSlow)
        {
            B_and_DB_Manager debuffManager = player.GetComponent<B_and_DB_Manager>();
            debuffManager.FindDBB("901");
        }
        
    }
}
