using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerSpellSlot : MonoBehaviour
{
    //อ้างอิง spell
    [SerializeField] SpellBase[] spellLibrary;
    public Dictionary<int, (SpellBase , float)> spellDict; //ลำดับช่อง (เวทย์ , cooldown)
    public int currentIndex; //เวทย์ที่เลือกอยู๋ในตอนนี้



    //อ้างอิงค่าจาก player
    private BasePlayerData stats;
    private PlayerStateManager playerS;

    //เพื่อกันร่ายเวทย์พร้อมกัน ระหว่างกำลังร่ายเวทย์อื่น

    private bool isCasting;
    public bool canChangeSpell;

    //UI Ref
    SpellSlotUiManager ui;
    BeastModeManager beastModeManager;
    PlayerHpManager playerHp;


    void Awake()
    {
        //variable set
        spellDict = new Dictionary<int, (SpellBase , float)>();
        isCasting = false;
        canChangeSpell = true;

        //get Component ref
        stats = GetComponent<BasePlayerData>();
        playerS = GetComponent<PlayerStateManager>();
        spellLibrary = Resources.LoadAll<SpellBase>("Spells");
        ui = GameObject.Find("[UI] SkillSlot").GetComponent<SpellSlotUiManager>();
        beastModeManager = FindAnyObjectByType<BeastModeManager>();
        playerHp = GetComponent<PlayerHpManager>();
    }

    void Start()
    {
        GetSpellData("01");
        resetSlotCD();
        ChangeSpell(1);
    }

    void resetSlotCD()
    {
        foreach (var key in  spellDict.Keys.ToList())
        {
            if(spellDict[key].Item2 > 0)
            {
                spellDict[key] = (spellDict[key].Item1, 0);
            }
        }
    }

    void Update()
    {
        if(stats.isBeastMode)
        {
            return;
        }


        if (!isCasting)
        {
            #region ปุ่มกด skill
            if (canChangeSpell)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeSpell(1);
                    if (spellDict[currentIndex].Item1 != null)
                    CastSpell(currentIndex);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeSpell(2);
                    if (spellDict[currentIndex].Item1 != null)
                    CastSpell(currentIndex);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeSpell(3);
                    if (spellDict[currentIndex].Item1 != null)
                    CastSpell(currentIndex);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ChangeSpell(4);
                    if (spellDict[currentIndex].Item1 != null)
                    CastSpell(currentIndex);
                }

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    ChangeSpell(5);
                    if (spellDict[currentIndex].Item1 != null)
                    CastSpell(currentIndex);
                }
            }
            #endregion

            if (Input.GetMouseButton(0))
            {
                if (spellDict[currentIndex].Item1 != null)
                    CastSpell(currentIndex);
            }
        }
    }

    void ChangeSpell(int index)
    {
        if(ui.ChangeChosenSlot(index))
        {
            currentIndex = index;
        }
    }

    
    


    void FixedUpdate()
    {
        //นับเวลา cooldown สำหรับ spell ทั้งหมด
        foreach (var key in  spellDict.Keys.ToList())
        {
            if(spellDict[key].Item2 > 0)
            {
                spellDict[key] = (spellDict[key].Item1, spellDict[key].Item2 - Time.deltaTime);
            }
        }
    }

    void CastSpell(int index)
    {
        //เช็ค cooldown spell
        if (spellDict[index].Item2 > 0)
        {
            Debug.Log($"{index} spell in on cooldown");
            return;
        }

        if (stats.Mana > spellDict[index].Item1.manaCost)
        {
            //สั่งใช้ spell
            StartCoroutine(Casting(index));
            //สั่งให้ spell ติด cooldown
            spellDict[index] = (spellDict[index].Item1 , spellDict[index].Item1.maxCD);
        }
        else
        {
            if (playerHp.PayHealth(1))
            {
                //สั่งใช้ spell
                StartCoroutine(Casting(index));

                //สั่งให้ spell ติด cooldown
                spellDict[index] = (spellDict[index].Item1, spellDict[index].Item1.maxCD);
            }
            else
            {
                Debug.Log("YOU HAVE NOTHING TO PAY ANYMORE KID");
            }
        }
    }

    IEnumerator Casting(int index)
    {
        //หยุดร่ายเวทย์
        isCasting = true;
        playerS.Casting(spellDict[index].Item1.castingDura);
        spellDict[index].Item1.BeforeCasting();

        yield return new WaitForSeconds(spellDict[index].Item1.castingDura);
        //ใช้ spell + ลด mana
        stats.Mana -= spellDict[index].Item1.manaCost;
        spellDict[index].Item1.UseSpell();
        isCasting = false;

        if(spellDict[index].Item1.spellID != "04")
        beastModeManager.ReducedBeastCount();
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
        spellDict.Add(spellDict.Count + 1, (spell,0));
        ui.CreateNewSpellSlot();
        ChangeSpell(spellDict.Count);
        Debug.Log("Added");
    }

}
