using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_02_StateManager : MonoBehaviour
{
    BroccoliBaseState currentState;
    //Input Unique States here
    public Broccoli_IdleStates states_Idle { get; private set; } = new Broccoli_IdleStates();
    public Broccoli_ChasingStates states_Chasing { get; private set; } = new Broccoli_ChasingStates();
    public Broccoli_DashAttack states_DashAttack { get; private set; } = new Broccoli_DashAttack();

    //Component ref
    [HideInInspector] public Enemy_02_Broccoli stats;
    public AIPath pathfinder { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public BroccoliAnimationController aController { get; private set; }

    //Player Ref
    private GameObject player;
    [HideInInspector] public float pDistance { get; private set; }
    [HideInInspector] public Vector2 pDirection { get; private set; }

    //Stats Condition
    public bool dashStateCon { get; private set; }
    public bool chaseCon { get; private set; }

    //DashAttackAdjustment
    public bool canDash;


    void Start()
    {
        #region component ref
        stats = GetComponent<Enemy_02_Broccoli>();
        pathfinder = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        aController = GetComponent<BroccoliAnimationController>();
        #endregion

        #region set variable
        player = GameObject.FindWithTag("Player");
        pathfinder.canMove = false;
        canDash = true;
        #endregion


        SwitchState(states_Idle);
        currentState.EnterState(this);
    }

    void Update()
    {
        //หาตำเเหน่งเเละระยาห่างของ player
        pDistance = Vector2.Distance(player.transform.position, transform.position);
        pDirection = (player.transform.position - transform.position).normalized;

        //StatesCondition
        dashStateCon = pDistance < stats.startDashDistance && canDash;
        chaseCon = pDistance < stats.StartMoveDistance;
        

        currentState.UpdateState(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(BroccoliBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }


    //Specific Method For Specific States
    public void StartTimeDelay()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canDash = false;
            yield return new WaitForSeconds(stats.delayed);
            canDash = true;
        }
    }

    public void Appearing()
    {
        aController.Spawn(stats.spawnStatesTime);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.spawnStatesTime);
            SwitchState(states_Chasing);
        }
    }

}
