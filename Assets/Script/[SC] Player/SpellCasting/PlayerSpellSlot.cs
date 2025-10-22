using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerSpellSlot : MonoBehaviour
{
    //อ้างอิง spell
    [SerializeField] SpellBase[] spellLibrary;

    //อารมณ์ดีเเล้วค่อยมาเเก้เป็น dictionary
    public SpellBase[] spellslot = new SpellBase[5];
    [HideInInspector]public float[] slotCD; //Cooldown เเต่ละ slot
    public int currentIndex{ get; private set; } //เวทย์ที่เลือกอยู๋ในตอนนี้



    //อ้างอิงค่าจาก player
    private BasePlayerData stats;
    private PlayerStateManager playerS;

    //เพื่อกันร่ายเวทย์พร้อมกัน ระหว่างกำลังร่ายเวทย์อื่น
    private bool isCasting;
    public bool canChangeSpell;

    //UI Ref
    SpellSlotUiManager ui;


    void Awake()
    {
        //get Component ref
        stats = GetComponent<BasePlayerData>();
        playerS = GetComponent<PlayerStateManager>();
        spellLibrary = Resources.LoadAll<SpellBase>("Spells");
        ui = GameObject.Find("[UI] SkillSlot").GetComponent<SpellSlotUiManager>();
        GetSpellData("01");

        //variable set
        slotCD = new float[spellslot.Length];

        isCasting = false;
        resetSlotCD();
        ChangeSpell(0);
        canChangeSpell = true;
    }
    
    void resetSlotCD()
    {
        for (int i = 0; i < slotCD.Length; i++)
        {
            slotCD[i] = 0;
        }
    }

    void Update()
    {
        if (!isCasting)
        {
            #region ปุ่มกด skill
            if (canChangeSpell)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeSpell(0);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeSpell(1);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeSpell(2);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ChangeSpell(3);
                }

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    ChangeSpell(4);
                }
            }
            #endregion

            if (Input.GetMouseButton(0))
            {
                if (spellslot[currentIndex] != null)
                    CastSpell(currentIndex);
            }
        }
    }

    void ChangeSpell(int index)
    {
        currentIndex = index;
        ui.ChangeChosenSlot(currentIndex);
    }

    
    


    void FixedUpdate()
    {
        //นับเวลา cooldown สำหรับ spell ทั้งหมด
        for(int i = 0; i < slotCD.Length; i++)
        {
            if(slotCD[i] > 0)
            {
                slotCD[i] -= Time.deltaTime;
            }
        }
    }

    void CastSpell(int index)
    {
        //เช็ค cooldown spell
        if (slotCD[index] > 0)
        {
            Debug.Log($"{index} spell in on cooldown");
            return;
        }

        if (stats.Mana > spellslot[index].manaCost)
        {
            //สั่งใช้ spell
            StartCoroutine(Casting(index));
            //สั่งให้ spell ติด cooldown
            slotCD[index] = spellslot[index].maxCD;
        }
        else
        {
            Debug.Log("YOU HAVE NO MANA!!!");
        }
    }

    IEnumerator Casting(int index)
    {
        //หยุดร่ายเวทย์
        isCasting = true;
        playerS.Casting(spellslot[index].castingDura);
        spellslot[index].BeforeCasting();

        yield return new WaitForSeconds(spellslot[index].castingDura);
        //ใช้ spell + ลด mana
        stats.Mana -= spellslot[index].manaCost;
        spellslot[index].UseSpell();
        isCasting = false;
    }

    public void GetSpellData(string spellID)
    {
        bool isFound = false;
        foreach (SpellBase spell in spellLibrary)
        {
            if (spell.spellID == spellID)
            {
                isFound = true;
                AddSpell(spell);
            }
        }
        
        //สำหรับเช็ค
        if(!isFound)
        {
            Debug.Log("Spell not found");
        }
    }
    
    void AddSpell(SpellBase spell)
    {
        for(int i = 0; i < spellslot.Length; i++)
        {
            if(spellslot[i] == null)
            {
                spellslot[i] = spell;
                break;
            }
        }
    }

}
