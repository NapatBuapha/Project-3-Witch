using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    [SerializeField] private KeyItem[] neededItem;
    [SerializeField] private int insertedKey;

    void Start()
    {
        insertedKey = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
            KeyInventory p_inven = col.collider.GetComponent<KeyInventory>();

            for(int i = 0; i < neededItem.Length ;i++)
            {
                if(p_inven.CheckForItem(neededItem[i]))
                {
                    insertedKey++;
                }
            }

            if(insertedKey == neededItem.Length)
            {
                Destroy(gameObject);
            }
        }

        else
        {
            Debug.Log("need Key");
        }
    }
}
