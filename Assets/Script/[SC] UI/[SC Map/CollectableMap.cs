using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMap : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DialogueManager.SetDialogue(dialogue);
            FindAnyObjectByType<MapUi>().MapEnable();
            Destroy(gameObject);
        }
    }
}
