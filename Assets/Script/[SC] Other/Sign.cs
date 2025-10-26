using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject signUi;
    [SerializeField] private TMP_Text text;
    [TextArea][SerializeField] private string signText;

    void Start()
    {
        signUi.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            signUi.SetActive(true);
            text.text = signText;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
           signUi.SetActive(false);
        }
    }
    void CloseUi()
    {
        signUi.SetActive(false);
    }
}
