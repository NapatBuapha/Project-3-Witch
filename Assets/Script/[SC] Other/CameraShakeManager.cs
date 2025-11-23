using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;
    [SerializeField] private float globalShakeForce = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    
    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce = -1)
    {
        if(shakeForce == -1)
        {
            shakeForce = globalShakeForce;
        }
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }


}
