using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosManager : MonoBehaviour
{
    [SerializeField] GameObject[] playerpos;
    public static PlayerPosManager instance;

    private void Awake() 
    {
        instance = this;    
        ResetPos();
    }


    public void ResetPos()
    {
        foreach(GameObject pos in playerpos)
        {
            pos.SetActive(false);
        }
    }
}
