using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastBoarderFilter : MonoBehaviour
{
    public static BeastBoarderFilter instance;
    [SerializeField] private Animator[] animators;
    void Awake()
    {
        instance = this;
    }

    public void CallFilter()
    {
        foreach(Animator animator in animators)
        {
            animator.SetTrigger("Call");
        }
    }

    public void DisableFilter()
    {
        foreach(Animator animator in animators)
        {
            animator.SetTrigger("End");
        }
    }
}
