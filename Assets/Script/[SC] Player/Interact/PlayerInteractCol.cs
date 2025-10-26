using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractCol : MonoBehaviour
{
    [SerializeField] IInteractable interactable;
    [SerializeField] GameObject guidebutton;

    void Start()
    {
        guidebutton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(interactable != null)
            {
                interactable.interact();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Interactable"))
        {
            guidebutton.SetActive(true);
            interactable = col.GetComponent<IInteractable>();
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if(col.CompareTag("Interactable"))
        {
            guidebutton.SetActive(false);
            interactable = null;
        }
    }
}
