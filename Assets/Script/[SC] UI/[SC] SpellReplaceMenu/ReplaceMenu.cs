using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceMenu : MonoBehaviour
{
    public static ReplaceMenu instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image[] mockUpSkillSlotIcons;
    PlayerSpellSlot spellRef;
    SpellBase spell_r;
    private bool isOpen;

    void Awake()
    {
        spellRef = FindAnyObjectByType<PlayerSpellSlot>();
        instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if(!isOpen) return;
        
        for (int i = 1; i <= 5; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    spellRef.spellDict[i] = (spell_r , 0);
                    ClosePanel();
                }
            }
        
        if(Input.GetKeyDown(KeyCode.X))
        {
            ClosePanel();
        }
    }

    public void OpenPanel(SpellBase spell)
    {
        spellRef.canCastSpell = false;
        isOpen = true;
        spell_r = spell;

        for(int i = 0 ;i < mockUpSkillSlotIcons.Length; i++)
        {
            mockUpSkillSlotIcons[i].sprite = spellRef.spellDict[i+1].Item1.icon;
        }
        panel.SetActive(true);
    }

    void ClosePanel()
    {
        isOpen = false;
        panel.SetActive(false);
        StartCoroutine(wait());

        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.5f);
            spellRef.canCastSpell = true;
        }
    }



}
