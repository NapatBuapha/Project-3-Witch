using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_04_StateManager : MonoBehaviour
{
    OnionBaseStates currentState;
    //Input Unique States here
    public Onion_IdleState state_Idle { get; private set; } = new Onion_IdleState();
    public Onion_ChasingState state_Chasing { get; private set; } = new Onion_ChasingState();
    public Onion_RunAwayState state_RunAway { get; private set; } = new Onion_RunAwayState();
    public Onion_ThrownState state_Thrown { get; private set; } = new Onion_ThrownState();

    //Component ref
    [HideInInspector] public Enemy_04_Onion stats;
    public AIPath pathfinder { get; private set; }
    public AIDestinationSetter aitarget { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public OnionAnimationController aController { get; private set; }

    //Player Ref
    public GameObject player { get; private set; }
    [HideInInspector] public float pDistance { get; private set; }
    [HideInInspector] public Vector2 pDirection { get; private set; }

    //Stats Condition
    public bool thrownCon { get; private set; }
    public bool chaseCon { get; private set; }
    public bool fleeCon { get; private set; }

    //Run Away from Player
    public Transform fleePoint;

    bool canThrown;



    void Start()
    {
        #region component ref
        stats = GetComponent<Enemy_04_Onion>();
        pathfinder = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        aController = GetComponent<OnionAnimationController>();
        aitarget = GetComponent<AIDestinationSetter>();
        #endregion

        #region set variable
        player = GameObject.FindWithTag("Player");
        pathfinder.canMove = false;
        canThrown = true;
        #endregion


        SwitchState(state_Idle);
        currentState.EnterState(this);
    }

    void Update()
    {
        //หาตำเเหน่งเเละระยาห่างของ player
        pDistance = Vector2.Distance(player.transform.position, transform.position);
        pDirection = (player.transform.position - transform.position).normalized;

        //StatesCondition
        thrownCon = pDistance < stats.startThrownDistance && canThrown;
        chaseCon = pDistance < stats.StartMoveDistance;
        fleeCon = pDistance < stats.startFleeDistance;
        

        currentState.UpdateState(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(OnionBaseStates state)
    {
        currentState = state;
        state.EnterState(this);
    }


    //Specific Method For Specific States
    public void Appearing()
    {
        aController.Spawn(stats.spawnStatesTime);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.spawnStatesTime);
            SwitchState(state_Chasing);
        }
    }

    public void Thrown()
    {
        aController.Thrown(stats.statesTime);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            canThrown = false;
            yield return new WaitForSeconds(stats.delayed);
            canThrown = true;
        }

        StartCoroutine(endThrown());
        IEnumerator endThrown()
        {
            yield return new WaitForSeconds(stats.statesTime);
            SwitchState(state_Chasing);
        }

    }
}
