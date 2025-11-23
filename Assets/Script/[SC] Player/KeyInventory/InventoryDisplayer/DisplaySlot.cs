using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySlot : MonoBehaviour
{
    Image image;
    [SerializeField] TMP_Text quantityText;
    public string itemID {get ; private set;}
    public bool hasItem {get ; private set;}

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        quantityText.text = "";
        hasItem = false;
    }

    // Update is called once per frame
    public void UpdateIcon(Sprite icon, int quantity, string itemID)
    {
        hasItem = true;
        this.itemID = itemID;
        image.enabled = true;
        image.sprite = icon;
        quantityText.text = quantity.ToString();
    }
}
