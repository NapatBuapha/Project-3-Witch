using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;

public class UpdateSpriteDirectionAI : MonoBehaviour
{
    public AIPath aIPath;
    void Start()
    {
        aIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
