using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class Hearth : MonoBehaviour
{
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite fullSprite;
    Image image;
    [SerializeField] int containValue;
    [SerializeField] Animator animator;
    public bool isDestroying;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        isDestroying = false;
        image = GetComponent<Image>();
        
        containValue = 0;
    }

    public void UpdateValue(int value)
    {
        if(containValue != value)
        {
            containValue = value;
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        if (containValue > 0)
        {
            image.sprite = fullSprite;
        }
        else
        {
            image.sprite = emptySprite;
        }
    }

    public void SelfDestroy()
    {
        isDestroying = true;
        animator.enabled = true;
        AudioManager.PlaySound(SoundType.Hearth_Breaking);
        //ทำ animation ตรงนี้
        if(containValue == 1)
        animator.SetTrigger("Full Breaking");
        else
        animator.SetTrigger("Empty Breaking");
    }

    public void DestroyEvent()
    {
        Destroy(gameObject);
    }
}
