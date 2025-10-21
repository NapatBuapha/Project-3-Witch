using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField] private TeleportGate linkedGates;
    private Transform destination;
    [SerializeField] private Vector3 positionAdjustment;
    private CinemachineConfiner2D camConfiner;
    [SerializeField] private PolygonCollider2D camBounds;
    public GameObject levelHeader;

    void Start()
    {
        destination = linkedGates.transform;
        camConfiner = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineConfiner2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.collider.gameObject;
        if(target.CompareTag("Player"))
        {
            levelHeader.SetActive(false);
            linkedGates.levelHeader.SetActive(true);
            
            camConfiner.m_BoundingShape2D = linkedGates.camBounds;

            target.transform.position = new Vector3(
            destination.position.x + positionAdjustment.x,
            destination.position.y + positionAdjustment.y,
            0);
        }
    }
}
