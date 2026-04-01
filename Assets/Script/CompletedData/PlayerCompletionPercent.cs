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

    void Awake()
    {
        instance = this;
    }

    public void KeyItemPuzzleData(string nameData,bool completeData,int percent)
    {
        percentCompletion += percent;

         completionRate = new CustomEvent("Completion_Rate")
         {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {nameData , completeData},
            {"PercentCompletion", percentCompletion},
         };

         AnalyticsService.Instance.RecordEvent(completionRate);

    }

    public void RoomPercent(bool completeRoom,int percentRoom)
    {
        percentCompletion += percentRoom;

        completionRate = new CustomEvent("Completion_Rate")
        {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {"PlaceDie" , SendDataDie.instance.dataDie.place},
            {"Completion_Room" , completeRoom},
            {"PercentCompletion", percentCompletion},
        };

        AnalyticsService.Instance.RecordEvent(completionRate);
    }
}
