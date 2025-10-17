using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStatsData : MonoBehaviour
{
    #region MoveStats
    [Header("Walking")]
    public float baseW_Speed = 10f; //จะปรับค่า speed พื้นฐานใช้ตัวนี้เท่านั้น
    public float DiagonalSpeedReduction = 1.5f;

    public Rigidbody2D rb { get; private set; }

    [Header("Dash")]
    public float dashCD = 2f;
    public float baseDashPower = 10f;
    public float dashStatesTime = 0.3f;
    public float dashSta_Consume = 3f;

    #endregion

    #region Stats
    [Header("Health")]
    public int maxHp;
    public int hp;
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

        #endregion

        #region  setVaraible
        Stamina = 0;
        mana = 0;
        hp = maxHp;
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
