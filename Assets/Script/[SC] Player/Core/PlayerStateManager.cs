using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BasePlayerData))]
public class PlayerStateManager : MonoBehaviour
{


    PlayerBaseState currentState;
    [SerializeField] private string statsName;

    //Input Each State Here
    public State_PlayerIdle state_PlayerIdle { get; private set; } = new State_PlayerIdle();
    public State_PlayerWalking state_PlayerWalking { get; private set; } = new State_PlayerWalking();
    public State_PlayerDash state_PlayerDash { get; private set; } = new State_PlayerDash();
    public State_PlayerCasting state_PlayerCasting { get; private set; } = new State_PlayerCasting();

    public BasePlayerData stats { get; private set; }

    #region Walking Stats
    [HideInInspector] public float w_speed = 0f;
    [HideInInspector] public float player_HInput;
    [HideInInspector] public float player_VInput;
    #endregion

    #region Dash Stats
    [HideInInspector] public float dashPower;
    private bool canDash;
    
    #endregion


    #region StateCondition
    public bool isWalking { get; private set; }
    public bool dashInput { get; private set; }

    #endregion

    #region  SpellCasting
    public float castingDura;
    #endregion

    #region  Animation
    public PlayerAnimationController animaCon { get; private set; }
    #endregion


    void Awake()
    {
        #region Get the component Ref here
        stats = GetComponent<BasePlayerData>();
        animaCon = GetComponent<PlayerAnimationController>();
        #endregion

        #region Set the variable
        dashPower = stats.baseDashPower;
        w_speed = stats.base_Speed;
        canDash = true;
        #endregion
    }

    #region StateMachineZone
    void Start()
    {
        SwitchState(state_PlayerIdle);
        currentState.EnterState(this);
    }

    void Update()
    {
        #region Normal update code
        //For checking
        statsName = currentState.GetType().Name;

        //คำสั่งที่ไม่เกี่ยวกับ state โดยตรง
        // Get player Movement Input
        player_HInput = Input.GetAxis("Horizontal");
        player_VInput = Input.GetAxis("Vertical");

        //นับเวลา Dash

        //ลดความเร็วเวลาเดินเฉียง
        if (Mathf.Abs(player_HInput) > 0 && Mathf.Abs(player_VInput) > 0)
        {
            dashPower = stats.baseDashPower / stats.DiagonalSpeedReduction;
            w_speed = stats.base_Speed / stats.DiagonalSpeedReduction;
        }
        else
        {
            dashPower = stats.baseDashPower;
            w_speed = stats.base_Speed;
        }

        #endregion


        #region StateCondition
        isWalking = player_HInput != 0 || player_VInput != 0;
        dashInput = Input.GetKeyDown(KeyCode.LeftShift) && canDash && stats.Stamina > stats.dashSta_Consume;
        #endregion

        #region NonStateCondition

        #endregion

        //คำสั่งที่ใช้กับทุก State

        currentState.UpdateState(this);
    }


    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    #endregion

    #region Method Ref for Specific State
    public IEnumerator SetDashCoolDown()
    {
        canDash = false;
        yield return new WaitForSeconds(stats.dashCD);
        canDash = true;
    }

    public void Casting(float castingDura)
    {
        this.castingDura = castingDura;
        SwitchState(state_PlayerCasting);
    }
    

    #endregion
}
