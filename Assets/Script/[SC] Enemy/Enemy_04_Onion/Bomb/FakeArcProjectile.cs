using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeArcProjectile : MonoBehaviour
{
    public Vector2 target;
    public float speed = 5f;
    public float heightFactor = 0.5f; // ยิ่งสูงยิ่งเด้งมาก

    private Vector2 startPos;
    private Vector2 endPos;
    private float journeyLength;
    private float startTime;

    [SerializeField] private GameObject bombPrefab;

    void Start()
    {
        startPos = transform.position;
        endPos = target;
        journeyLength = Vector2.Distance(startPos, endPos);
        startTime = Time.time;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float t = distCovered / journeyLength;

        if (t >= 1f)
        {
            transform.position = endPos;
            Instantiate(bombPrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        // ตำแหน่งจริงในระนาบ 2D
        Vector2 basePos = Vector2.Lerp(startPos, endPos, t);

        // คำนวณความสูงหลอก (โค้งขึ้นลง)
        float maxHeight = heightFactor * journeyLength; // ระยะยิ่งไกลยิ่งสูง
        float fakeHeight = 4 * maxHeight * t * (1 - t); // Parabola

        // เพิ่ม fake height ในแกน Y (หรือ Z ถ้าอยากให้ shadow แยก)
        transform.position = new Vector3(basePos.x, basePos.y + fakeHeight, 0);
    }
}
