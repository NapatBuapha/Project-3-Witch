using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverDoorUI : MonoBehaviour
{
    [SerializeField] CloverReceiver[] cloverReceivers;
    [SerializeField] GameObject[] clovers;
    [SerializeField] GameObject header;
    [SerializeField] Dialogue dialogue;

    [SerializeField] GameObject[] disableAfterComObj;
    PlayerSpellSlot playerSpellSlot;
    int cloverCount;
    int maxCloverCount = 4;

    void Awake()
    {
        playerSpellSlot = FindAnyObjectByType<PlayerSpellSlot>();
        CloseUi();
        foreach(GameObject clover in clovers)
        {
            clover.SetActive(false);
        }
    }

    public void OpenUi()
    {
        playerSpellSlot.canCastSpell = false;
        header.SetActive(true);
    }

    public void CloseUi()
    {
        playerSpellSlot.canCastSpell = true;
        header.SetActive(false);
    }

    public void ActiveClover(int amouth)
    {
        for(int i = 0; i < amouth; i++)
        {
            clovers[i].SetActive(true);
        }
    }



    public void UpdateClover()
    {
        cloverCount = 0;
        foreach(CloverReceiver receiver in cloverReceivers)
        {
            if(receiver.transform.childCount > 0)
            {
                cloverCount++;
            }
        }

        if(cloverCount == maxCloverCount)
        {
            Complete();
        }
    }

    void Complete()
    {
        CloseUi();
        AudioManager.PlaySound(SoundType.Puzzle_Complete , 0.2f);
        foreach(GameObject gameObject in disableAfterComObj)
        {
            gameObject.SetActive(false);
        }

        DialogueManager.SetDialogue(dialogue);
    }

    
}
