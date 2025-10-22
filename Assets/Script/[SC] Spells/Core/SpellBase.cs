using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpellBase: ScriptableObject
{
    public string spellID { get; private set; }
    public string name_ { get; private set; }
    public int manaCost { get; private set; }
    public float maxCD { get; private set; }

    public float castingDura { get; private set; }
    public Sprite icon { get; private set; }
    public string desc { get; private set; }

    protected GameObject player;
    protected SpellBook spellbookref;
    // constructor สำหรับยัดค่า [for child]
    public SpellBase()
    {}
    protected void init(string spellID, string name_, int manaCost, float maxCD , float castingDura ,Sprite icon , string desc)
    {
        this.spellID = spellID;
        this.name_ = name_;
        this.manaCost = manaCost;
        this.maxCD = maxCD;
        this.castingDura = castingDura;
        this.icon = icon;
        this.desc = desc;
    }

    public virtual void BeforeCasting()
    {
        player = GameObject.FindWithTag("Player");
        spellbookref = player.GetComponentInChildren<SpellBook>();
        Debug.Log($"{name_} , {manaCost} , {maxCD}");
    }

    public virtual void UseSpell()
    {
        
    }
    public virtual void Penalty()
    { }

    
    
}
