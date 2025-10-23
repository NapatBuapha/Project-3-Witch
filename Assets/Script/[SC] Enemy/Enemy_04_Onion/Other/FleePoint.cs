using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleePoint : MonoBehaviour
{
    public Transform target; // ตัวที่อยากให้หมุนไปหา

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;

        // หาทิศทางจากตัวเองไปหา target
        Vector2 direction = target.position - transform.position;

        // หาองศา (เป็นเรเดียน → แปลงเป็นองศา)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // หมุนตามแกน Z (เพราะเป็น 2D topdown)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
