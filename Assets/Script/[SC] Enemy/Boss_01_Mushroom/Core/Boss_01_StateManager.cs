using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using UnityEditor;

public class Boss_01_StateManager : MonoBehaviour
{
    MushroomBossBaseState currentState;
    //Input Unique States here
    public MushroomBoss_BeforeSpawn state_BeforeSpawn { get; private set; } = new MushroomBoss_BeforeSpawn();
    public MushroomBoss_Spawning state_Spawning { get; private set; } = new MushroomBoss_Spawning();
    public MushroomBoss_Idle state_Idle { get; private set; } = new MushroomBoss_Idle();
    public MushroomBoss_GroundAttack state_GroundAttack { get; private set; } = new MushroomBoss_GroundAttack();
    public MushroomBoss_Scream state_Scream { get; private set; } = new MushroomBoss_Scream();
    public MushroomBoss_Hide state_Hide { get; private set; } = new MushroomBoss_Hide();


    //Component ref
    public Boss_01_MushroomBoss stats { get; private set; }
    public MushroomBossAnimationController aController { get; private set; }
    //Player Ref
    private GameObject player;

    //Stats Condition


    //DashAttackAdjustment


    void Start()
    {
        #region component ref
        aController = GetComponent<MushroomBossAnimationController>();
        stats = GetComponent<Boss_01_MushroomBoss>();
        #endregion

        #region set variable

        #endregion


        SwitchState(state_BeforeSpawn);

        
        currentState.EnterState(this);
    }

    void Update()
    {
        //หาตำเเหน่งเเละระยาห่างของ player

        //StatesCondition
        

        currentState.UpdateState(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(MushroomBossBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }



    public void Appearing()
    {
        SwitchState(state_Spawning);
        aController.Spawn();

        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.state_Spawn_Dura);
            BackToIdle();
        }
    }

    public void ReAppearing()
    {
        stats.actionDelayed /= 2;
        Appearing();
    }

    public void BackToIdle()
    {
        SwitchState(state_Idle);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.actionDelayed);
            if (stats.screamToken > 0)
            {
                Scream();
            }
            else
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        SwitchState(state_GroundAttack);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.state_Stomp_Dura);
            BackToIdle();
        }
    }

    public void Scream()
    {
        AudioManager.PlaySound(SoundType.Enemy_Boss_Roar);
        SwitchState(state_Scream);
        stats.screamToken--;
        stats.mushroom_Gauntlet.StartWave();

        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(stats.state_Stomp_Dura);
            SwitchState(state_Hide);
        }
    }

}
