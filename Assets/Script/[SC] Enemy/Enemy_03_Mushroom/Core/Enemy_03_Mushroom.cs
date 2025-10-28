using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class Enemy_03_Mushroom : BaseEnemyData
{
    //Stats พื้นฐาน BaseMobData name_ ,base_Speed , MaxHp , Atk ปรับได้ใน inspector
    AIPath aIPath;

    //Stats for dash attack
    [Header("Exploding variable")]
    public float explodingStatesTime = 0.5f;
    public float startExplodingDistance = 2f;
    public GameObject explosivePrefab;

    [Header("Spawning state variable")]
    public float spawnStatesTime = 0.5f;
    protected override void Awake()
    {
        base.Awake();
        aIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        aIPath.maxSpeed = base_Speed;
        base.Update();
    }

    //กันมันสร้างระเบิดหลังกดออก
    private bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (isQuitting) return; // ปิดไม่ให้ทำงานตอนออกเกม

        // ถ้ากำลังเปลี่ยน scene
        if (SceneManager.GetActiveScene().isLoaded == false)
            return;

        Instantiate(explosivePrefab, transform.position, quaternion.identity);
    }
}
