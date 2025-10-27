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
    [SerializeField] private Slider staSlider;
    [SerializeField] private Slider manaSlider;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<BasePlayerData>();
        hpManager = stats.gameObject.GetComponent<PlayerHpManager>();

        staSlider.maxValue = stats.maxStamina;
        manaSlider.maxValue = stats.maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        staSlider.value = stats.Stamina;
        manaSlider.value = stats.Mana;
    }
}
