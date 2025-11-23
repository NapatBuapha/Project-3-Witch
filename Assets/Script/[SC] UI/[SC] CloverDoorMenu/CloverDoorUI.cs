using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverDoorUI : MonoBehaviour
{
    [SerializeField] CloverReceiver[] cloverReceivers;
    [SerializeField] GameObject[] clovers;
    [SerializeField] GameObject header;

    [SerializeField] GameObject[] disableAfterComObj;
    int cloverCount;
    int maxCloverCount = 4;

    void Awake()
    {
        CloseUi();
        foreach(GameObject clover in clovers)
        {
            clover.SetActive(false);
        }
    }

    public void OpenUi()
    {
        header.SetActive(true);
    }

    public void CloseUi()
    {
        header.SetActive(false);
    }

    public void ActiveClover(int amouth)
    {
        for(int i = 0; i < amouth; i++)
        {
            clovers[i].SetActive(true);
        }
    }



    public void UpdateClover()
    {
        cloverCount = 0;
        foreach(CloverReceiver receiver in cloverReceivers)
        {
            if(receiver.transform.childCount > 0)
            {
                cloverCount++;
            }
        }

        if(cloverCount == maxCloverCount)
        {
            Complete();
        }
    }

    void Complete()
    {
        CloseUi();
        foreach(GameObject gameObject in disableAfterComObj)
        {
            gameObject.SetActive(false);
        }
    }

    
}
