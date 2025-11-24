using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    public State_PlayerDying state_PlayerDying {get; private set;} = new State_PlayerDying();

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
    public DashDisplayer dashDisplayer {get ; private set;}
    
    #endregion


    #region StateCondition
    public bool isWalking { get; private set; }
    public bool dashInput { get; private set; }

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
        dashDisplayer = FindAnyObjectByType<DashDisplayer>();
        #endregion

        #region Set the variable
        dashPower = stats.baseDashPower;
        w_speed = stats.base_Speed;
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

        #endregion


        #region StateCondition
        isWalking = player_HInput != 0 || player_VInput != 0;
        dashInput = Input.GetKeyDown(KeyCode.LeftShift) && stats.canDash;
        AttackCon = Input.GetMouseButton(0) && !isNearDeTransform;
        
        #endregion

        #region NonStateCondition

        #endregion

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
        stats.canDash = false;
        yield return new WaitForSeconds(stats.dashCD);
        dashDisplayer.DashReady();
        stats.canDash = true;
    }

    public void Casting(float castingDura)
    {
        this.castingDura = castingDura;
        SwitchState(state_PlayerCasting);
    }

    public void Dying()
    {
        stats.rb.isKinematic = true;
        SwitchState(state_PlayerDying);
    }

    #endregion


    #region BeastState

    public void BeastTransform()
    {
        stats.rb.isKinematic = true;
        AudioManager.PlaySound(SoundType.Player_Transform , 0.1f);
        BeastBoarderFilter.instance.CallFilter();
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
        AudioManager.PlaySound(SoundType.Player_DeTransform , 0.1f);
        BeastBoarderFilter.instance.DisableFilter();
        stats.filter.EndBeast();
        animaCon.BeastModeDeTransform(stats.transformDura);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.transformDura);
            SwitchState(state_PlayerIdle);
            stats.isBeastMode = false;
            stats.beastModeManager.ResetBeastCount();
            stats.spellBook.ChangeState(0);
            stats.rb.isKinematic = false;
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
