using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrededPaperPage : MonoBehaviour
{
   [Header("Movement")]
    public float speedMin = 0.5f;
    public float speedMax = 1.5f;
    public float lifetime = 5f;

    [Header("Wobble")]
    public float wobbleAmplitude = 0.2f;  // ความกว้างการแกว่ง
    public float wobbleFrequency = 3f;    // ความถี่

    private Vector2 direction;
    private float speed;
    private float timer = 0f;

    private SpriteRenderer sr;
    private Color originalColor;
    private Vector3 startPos;

    void Start()
    {
        // ทิศทางสุ่ม
        direction = Random.insideUnitCircle.normalized;

        speed = Random.Range(speedMin, speedMax);

        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        startPos = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 1) เคลื่อนไปตามทิศทางหลัก
        Vector2 mainMove = direction * speed * timer;

        // 2) การโยกไปมาแบบ sine
        float wobble = Mathf.Sin(timer * wobbleFrequency) * wobbleAmplitude;

        // สร้าง vector แกว่ง 90 องศาจากทิศหลัก
        Vector2 perp = new Vector2(-direction.y, direction.x);

        // รวมเป็นตำแหน่งใหม่
        transform.position = startPos + (Vector3)mainMove + (Vector3)(perp * wobble);

        // 3) จางหายตามเวลา
        float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        if (timer >= lifetime)
            Destroy(gameObject);
    }

}
