using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    public Dictionary<string, (KeyItem,int)> inventoryDict;
    InventoryDisplayer ui;
    void Start()
    {
        ui = FindAnyObjectByType<InventoryDisplayer>();
        inventoryDict = new Dictionary<string, (KeyItem,int)>();
    }

    public bool CheckForItem(KeyItem item)
    {
        if(inventoryDict.ContainsKey(item.itemID))
        {
            return true;
        }

        return false;
    }

    public void AddItem(KeyItem item)
    {
        if(inventoryDict.ContainsKey(item.itemID))
        {
            int currentQuantity = inventoryDict[item.itemID].Item2;
            inventoryDict[item.itemID] = (item , currentQuantity + 1);
        }
        else inventoryDict.Add(item.itemID , (item , 1));

        ui.UpdateUi(inventoryDict);
    }
}
