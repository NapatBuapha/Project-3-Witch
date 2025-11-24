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
    [SerializeField] private int StartBeastCount = 25;
    public bool isBeastMode_Able { get; private set; }

    //Component Ref
    PlayerHpManager playerHp;
    BasePlayerData stats;
    SpellBook spellBook;
    PlayerStateManager player;
    [SerializeField] private CanvasGroup manaDisplayer;

    //Visual
    [Header("Visual Ui")]
    bool isUIOpen;
    [SerializeField] private GameObject groupParent;
    [SerializeField] private GameObject mask;
    [SerializeField] private TMP_Text beastCountText;

    [SerializeField] private Dialogue beastCutScene;
    [SerializeField] private Dialogue deTransformCutscene;

    [SerializeField] private CanvasGroup attackDisplayer;
    [SerializeField] private CanvasGroup dashDisplayer;
    bool isSeenTheCutscene;

    void Start()
    {
        //Get ref
        spellBook = FindAnyObjectByType<SpellBook>();
        stats = FindAnyObjectByType<BasePlayerData>();
        playerHp = stats.gameObject.GetComponent<PlayerHpManager>();
        player = FindAnyObjectByType<PlayerStateManager>();


        groupParent.SetActive(false);

        beastCount = StartBeastCount;
        isBeastMode_Able = false;
        isUIOpen = false;
        isSeenTheCutscene = false;
        attackDisplayer.alpha = 0;
        dashDisplayer.alpha = 1;
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
            groupParent.SetActive(true);

            if(!isUIOpen)
            {
                DialogueManager.SetDialogue(beastCutScene);
                isUIOpen = true;
            }

            player.SwitchState(player.state_PlayerBeastTransform);
            StartCoroutine(Wait());
            UpdateUI();            
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
            dashDisplayer.alpha = 0;
            manaDisplayer.alpha = 0;
            attackDisplayer.alpha = 1;
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
        if(!isSeenTheCutscene)
        {
            DialogueManager.SetDialogue(deTransformCutscene);
            isSeenTheCutscene = true;
        }

        isBeastMode_Able = false;
        beastCount = maxBeastCount;
        playerHp.BeastPenalty();

        attackDisplayer.alpha = 0;
        dashDisplayer.alpha = 1;
        manaDisplayer.alpha = 1;
        UpdateUI();
    }

}
