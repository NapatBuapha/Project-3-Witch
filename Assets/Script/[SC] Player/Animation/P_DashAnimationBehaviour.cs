using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DashAnimationBehaviour : StateMachineBehaviour
{
    [SerializeField] private string animation;
    [SerializeField] BaseEnemyAnimationController enemator;
    private BasePlayerData playerData;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        enemator = animator.gameObject.transform.parent.GetComponent<BaseEnemyAnimationController>();

        enemator.ChangeAnimation(animation,0f,stateInfo.length);
    }
}
