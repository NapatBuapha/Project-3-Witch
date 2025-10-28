using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private float duraion;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(duraion);
        animator.SetTrigger("Fade");
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
        
    }
}
