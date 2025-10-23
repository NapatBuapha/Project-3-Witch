using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ThrowBombAtPlayer : MonoBehaviour
{
    [SerializeField] Transform thrownPoint;
    [SerializeField] GameObject bombPrefab;

    [SerializeField] private float minRandom, maxRandom;
    Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void Thrown()
    {
        GameObject bomb = Instantiate(bombPrefab, thrownPoint.position, quaternion.identity);
        bomb.GetComponent<FakeArcProjectile>().target = new Vector2(player.position.x + UnityEngine.Random.Range(minRandom, maxRandom), player.position.y + UnityEngine.Random.Range(minRandom, maxRandom));
    }
}
