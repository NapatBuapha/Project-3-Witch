using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Item", menuName = "Key_Item")]
public class KeyItem : ScriptableObject
{
    public string itemID;
    public string item_name;
    public Sprite icon;
}
