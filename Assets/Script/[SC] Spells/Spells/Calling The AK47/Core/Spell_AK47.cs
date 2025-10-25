using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "NewAK47_Magic", menuName = "Spells/AK47_Magic")]
public class Spell_AK47 : SpellBase
{

    #region ค่าทั่วไป
    [Header("Spell Value")]
    [SerializeField] private string spellID_ = "03";
    [SerializeField] private string thisName_ = "AK47_Magic";
    [SerializeField] private int manaCost_ = 40;
    [SerializeField] private float maxCD_ = 20f;
    [SerializeField] private float castingDura_ = 0.25f;
    [SerializeField] private Sprite icon_;
    [SerializeField] [TextArea] private string desc_;
    #endregion

    [Header("Penalty value")]
    [SerializeField] private int hpCost;



    private void OnEnable()
    {
        init(spellID_, thisName_, manaCost_, maxCD_, castingDura_,icon_,desc_);
    }

    public override void UseSpell()
    {
        SpellBook spellBook = FindAnyObjectByType<SpellBook>();
        spellBook.ChangeState(2);
        base.UseSpell();
        Penalty();
    }

    public override void Penalty()
    {
        PlayerHpManager p_HP = player.GetComponent<PlayerHpManager>();

        p_HP.PayHealth(hpCost);
        B_and_DB_Manager debuffManager = player.GetComponent<B_and_DB_Manager>();
        debuffManager.FindDBB("801");
    }
}

