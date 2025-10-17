using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanityGauge : MonoBehaviour
{
    [SerializeField] private float maxValue = 100;
    private float humanityValue;
    public float HumanityValue
    {
        get { return humanityValue; }
        set
        {
            if (value > maxValue)
            {
                humanityValue = maxValue;
            }
            else
            {
                humanityValue = value;
            }
        }
    }

    Queue<float> corruptEventThreshold = new Queue<float>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
