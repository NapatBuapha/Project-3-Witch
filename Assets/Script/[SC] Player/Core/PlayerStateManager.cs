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

    //Beast State here
    public State_PlayerBeastTransform state_PlayerBeastTransform { get; private set; } = new State_PlayerBeastTransform();
    public State_PlayerBeastIdle state_PlayerBeastIdle { get; private set; } = new State_PlayerBeastIdle();
    public State_PlayerBeastWalking state_PlayerBeastWalking { get; private set; } = new State_PlayerBeastWalking();
    public State_PlayerDeTransform state_PlayerDeTransform { get; private set; } = new State_PlayerDeTransform();
    public State_PlayerBeastAttack state_PlayerBeastAttack { get; private set; } = new State_PlayerBeastAttack();

    //Components
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
    public bool transformCon { get; private set; }
    public bool AttackCon { get; private set; }

    #endregion

    #region  SpellCasting
    public float castingDura;
    #endregion

    #region  Animation
    public PlayerAnimationController animaCon { get; private set; }
    #endregion

    #region BeastAttackAdjustment
    private float nearEndDecreaser = 1;
    private bool isNearDeTransform;
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
        isNearDeTransform = true;
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
        transformCon = Input.GetKeyDown(KeyCode.LeftControl) && !stats.isBeastMode && stats.beastModeManager.isBeastMode_Able;
        AttackCon = Input.GetMouseButton(0) && !isNearDeTransform;
        
        #endregion

        #region NonStateCondition

        #endregion

        //คำสั่งที่ใช้กับทุก State
        if(transformCon)
        {
            SwitchState(state_PlayerBeastTransform);
        }

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


    #region BeastState

    public void BeastTransform()
    {
        stats.rb.isKinematic = true;
        AudioManager.PlaySound(SoundType.Player_Transform , 1f);
        stats.filter.EnterBeast();
        stats.spellBook.ChangeState(1);
        stats.isBeastMode = true;
        animaCon.BeastModeTransform(stats.transformDura);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.transformDura);
            stats.rb.isKinematic = false;
            SwitchState(state_PlayerBeastIdle);
            StartCoroutine(BeastModeTimer());
            StartCoroutine(AbleToAttackTimer());
        }

        IEnumerator AbleToAttackTimer()
        {
            isNearDeTransform = false;
            yield return new WaitForSeconds(stats.beastModeDura - nearEndDecreaser);
            isNearDeTransform = true;
        }

        IEnumerator BeastModeTimer()
        {
            yield return new WaitForSeconds(stats.beastModeDura);
            SwitchState(state_PlayerDeTransform);
        }
    }

    public void BeastDeTransform()
    {
        stats.rb.isKinematic = true;
        AudioManager.PlaySound(SoundType.Player_DeTransform , 1f);
        stats.filter.EndBeast();
        animaCon.BeastModeDeTransform(stats.transformDura);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            stats.rb.isKinematic = false;
            yield return new WaitForSeconds(stats.transformDura);
            SwitchState(state_PlayerIdle);
            stats.isBeastMode = false;
            stats.beastModeManager.ResetBeastCount();
            stats.spellBook.ChangeState(0);
        }
    }
    
    public void BeastAttack()
    {
        animaCon.BeastModeAttack(stats.attackDura);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.attackDura);
            SwitchState(state_PlayerBeastIdle);
        }
    }
    

    #endregion
}
