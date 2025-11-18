using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    public Dictionary<string, KeyItem> inventoryDict;
    void Start()
    {
        inventoryDict = new Dictionary<string, KeyItem>();
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
        inventoryDict.Add(item.itemID , item);
    }

}
