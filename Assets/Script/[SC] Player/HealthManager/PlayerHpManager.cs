using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHpManager : MonoBehaviour , IDamageable
{
    public int hp { get; private set; }

    public BasePlayerData stats{ get; private set; }

    //ช่วงเวลาอมตะ
    [SerializeField] float inviTime = 2f;
    bool isInvi;
    public static bool isDeath;


    //variable สำหรับระบบ player เดินทะลุหลังโดนตี
    [SerializeField] LayerMask enemyLayer;


    //For on hit effect
    [SerializeField] private Animator animator;
    private PlayerAnimationController animaCon;
    HealthUIManager hpUi;

    //BeastPenalty
    [SerializeField] private int startPenaltyValue = 2;
    int beastPenaltyVal;

    //Withred HP System
    public int witheredHp;
    [SerializeField] private float witheredRegenRate = 10f;

    void Start()
    {
        //component ref
        stats = GetComponent<BasePlayerData>();
        hpUi = FindAnyObjectByType<HealthUIManager>();
        animaCon = GetComponent<PlayerAnimationController>();

        //set variable
        hp = stats.maxHp;
        isInvi = false;
        hpUi.UpdateHP();
        isDeath = false;
        beastPenaltyVal = startPenaltyValue;

        SendDataDie.instance.StartRecording();
    }


    public void getDamage(int damageValue)
    {
        //ถ้าเข้า beast mode ไมรับดาเมจ
        if(stats.isBeastMode)
        {
            return;
        }
        //ถ้าเป็นอมตะอยู่ ไม่รันcodeที่เหลือ
        if(isInvi || isDeath)
        {
            return;
        }

        //ON hit event//

        Debug.Log("Player Take Damage: " + damageValue);
        witheredHp = Mathf.Clamp(witheredHp,0,hp); //เพื่อจำกัดจำนวน withered hp ให้เท่ากับ จำนวน hp ในปัจจุบัน
        animator.SetTrigger("Hit");

        hp--;
        hpUi.UpdateHP();
        stats.filter.Hit();
        AudioManager.PlaySound(SoundType.Hit , 0.5f);
        CameraShakeManager.instance.CameraShake(stats.impulseSource , 3f);
        
        if (hp > 0 || hp > witheredHp) animaCon.Hurt();
        StartCoroutine(FrameFreeze());
        
        IEnumerator FrameFreeze()
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(0.1f);
            Time.timeScale = 1f;
        }
        stats.rb.velocity = Vector2.zero;

        ////
        
        StartCoroutine(Invincible(inviTime));

        if (hp <= 0 || hp <= witheredHp) 
        {
            Death();
        }
    }

    public void Death()
    {
        PlayerStateManager playerState = GetComponent<PlayerStateManager>();
        PlayerSpellSlot playerSpellSlot = GetComponent<PlayerSpellSlot>();
        playerState.Dying();

        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            GameOverFilter.instance.GameOver();
            playerSpellSlot.canCastSpell = false;
            isDeath = true;
            BGManager.instance.ChangeMusic("GameOver");
            BGManager.instance.duration = 2f;
            yield return new WaitForSeconds(4);
            GameOverMenu.instance.GameOver();

            SendDataDie.instance.StopTimeRecord();

        }
    }


    public bool PayHealth(int value, bool isBeastPenalty = false)
    {
        if (!isBeastPenalty && witheredHp >= hp)
        {
            return false;
        }

        if (witheredHp <= 0)
        {
            witheredHp += value;
            StartCoroutine(StartRegen());
        }
        else
        {
            witheredHp += value;
        }
        witheredHp = Mathf.Clamp(witheredHp,0,hp);

        hpUi.UpdateHP();
        stats.filter.Hit();
        AudioManager.PlaySound(SoundType.Hearth_Breaking , 0.5f);

        IEnumerator StartRegen()
        {
            while (witheredHp > 0)
            {
                yield return new WaitForSeconds(witheredRegenRate);
                witheredHp--;
                hpUi.UpdateHP();
            }
        }


        return true;
    }

    public void GainHealth(int value)
    {
        if (hp + value > stats.maxHp)
        {
            hp = stats.maxHp;
        }
        else
        {
            hp += value;
        }

        hpUi.UpdateHP();
    }

    IEnumerator Invincible(float inviTime)
    {
        Debug.Log("StartInvi");
        isInvi = true;
        animator.SetBool("IsIframe", true);
        //สั่งให้ collision ระหว่าง player กับ enemy ไม่ทำงาน
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Player"),
        LayerMask.NameToLayer("Enemy"),
        true);

        yield return new WaitForSeconds(inviTime);

        isInvi = false;
        animator.SetBool("IsIframe", false);
        //คืนค่าให้กลับมาทำงานอีกครั้ง
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Player"),
        LayerMask.NameToLayer("Enemy"),
        false);

        Debug.Log("EndInvi");
    }
    
    public void BeastPenalty()
    {
        PayHealth(beastPenaltyVal , true);
        StartCoroutine(Invincible(inviTime));
        beastPenaltyVal ++;
    }

    public void GetDeathBy(string whom)
    {
        Debug.Log(whom);
        SendDataDie.instance.dataDie.dieBy = whom;
    }
}
