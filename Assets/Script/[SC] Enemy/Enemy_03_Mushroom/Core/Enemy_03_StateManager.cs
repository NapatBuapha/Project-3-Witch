using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_03_StateManager : MonoBehaviour
{
    MushroomBaseState currentState;
    //Input Unique States here
    public Mushroom_IdleState state_Idle { get; private set; } = new Mushroom_IdleState();
    public Mushroom_ChasingState state_Chasing { get; private set; } = new Mushroom_ChasingState();
    public Mushroom_ExplodingState state_Exploding { get; private set; } = new Mushroom_ExplodingState();


    //Component ref
    [HideInInspector] public Enemy_03_Mushroom stats;
    public AIPath pathfinder { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public MushroomAnimationController aController { get; private set; }

    //Player Ref
    private GameObject player;
    [HideInInspector] public float pDistance { get; private set; }
    [HideInInspector] public Vector2 pDirection { get; private set; }

    //Stats Condition
    public bool ExplodingCon { get; private set; }
    public bool chaseCon { get; private set; }



    void Start()
    {
        #region component ref
        stats = GetComponent<Enemy_03_Mushroom>();
        pathfinder = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        aController = GetComponent<MushroomAnimationController>();
        #endregion

        #region set variable
        player = GameObject.FindWithTag("Player");
        pathfinder.canMove = false;
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
        ExplodingCon = pDistance < stats.startExplodingDistance;
        chaseCon = pDistance < stats.StartMoveDistance;
        

        currentState.UpdateState(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(MushroomBaseState state)
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

    public void Explode()
    {
        aController.Explode(stats.explodingStatesTime);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.explodingStatesTime);
            Destroy(gameObject);
        }

    }



}
