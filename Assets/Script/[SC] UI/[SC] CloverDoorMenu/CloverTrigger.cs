using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverTrigger : MonoBehaviour ,IInteractable

{
    KeyInventory inventory;
    CloverDoorUI cloverDoorUI;
    bool isOpen;
    void Start()
    {
        cloverDoorUI = FindAnyObjectByType<CloverDoorUI>();
        inventory = FindAnyObjectByType<KeyInventory>();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            cloverDoorUI.CloseUi();
        }
        
    }

    public void interact()
    {
        cloverDoorUI.OpenUi();
        if(inventory.inventoryDict.ContainsKey("03"))
        {
    
            cloverDoorUI.ActiveClover(inventory.inventoryDict["03"].Item2);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            cloverDoorUI.CloseUi();
        }
    }
}
