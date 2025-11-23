using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // ระยะการลอยขึ้นลง
    public float frequency = 1f;   // ความเร็วการลอย

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Mathf.Sin(Time.time * frequency) จะออกค่า -1 ถึง 1
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}

