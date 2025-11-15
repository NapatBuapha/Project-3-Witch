using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    PlayerHpManager playerHp;
    [SerializeField] GameObject hpGroup;
    [SerializeField] Hearth[] hearth;

    public int witheredValue;
    int hpValue;

    void Awake()
    {
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerHpManager>();
        hearth = hpGroup.transform.GetComponentsInChildren<Hearth>();
    }

    public void UpdateHP()
    {
        hpValue = playerHp.hp;
        Debug.Log(playerHp.hp);

    
        //Set Hearth
        for (int i = 0; i < hearth.Length; i++)
        {
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

        //Set Withered
        witheredValue = playerHp.witheredHp;
            for(int i = hearth.Length-1; i >= 0; i--)
            {
            if(witheredValue > 0)
            {
                if(hearth[i].Withered())
                {
                    witheredValue--;
                }
            }            
            }



    }

    public void DestroyLastHearth()
    {
        for (int i = hearth.Length - 1; i > 0; i--)
        {
            if (hearth[i] != null && !hearth[i].isDestroying)
            {
                hearth[i].SelfDestroy();
                break;
            }
        
        }
    }
}
