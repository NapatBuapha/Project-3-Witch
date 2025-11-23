using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryDisplayer : MonoBehaviour
{
    public Dictionary<string, (KeyItem,int)> inventoryDict;
    [SerializeField] DisplaySlot[] slots;
    void Start()
    {
        
    }

    public void UpdateUi(Dictionary<string, (KeyItem,int)> inventoryDict)
    {
        this.inventoryDict = inventoryDict;
        foreach (var key in  inventoryDict.Keys.ToList())
        {
            KeyItem item = inventoryDict[key].Item1;

            //กรณีมี id ตรงจะไป update slot ช่องนั้น
            foreach(DisplaySlot slot in slots)
            {
                if(slot.itemID == item.itemID)
                {
                    slot.UpdateIcon(item.icon, inventoryDict[key].Item2, item.itemID);
                    break;
                }

                if(!slot.hasItem)
                {
                    slot.UpdateIcon(item.icon, inventoryDict[key].Item2, item.itemID);
                    break;
                }
            }

            //loop หาช่องที่ว่างอยู่
            /*foreach(DisplaySlot slot in slots)
            {
                if(!slot.hasItem)
                {
                    slot.UpdateIcon(item.icon, inventoryDict[key].Item2, item.itemID);
                    break;
                }
            }*/
        }
    }
}
