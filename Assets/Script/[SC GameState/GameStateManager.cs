using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;

    public enum GameState
    {
        Default,
        DialogueSequence,
    }

    GameState currentGameState;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public static void ChangeState(GameState state)
    {
        switch(state)
        {
            case GameState.Default:
            Time.timeScale = 1f;
            break;

            case GameState.DialogueSequence:
            Time.timeScale = 0f;
            break;

        }
    }
}
