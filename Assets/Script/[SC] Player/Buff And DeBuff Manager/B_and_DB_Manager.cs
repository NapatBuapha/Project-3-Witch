using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class B_and_DB_Manager : MonoBehaviour
{
    //dbb ย่อมากจาก debuff and buff
    [SerializeField] Debuff_Buff_Base[] dbb_Library;

    Dictionary<Debuff_Buff_Base, float> current_DBB = new Dictionary<Debuff_Buff_Base, float>();

    void Awake()
    {
        dbb_Library = Resources.LoadAll<Debuff_Buff_Base>("Debuff and Buff");
    }

    // Update is called once per frame
    void Update()
    {
        List<Debuff_Buff_Base> toRemove = new List<Debuff_Buff_Base>();

        // อัปเดตเวลา
        foreach (var debuff in current_DBB.Keys.ToList())
        {

            current_DBB[debuff] -= Time.deltaTime;
            if (current_DBB[debuff] <= 0)
                toRemove.Add(debuff);
        }

        // ลบ debuff ที่หมดเวลาแล้ว
        foreach (var d in toRemove)
        {
            d.OnEffectEnd();
            current_DBB.Remove(d);
        }


    }

    public void FindDBB(string dbbId)
    {
        bool isFound = false;
        foreach (Debuff_Buff_Base dbb in dbb_Library)
        {
            if (dbb.buffId == dbbId)
            {
                isFound = true;
                GainDBB(dbb);
            }
        }

        //สำหรับเช็ค
        if (!isFound)
        {
            Debug.Log("Debuff not found");
        }
    }
    
    public void GainDBB(Debuff_Buff_Base dbb)
    {
        dbb.ApplyEffect();
        current_DBB.Add(dbb, dbb.maxDura);
    }
}
