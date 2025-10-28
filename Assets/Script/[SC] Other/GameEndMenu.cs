using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndMenu : MonoBehaviour
{
    public static GameEndMenu instance;
    [SerializeField] private GameObject panel;
    [SerializeField] bool isOpen;
    private void Awake()
    {
        instance = this;
        isOpen = false;
        panel.SetActive(false);
    }

    void Update()
    {
        if(isOpen)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("Quit");
                Application.Quit();
            }
        }
    }

    public void GameEnd()
    {
        GameStateManager.instance.ChangeState(GameStateManager.GameState.GameOver);
        panel.SetActive(true);
        isOpen = true;
    }
}
