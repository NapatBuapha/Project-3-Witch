using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerFinish : StateMachineBehaviour
{
    [SerializeField] private string animation;
    [SerializeField] PlayerAnimationController controller;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       controller = animator.gameObject.transform.parent.GetComponent<PlayerAnimationController>();

       controller.ChangeAnimation(animation,0f,stateInfo.length);
    }
}
