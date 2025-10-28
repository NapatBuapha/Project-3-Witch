using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                 Debug.Log("Quit");
                Application.Quit();
            }
        }
    }

    public void GameOver()
    {
        GameStateManager.instance.ChangeState(GameStateManager.GameState.GameOver);
        panel.SetActive(true);
        isOpen = true;
    }


}
