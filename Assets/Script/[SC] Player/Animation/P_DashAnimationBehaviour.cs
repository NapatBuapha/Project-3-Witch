using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DashAnimationBehaviour : StateMachineBehaviour
{
    [SerializeField] private string animation;
    private PlayerStatsData playerData;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Dash!!");
        playerData = GameObject.FindWithTag("Player").GetComponent<PlayerStatsData>();
        float stateDura = playerData.dashStatesTime;
        GameObject.FindWithTag("Player").GetComponent<AnimationController>().ChangeAnimation(animation,stateDura);
    }
}
