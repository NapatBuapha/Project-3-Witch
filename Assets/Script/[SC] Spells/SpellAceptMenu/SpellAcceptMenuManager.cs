using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellAcceptMenuManager : MonoBehaviour
{
    private string spellID;
    //ค่าสำหรับเเสดงผลใน menu
    string spellName;
    [SerializeField] private  TMP_Text spellNameText;

    string spellDesc;
    [SerializeField] private  TMP_Text spellDescText;

    Sprite spellIcon;
    
    [SerializeField] private Image spellIconImage;
    

    //Manager variable
    [SerializeField] private GameObject panelHeader;
    PlayerSpellSlot playerSpellSlot;
    bool isMenuOpen;

    void Start()
    {
        //Component Ref
        playerSpellSlot = GameObject.FindWithTag("Player").GetComponent<PlayerSpellSlot>();

        //ปิด menu เมื่อเริ่ม
        CloseMenu();
    }

    void Update()
    {
        if (!isMenuOpen)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            LearnSkill();
        }
        
        if(Input.GetKeyDown(KeyCode.X))
        {
            DiscardSkill();        
        }
    }

    void LearnSkill()
    {
        playerSpellSlot.GetSpellData(spellID);
        CloseMenu();
    }
    
    void DiscardSkill()
    {
        CloseMenu();
    }

    public void OpenMenu(string spellName, string spellDesc, Sprite spellIcon, string spellID)
    {
        isMenuOpen = true;
        panelHeader.SetActive(true);

        this.spellName = spellName;
        spellNameText.text = spellName;

        this.spellDesc = spellDesc;
        spellDescText.text = spellDesc;

        this.spellIcon = spellIcon;
        spellIconImage.sprite = spellIcon;

        this.spellID = spellID;
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        panelHeader.SetActive(false);
    }
}
