using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CaliburExplosionArea : MonoBehaviour
{
    [SerializeField] private Transform maxRangeRef;
    [SerializeField] private float expandingSpeed = 10f;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject explodPrefab;
    void Start()
    {
        gameObject.transform.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.x < maxRangeRef.localScale.x)
        {
            gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x + expandingSpeed * Time.deltaTime,
            gameObject.transform.localScale.y + expandingSpeed * Time.deltaTime,
            1);
        }
        else
        {
            GameObject explosive = Instantiate(explodPrefab, transform.position, quaternion.identity);
            explosive.transform.localScale = maxRangeRef.localScale;
            Destroy(parent);
        }
    }
}
