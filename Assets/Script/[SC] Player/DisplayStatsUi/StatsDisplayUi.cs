using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayUi : MonoBehaviour
{
    private BasePlayerData stats;
    private PlayerHpManager hpManager;

    //อ้างอิง textmeshpro
    [SerializeField] private TMP_Text healthDisplayer;
    [SerializeField] private TMP_Text staminaDisplayer;
    [SerializeField] private TMP_Text manaDisplayer;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<BasePlayerData>();
        hpManager = stats.gameObject.GetComponent<PlayerHpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplayer.text = "HP: " + hpManager.hp + "/" + stats.maxHp;
        staminaDisplayer.text = "Stamina: " + stats.Stamina.ToString("F1") + "/" + stats.maxStamina; 
        manaDisplayer.text = "Mana: " + stats.Mana.ToString("F1") + "/" + stats.maxMana; 
    }
}
