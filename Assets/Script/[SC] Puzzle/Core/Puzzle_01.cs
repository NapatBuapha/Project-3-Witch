using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_01 : MonoBehaviour
{
    Lever[] levers;
    [SerializeField] private GameObject lockWall;

    void Start()
    {
        lockWall.SetActive(true);
        levers = GetComponentsInChildren<Lever>();
    }

    public void CheckLeverDi()
    {
        int correct = 0;
        foreach (Lever lever in levers)
        {
            if (lever.isAtCorrectDi)
            {
                correct++;
            }
        }

        if(correct == levers.Length)
        {
            lockWall.SetActive(false);
            AudioManager.PlaySound(SoundType.Puzzle_Complete);
        }
    }
}
