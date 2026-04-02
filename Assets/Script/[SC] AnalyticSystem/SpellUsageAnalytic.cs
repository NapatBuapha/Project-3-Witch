using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class SpellUsageAnalytic : MonoBehaviour
{
    public static SpellUsageAnalytic instance { get; private set; }
    Dictionary<string, int> spellCountDict;
    Dictionary<string, bool> unlockedSpellDict;
    [SerializeField] string gameSessionId;
    bool isSend;



    private void Awake()
    {
        instance = this;
        Initialize();
        GetAllSpellId();
    }

    void Start()
    {
        isSend = false;
        gameSessionId = GameStateManager.instance.gameSessionId;
    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    public void getSpellUseCount(string spell_ID)
    {
        if (spell_ID.Contains(spell_ID))
        {
            spellCountDict[spell_ID]++;
        }
    }

    public void UnlockedSpell(string spell_ID)
    {
        if (unlockedSpellDict.ContainsKey(spell_ID))
        {
            unlockedSpellDict[spell_ID] = true;
        }
    }

    private void GetAllSpellId()
    {
        spellCountDict = new Dictionary<string, int>();
        unlockedSpellDict = new Dictionary<string, bool>();

        var spellLibrary = Resources.LoadAll<SpellBase>("Spells");
        foreach (var spell in spellLibrary)
        {
            spellCountDict.Add(spell.spellID, 0);
            unlockedSpellDict.Add(spell.spellID, false);

        }
    }

    void OnApplicationQuit()
    {
        //ส่งข้อมูลฉึกเฉินกรณี player หลุด
        if(!isSend) SendFinalAnalyticData();
    }

    //Send Data
    public void SendFinalAnalyticData()
    {
        int spellSum = 0;

        foreach (var spellData in spellCountDict)
        {
            SpellUsageDataSend(spellData.Key, spellData.Value);
            spellSum += spellData.Value;
        }
        foreach (var unlockSpell in unlockedSpellDict)
        {
            SpellUnlockDataSend(unlockSpell.Key, unlockSpell.Value);
        }

        //Sent Summary Data 

        CustomEvent spellSumEvent = new CustomEvent("Spell_Spell_UsageSummary")
        {
            {"GamePlayID", gameSessionId},
            {"Spell_Usage_Sum" , spellSum},
        };

        AnalyticsService.Instance.RecordEvent(spellSumEvent);
        Debug.Log("SendData");
        isSend = true;
    }

    public void SpellUsageDataSend(string spell_ID, int spell_UseCount)
    {
        CustomEvent spellUsageEvent = new CustomEvent("Spell_Usage_Stats")
        {
            {"GamePlayID", gameSessionId},
            {"Spell_ID" , spell_ID},
            {"Spell_UseCount" , spell_UseCount},
        };

        AnalyticsService.Instance.RecordEvent(spellUsageEvent);
    }

    public void SpellUnlockDataSend(string spell_ID, bool isUnlock)
    {
        CustomEvent spellSumEvent = new CustomEvent("spell_unlocked")
        {
            {"GamePlayID", gameSessionId},
            {"Spell_ID" , spell_ID},
            {"Spel_IsUnlock" , isUnlock}
        };

        AnalyticsService.Instance.RecordEvent(spellSumEvent);
    }
}
