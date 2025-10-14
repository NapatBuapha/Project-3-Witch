using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    //อ้างอิง spell
    [SerializeField] SpellBase[] spellLibrary;
    [SerializeField] SpellBase[] spellslot = new SpellBase[5];
    float[] slotCD; //Cooldown เเต่ละ slot
    int currentIndex; //เวทย์ที่เลือกอยู๋ในตอนนี้

    //อ้างอิงค่าจาก player
    private PlayerStatsData stats;
    private PlayerStateManager playerS;

    //เพื่อกันร่ายเวทย์พร้อมกัน ระหว่างกำลังร่ายเวทย์อื่น
    private bool isCasting;


    void Awake()
    {
        //get Component ref
        stats = GetComponent<PlayerStatsData>();
        playerS = GetComponent<PlayerStateManager>();
        spellLibrary = Resources.LoadAll<SpellBase>("Spells");
        GetSpellData("01");

        //variable set
        slotCD = new float[spellslot.Length];
        isCasting = false;
        resetSlotCD();
        currentIndex = 0;
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
        #region ปุ่มกด skill
        if (!isCasting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentIndex = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentIndex = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentIndex = 3;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentIndex = 4;
            }



            if (Input.GetMouseButton(0))
            {
                if(spellslot[currentIndex] != null)
                CastSpell(currentIndex);
            }
        }

        
        #endregion
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
        yield return new WaitForSeconds(spellslot[index].castingDura);
        //ใช้ spell + ลด mana
        stats.Mana -= spellslot[index].manaCost;
        spellslot[index].UseSpell();
        isCasting = false;
    }

    void GetSpellData(string spellID)
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
