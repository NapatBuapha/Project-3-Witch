using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlotUiManager : MonoBehaviour
{
    //อ้างอิง ref จาก component ต่างๆ
    PlayerSpellSlot slotRef;//เก็บไว้ดึงค่า icon skill กับ max cooldown
  
    //เพิ่ม slot ใหม่ทุกครั้งที่มีเวทย์เพิ่ม
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotParent;
    Dictionary<int, SpellSlot> spellSlotDict;

    int currentIndex;

    void Awake()
    {
        //Variable Set
        spellSlotDict = new Dictionary<int, SpellSlot>();
    }
    void Start()
    {
        //Comp Ref
        slotRef = GameObject.FindWithTag("Player").GetComponent<PlayerSpellSlot>();

        //Code line
    }

    public void CreateNewSpellSlot()
    {
        GameObject slot = Instantiate(slotPrefab, slotParent.transform);
        SpellSlot spSlot = slot.GetComponent<SpellSlot>();
        int numb = spellSlotDict.Count + 1;
        spSlot.SetButton(numb);
        spellSlotDict.Add(numb, spSlot);
    }

    // Update is called once per frame
    void Update()
    {
        this.currentIndex = slotRef.currentIndex;
        foreach(KeyValuePair<int, SpellSlot> spellslot in spellSlotDict)
        {
            spellslot.Value.maxCooldown = slotRef.spellDict[spellslot.Key].Item1.maxCD;
            spellslot.Value.cooldown = slotRef.spellDict[spellslot.Key].Item2;
            spellslot.Value.icon.sprite = slotRef.spellDict[spellslot.Key].Item1.icon;
        }
    }

    public bool ChangeChosenSlot(int index)
    {
        if (spellSlotDict.ContainsKey(index))
        {
            ///
            return true;
        }

        return false;
    }
    
    public void ResetSelected()
    {
        foreach(KeyValuePair<int, SpellSlot> spellslot in spellSlotDict)
        {
            spellslot.Value.isSelected = false;
            spellslot.Value.SelectedUpdate();
        }
    }
}
