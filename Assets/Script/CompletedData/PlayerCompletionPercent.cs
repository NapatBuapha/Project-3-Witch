using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class PlayerCompletionPercent : MonoBehaviour
{
    public static PlayerCompletionPercent instance { get; private set; }

    public CustomEvent completionRate;

    public int percentCompletion = 0;
    
    public bool isSend;
    public KeyItemPuzzleData keyItemPuzzleData = new KeyItemPuzzleData();

    void Awake()
    {
        instance = this;
    }

    public void KeyItemPuzzleData(string nameData,bool completeData,int percent)
    {
        percentCompletion += percent;

        if(nameData == "Blue_Ring")
        {
            keyItemPuzzleData.Blue_Ring = true;
        }
        else if(nameData == "Red_Ring")
        {
            keyItemPuzzleData.Red_Ring = true;
        }
        else if(nameData == "Clover_Puzzle")
        {
            keyItemPuzzleData.Clover_Puzzle = true;
        }

        /*
        completionRate = new CustomEvent("Completion_Rate")
        {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {nameData , completeData},
            {"PercentCompletion", percentCompletion},
        };
        

        AnalyticsService.Instance.RecordEvent(completionRate);
        */

    }

    public void RoomPercent(bool completeRoom,int percentRoom)
    {
        percentCompletion += percentRoom;

        /*
        completionRate = new CustomEvent("Completion_Rate")
        {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {"PlaceDie" , SendDataDie.instance.dataDie.place},
            {"Completion_Room" , completeRoom},
            {"PercentCompletion", percentCompletion},
        };
        

        AnalyticsService.Instance.RecordEvent(completionRate);
        */
    }

    public void CompletionSendData()
    {
        Debug.Log(percentCompletion);

        completionRate = new CustomEvent("Completion_Rate")
        {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {"PercentCompletion", percentCompletion},
            {"Blue_Ring",keyItemPuzzleData.Blue_Ring},
            {"Red_Ring",keyItemPuzzleData.Red_Ring},
            {"Clover_Puzzle",keyItemPuzzleData.Clover_Puzzle},
        };

        AnalyticsService.Instance.RecordEvent(completionRate);
    }
}

public class KeyItemPuzzleData
{
    public bool Blue_Ring = false;
    public bool Red_Ring = false;
    public bool Clover_Puzzle = false;
}