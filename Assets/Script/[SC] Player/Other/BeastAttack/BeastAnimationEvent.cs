using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BeastAnimationEvent : MonoBehaviour
{
    //Script นี้จะใช้ร่วมกับ animation event ของ unity เพื่อความง่ายเเละความเป๊ะของการสร้างวงโจมตีในร่าง beast
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform attackPoint;

    public void AttackAreaCreate()
    {
        AudioManager.PlaySound(SoundType.Player_BeastStomp, 0.5f);
        Instantiate(prefab, attackPoint.position, quaternion.identity);
    }
    
    public void ScreamAnimationEvent()
    {
        AudioManager.PlaySound(SoundType.Player_BeastScream);
    }
}
