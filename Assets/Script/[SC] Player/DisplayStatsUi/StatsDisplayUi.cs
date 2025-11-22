using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayUi : MonoBehaviour
{
    private BasePlayerData stats;

    [SerializeField] private Slider manaSlider;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<BasePlayerData>();

        manaSlider.maxValue = stats.maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        manaSlider.value = stats.Mana;
    }
}
