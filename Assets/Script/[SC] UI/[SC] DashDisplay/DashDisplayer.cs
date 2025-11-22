using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashDisplayer : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private TMP_Text guideText;

    void Start()
    {
        DashReady();
    }

    public void DashReady()
    {
        mask.enabled = false;
        guideText.enabled = true;
    }

    public void DashCooldown()
    {
        mask.enabled = true;
        guideText.enabled = false;
    }
}
