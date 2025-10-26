using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Filter : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void EnterBeast()
    {
        animator.SetBool("IsBeastMode",true);
    }
    
    public void EndBeast()
    {
        animator.SetBool("IsBeastMode",false);
    }
}
