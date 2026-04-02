using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    [SerializeField] private GameObject inMapIcon;
    [SerializeField] private KeyItem item;
    [SerializeField] private Dialogue dialogue;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(item.itemID == "02")
            {
                PlayerCompletionPercent.instance.KeyItemPuzzleData("Red_Ring", true, 4);
            }

            if(item.itemID == "01")
            {
                PlayerCompletionPercent.instance.KeyItemPuzzleData("Blue_Ring", true, 4);
            }

            if(inMapIcon!=null)inMapIcon.SetActive(false);
            KeyInventory p_inven = col.GetComponent<KeyInventory>();
            p_inven.AddItem(item);
            DialogueManager.SetDialogue(dialogue);
            
            Debug.Log("!");
            Destroy(gameObject); 
        }
    }
}
