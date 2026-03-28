using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPage : MonoBehaviour , IInteractable
{
    [SerializeField] private SpellBase spell;
    private SpellAcceptMenuManager SAManager;

    // Start is called before the first frame update
    void Start()
    {
        SAManager = GameObject.FindWithTag("SAManager").GetComponent<SpellAcceptMenuManager>();
    }

    //TEMP
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Touch");
            SpellUsageAnalytic.instance.UnlockedSpell(spell.spellID);
            SAManager.OpenMenu(spell.name, spell.desc, spell.icon, spell.spellID);
            Destroy(gameObject); 
        }
    }

    public void interact()
    {
        //มาทำทีหลัง
    }
}
