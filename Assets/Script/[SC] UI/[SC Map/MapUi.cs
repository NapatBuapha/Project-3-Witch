using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUi : MonoBehaviour
{
    bool canOpen;
    bool isOpen;
    [SerializeField] GameObject mapPanel;
    [SerializeField] GameObject guideIcon;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = mapPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
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
            canvasGroup.alpha = 1;
            isOpen = true;
        }
        else if(isOpen && Input.GetKeyUp(KeyCode.Tab))
        {
            canvasGroup.alpha = 0;
            isOpen = false;
        }
    }
    public void MapEnable()
    {
        canOpen = true;
        guideIcon.SetActive(true);
    }
}
