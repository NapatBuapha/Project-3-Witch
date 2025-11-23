using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverTrigger : MonoBehaviour ,IInteractable

{
    KeyInventory inventory;
    CloverDoorUI cloverDoorUI;
    PlayerSpellSlot playerSpellSlot;
    void Start()
    {
        playerSpellSlot = FindAnyObjectByType<PlayerSpellSlot>();
        cloverDoorUI = FindAnyObjectByType<CloverDoorUI>();
        inventory = FindAnyObjectByType<KeyInventory>();
    }

    public void interact()
    {
        cloverDoorUI.OpenUi();
        playerSpellSlot.canCastSpell = false;
        if(inventory.inventoryDict.ContainsKey("03"))
        {
            cloverDoorUI.ActiveClover(inventory.inventoryDict["03"].Item2);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            playerSpellSlot.canCastSpell = true;
            cloverDoorUI.CloseUi();
        }
    }
}
