using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    [SerializeField] AudioSource audioSource;

    public enum GameState
    {
        Default,
        DialogueSequence,
        GameOver,
    }

    GameState currentGameState;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void ChangeState(GameState state)
    {
        switch(state)
        {
            case GameState.Default:
            AudioListener.pause = false;
            Time.timeScale = 1f;
            break;

            case GameState.DialogueSequence:
            AudioListener.pause = true;
            Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                break;

        }
    }
}
