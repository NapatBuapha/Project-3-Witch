using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] float delayed = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(Delay());
        }
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayed);
        DialogueManager.SetDialogue(dialogue);
        Destroy(gameObject);
    }
    
}
