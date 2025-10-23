using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFinish : StateMachineBehaviour
{
    [SerializeField] private string animation;
    [SerializeField] BaseEnemyAnimationController enemator;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        enemator = animator.gameObject.transform.parent.GetComponent<BaseEnemyAnimationController>();

        enemator.ChangeAnimation(animation,0f,stateInfo.length);
    }
}
