using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//[CreateAssetMenu(fileName = "Debuff_Buff_Base", menuName = "Debuff_Buff_Base")]
public class Debuff_Buff_Base : ScriptableObject
{
    public string buffId { get; private set; }
    public string buffName { get; private set; }
    public float maxDura { get; private set; }

    protected GameObject player;
    protected void Init(string buffId, string buffName, float maxDura)
    {
        this.buffId = buffId;
        this.buffName = buffName;
        this.maxDura = maxDura;
    }

    public virtual void ApplyEffect()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public virtual void OnEffectEnd()
    {
        
    }
}
    


   

