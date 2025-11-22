using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cinemachine;
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
    public bool canDash;
    public float baseDashPower = 10f;
    public float dashStatesTime = 0.3f;

    #endregion

    #region Stats
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
            if(mana < 0)
            {
                mana = 0;
            }
            else
            {
                mana = value;
            }
        }
    }
    [SerializeField] private float rechargeSpeedMana = 0.01f;
    #endregion

    //Other
    public Filter filter { get; private set; }
    public CinemachineImpulseSource impulseSource { get; private set; }




    void Awake()
    {
        #region get Component ref
        rb = GetComponent<Rigidbody2D>();
        beastModeManager = FindAnyObjectByType<BeastModeManager>();
        spellBook = FindAnyObjectByType<SpellBook>();
        filter = FindAnyObjectByType<Filter>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        #endregion

        #region  setVaraible
        mana = maxMana;
        isBeastMode = false;
        canDash = true;
        #endregion
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        
        if (mana < maxMana)
        {
            Mana += rechargeSpeedMana;
        }
    }
}
