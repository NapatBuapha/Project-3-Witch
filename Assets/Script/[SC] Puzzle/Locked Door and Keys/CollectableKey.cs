using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    [SerializeField] private KeyItem item;
    [SerializeField] private Dialogue dialogue;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            KeyInventory p_inven = col.GetComponent<KeyInventory>();
            p_inven.AddItem(item);
            DialogueManager.SetDialogue(dialogue);
            Destroy(gameObject); 
        }
    }
}
