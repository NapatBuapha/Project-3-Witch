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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
