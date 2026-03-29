using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class PlayerCompletionPercent : MonoBehaviour
{
    public static PlayerCompletionPercent Instance { get; private set; }

    public bool isSend;

    void Awake()
    {
        instance = this;

    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }


    
}
