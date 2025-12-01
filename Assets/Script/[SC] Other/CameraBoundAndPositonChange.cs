using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraBoundAndPositonChange : MonoBehaviour
{
    [SerializeField] GameObject pos;
    CinemachineConfiner2D confiner2D;
    CompositeCollider2D bound;
    void Awake()
    {
        bound = GetComponent<CompositeCollider2D>();
        confiner2D = FindAnyObjectByType<CinemachineConfiner2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            confiner2D.m_BoundingShape2D = bound;
            PlayerPosManager.instance.ResetPos();
            pos.SetActive(true);
        }
    }
}
