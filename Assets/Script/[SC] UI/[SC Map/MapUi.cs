using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUi : MonoBehaviour
{
    bool canOpen;
    bool isOpen;
    [SerializeField] GameObject mapPanel;
    [SerializeField] GameObject guideIcon;

    void Start()
    {
        mapPanel.SetActive(false);
        guideIcon.SetActive(false);
        isOpen = false;
        canOpen = false;
    }

    void Update()
    {
        if(!canOpen)
        {
            return;
        }

        if(!isOpen && Input.GetKey(KeyCode.Tab))
        {
            mapPanel.SetActive(true);
            isOpen = true;
        }
        else if(isOpen && Input.GetKeyUp(KeyCode.Tab))
        {
            mapPanel.SetActive(false);
            isOpen = false;
        }
    }
    public void MapEnable()
    {
        canOpen = true;
        guideIcon.SetActive(true);
    }
}
