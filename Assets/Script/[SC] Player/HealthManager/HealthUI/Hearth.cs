using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite fullSprite;
    Image image;
    int containValue;

    void Awake()
    {
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
        //ทำ animation ตรงนี้
        Destroy(gameObject);
    }
}
