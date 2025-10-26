using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastModeManager : MonoBehaviour
{
    //BeastModeValue
    [SerializeField] private int beastCount;
    [SerializeField] private int maxBeastCount = 6;
    public bool isBeastMode_Able { get; private set; }

    //Component Ref
    PlayerHpManager playerHp;
    BasePlayerData stats;
    SpellBook spellBook;

    //Visual
    [Header("Visual Ui")]
    bool isUIOpen;
    [SerializeField] private GameObject groupParent;
    [SerializeField] private GameObject mask;
    [SerializeField] private TMP_Text beastCountText;

    [SerializeField] private Dialogue beastCutScene;

    void Start()
    {
        //Get ref
        spellBook = FindAnyObjectByType<SpellBook>();
        stats = FindAnyObjectByType<BasePlayerData>();
        playerHp = stats.gameObject.GetComponent<PlayerHpManager>();


        groupParent.SetActive(false);

        beastCount = maxBeastCount;
        isBeastMode_Able = false;
        isUIOpen = false;
    }

    public void ReducedBeastCount()
    {
        beastCount--;

        if (isUIOpen)
        {
            UpdateUI();
        }

        if (beastCount <= 0)
        {
            isBeastMode_Able = true;
            groupParent.SetActive(true);

            if(!isUIOpen)
            {
                DialogueManager.SetDialogue(beastCutScene);
                isUIOpen = true;
            }
            UpdateUI();
        }
        

    }

    void UpdateUI()
    {
        if (beastCount <= 0)
        {
            mask.SetActive(false);
            beastCountText.text = "";
        }
        else
        {
            mask.SetActive(true);
            beastCountText.text = beastCount.ToString();
        }
    }

    public void ResetBeastCount()
    {
        isBeastMode_Able = false;
        beastCount = maxBeastCount;
        playerHp.BeastPenalty();
        UpdateUI();
    }

}
