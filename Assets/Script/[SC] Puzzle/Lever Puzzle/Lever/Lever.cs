using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Lever : MonoBehaviour , IInteractable
{
    [SerializeField]
    private enum Direction
    {
        N,
        E,
        S,
        W
    }

    Puzzle_01 puzzleManager;

    [SerializeField] private Direction currentDi;
    [SerializeField] private Direction correctDi;

    public bool isAtCorrectDi { get; private set; }

    void Start()
    {
        puzzleManager = transform.parent.GetComponent<Puzzle_01>();
        isAtCorrectDi = false;
        RandomDi();
    }

    void RandomDi()
    {
        Direction[] allDirection = (Direction[])Enum.GetValues(typeof(Direction));
        currentDi = allDirection[UnityEngine.Random.Range(0, allDirection.Length)];
        Rotate();

        if (currentDi == correctDi)
            RandomDi();
    }

    public void interact()
    {
        AudioManager.PlaySound(SoundType.Lever_Interact);
        ChangeDi();
        Rotate();
    }

    void ChangeDi()
    {
        switch (currentDi)
        {
            case Direction.N:
                currentDi = Direction.E;
                break;
            case Direction.E:
                currentDi = Direction.S;
                break;
            case Direction.S:
                currentDi = Direction.W;
                break;
            default:
                currentDi = Direction.N;
                break;
        }

        Rotate();
        CheckDi();
        puzzleManager.CheckLeverDi();
    }
    
    void Rotate()
    {
        switch (currentDi)
        {
            case Direction.N:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case Direction.W:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case Direction.S:
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            default:
                transform.eulerAngles = new Vector3(0, 0, 270);
                break;
        }
    }

    void CheckDi()
    {
        if (currentDi == correctDi)
        {
            isAtCorrectDi = true;
        }
        else
        {
            isAtCorrectDi = false;
        }
    }
    
}
