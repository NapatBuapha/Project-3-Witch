using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    PlayerHpManager playerHp;
    [SerializeField] GameObject hpGroup;
    [SerializeField] Hearth[] hearth;
    int hpValue;

    void Awake()
    {
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerHpManager>();
        hearth = hpGroup.transform.GetComponentsInChildren<Hearth>();
    }

    public void UpdateHP()
    {
        hpValue = playerHp.hp;

        for (int i = 0; i < hearth.Length; i++)
        {
            if (hearth[i] == null)
            {
                break;
            }

            if (hpValue > 0)
            {
                hearth[i].UpdateValue(1);
                hpValue--;
            }
            else
            {
                hearth[i].UpdateValue(0);
            }
        }
    }

    public void DestroyLastHearth()
    {
        for (int i = hearth.Length-1; i > 0; i--)
        {
            if (hearth[i] != null)
            {
                hearth[i].SelfDestroy();
                break;
            }
            
        
        }
    }
}
