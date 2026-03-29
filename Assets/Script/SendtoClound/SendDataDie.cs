using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class SendDataDie : MonoBehaviour
{
    public static SendDataDie instance {get; private set;}
    
    public CustomEvent placePlayerDie;
    public CustomEvent timePerRun;
    public DataDie dataDie = new DataDie();

    public bool startRecord;
    float startTime;


    void Awake()
    {
        instance = this;
        dataDie.place = "LV(1)";
    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    public void StartRecording() 
    {
        Debug.Log("Start Record time");
        startTime = Time.time; // บันทึกเวลาที่เริ่ม
        startRecord = true;
    }

    public void StopTimeRecord()
    {
        if (!startRecord) return;

        dataDie.playTime = Time.time - startTime; 
        startRecord = false;

        SendDataToServer();
    }

    public void SendDataToServer()
    {
        placePlayerDie = new CustomEvent("Place_Player_Die")
        {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {"DieFrom" , dataDie.dieBy},
            {"PlaceDie" , dataDie.place},
        };

        timePerRun = new CustomEvent("TimePlayPerRun")
        {
            {"GamePlayID", GameStateManager.instance.gameSessionId},
            {"TimePerRun" , dataDie.playTime},
        };

        AnalyticsService.Instance.RecordEvent(placePlayerDie);
        AnalyticsService.Instance.RecordEvent(timePerRun);
        Debug.Log("SendDataDie");
    }

    public class DataDie
    {
        public string place;
        public string dieBy;
        public float playTime;
    }
}
