using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasePlayerData : BaseMobData
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector

    #region MoveStats
    [Header("Walking")]

    public float DiagonalSpeedReduction = 1.5f;

    public Rigidbody2D rb { get; private set; }
    public BeastModeManager beastModeManager { get; private set; }
    public SpellBook spellBook { get; private set; }

    [Header("Dash")]
    public float dashCD = 2f;
    public float baseDashPower = 10f;
    public float dashStatesTime = 0.3f;
    public float dashSta_Consume = 3f;

    #endregion

    #region Stats

    [Header("Stamina")]
    public float maxStamina = 10f;
    private float stamina;
    public float Stamina
    {
        get { return stamina; }
        set
        {
            if (value > maxStamina)
            {
                stamina = maxStamina;
            }
            else
            {
                stamina = value;
            }
        }
    }
    [SerializeField] private float rechargeSpeedSta = 0.03f;


    [Header("Beast Mode")]
    public float transformDura = 2.2f;
    public float attackDura = 0.75f;

    public float beastModeDura = 13f;
    public bool isBeastMode;
    



    [Header("Mana")]
    public float maxMana = 10f;
    private float mana;
    public float Mana
    {
        get { return mana; }
        set
        {
            if (value > maxMana)
            {
                mana = maxMana;
            }
            else
            {
                mana = value;
            }
        }
    }
    [SerializeField] private float rechargeSpeedMana = 0.01f;
    #endregion




    void Awake()
    {
        #region get Component ref
        rb = GetComponent<Rigidbody2D>();
        beastModeManager = FindAnyObjectByType<BeastModeManager>();
        spellBook = FindAnyObjectByType<SpellBook>();
        #endregion

        #region  setVaraible
        Stamina = 0;
        mana = 0;
        isBeastMode = false;
        #endregion
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (stamina < maxStamina)
        {
            Stamina += rechargeSpeedSta;
        }
        
        if (mana < maxMana)
        {
            Mana += rechargeSpeedMana;
        }
    }
}
