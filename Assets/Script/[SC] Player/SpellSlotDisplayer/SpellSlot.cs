using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    //อ้างอิงค่าต่างๆจาก inspector
    [SerializeField] private Image cooldownMask;
    [SerializeField] private TMP_Text cooldownText;

    [HideInInspector] public float maxCooldown;
    [HideInInspector] public float cooldown;

    void Start()
    {
        cooldown = 0;
        cooldownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            cooldownText.gameObject.SetActive(true);
            cooldownText.text = cooldown.ToString("F1");
            cooldownMask.fillAmount = cooldown / maxCooldown;
        }
        
        if(cooldown <= 0)
        {
            cooldownText.gameObject.SetActive(false);
            cooldownText.text = cooldown.ToString("F1");
            cooldownMask.fillAmount = cooldown / maxCooldown;
        }
    }
}
