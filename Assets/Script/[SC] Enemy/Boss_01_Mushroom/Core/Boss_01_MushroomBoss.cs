using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss_01_MushroomBoss : BaseMobData , IDamageable
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector

    [Header("Spawning Variable")]
    public float state_Spawn_Dura = 3.5f;

    [Header("Smash Variable")]
    public float state_Stomp_Dura = 1f;

    [Header("Scream Variable")]
    public float state_Scream_Dura = 0.5f;
    public float state_Hiding_Dura = 1f;

    public int hpThreshHold1, hpThreshHold2;

    public int screamToken;

    public Mushroom_Gauntlet mushroom_Gauntlet { get; private set; }
    private Dictionary<int, bool> screamThreshold = new Dictionary<int, bool>();


    [Header("Other")]
    public float actionDelayed = 5;

    [SerializeField] private Animator animator;

    [Header("Health Value")]
    public bool isInvi;
    public float hp;
    

    void Awake()
    {

        //Component Ref
        mushroom_Gauntlet = GetComponent<Mushroom_Gauntlet>();


        //Variable Set
        hp = maxHp;
        screamThreshold.Add(1, true);
        screamThreshold.Add(2, true);
    }

    void Update()
    {
        
    }

    public void getDamage(int damageValue)
    {
        if(isInvi)
        {
            return;
        }

        AudioManager.PlaySound(SoundType.Hit);
        hp -= damageValue;
        animator.SetTrigger("Hit");

        if (hp <= hpThreshHold1 && screamThreshold[1])
        {
            screamThreshold[1] = false;
            screamToken++;
        }

        if (hp <= hpThreshHold2 && screamThreshold[2])
        {
            screamThreshold[2] = false;
            screamToken++;
        }
        
        if(hp <= 0)
        {
            //ตอนตาย
            Destroy(gameObject);
        }
    }
}
