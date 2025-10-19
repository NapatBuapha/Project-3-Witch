using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlotUiManager : MonoBehaviour
{
    //อ้างอิง ref จาก component ต่างๆ
    PlayerSpellSlot slotRef;//เก็บไว้ดึงค่า icon skill กับ max cooldown


    [SerializeField] SpellSlot[] spellSlots = new SpellSlot[5];
    int currentIndex;
    float[] slotCD;
    void Start()
    {
        slotRef = GameObject.FindWithTag("Player").GetComponent<PlayerSpellSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        this.currentIndex = slotRef.currentIndex;
        this.slotCD = slotRef.slotCD;
        for (int i = 0; i < slotRef.spellslot.Length; i++)
        {
            if (slotRef.spellslot[i] != null)
            {
                spellSlots[i].maxCooldown = slotRef.spellslot[i].maxCD;
                spellSlots[i].cooldown = slotCD[i];
                spellSlots[i].icon.sprite = slotRef.spellslot[i].icon;
            }

        }
    }

    public void ChangeChosenSlot(int index)
    {
        ResetSelected();
        spellSlots[index].isSelected = true;
        spellSlots[index].SelectedUpdate();
    }
    
    void ResetSelected()
    {
        for(int i = 0; i < spellSlots.Length; i++)
        {
            spellSlots[i].isSelected = false;
            spellSlots[i].SelectedUpdate();
        }
    }
}
