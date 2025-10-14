using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayUi : MonoBehaviour
{
    private PlayerStatsData stats;

    //อ้างอิง textmeshpro
    [SerializeField] private TMP_Text staminaDisplayer;
    [SerializeField] private TMP_Text manaDisplayer;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<PlayerStatsData>();
    }

    // Update is called once per frame
    void Update()
    {
        staminaDisplayer.text = "Stamina: " + stats.Stamina.ToString("F1") + "/" + stats.maxStamina; 
        manaDisplayer.text = "Mana: " + stats.Mana.ToString("F1") + "/" + stats.maxMana; 
    }
}
