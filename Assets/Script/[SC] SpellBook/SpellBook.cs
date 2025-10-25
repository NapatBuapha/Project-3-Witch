using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    enum SpellBookState
    {
        Default,
        Invisible,
        Ak47
    }


    SpellBookState currentState;
    public SpriteRenderer render;
    [SerializeField] private Sprite[] book_Sprite = new Sprite[3];
    // 0 ด้านหน้า
    // 1 ด้านข้าง
    // 2 ด้านหลัง
    [SerializeField] private Sprite ak47Sprite;
    Vector3 mousePos;

    public Transform spellBookPos; //เพื่ออ้างอิงตำเเหน่ง spell book จริงๆ
    public float z;

    void Start()
    {
        render.sprite = book_Sprite[0];
    }

    void Update()
    {
        RotateTowardMouse();

        if (currentState == SpellBookState.Default)
        {
            render.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            UpdateDefaultSprite();
        }
        
        
        

    }

    public void ChangeState(int state = 0)
    {
        switch(state)
        {
            case 1:
                currentState = SpellBookState.Invisible;
                render.enabled = false;
                break;
            case 2:
                currentState = SpellBookState.Ak47;
                render.sprite = ak47Sprite;
                render.enabled = true;
                break;
            default:
                currentState = SpellBookState.Default;
                render.enabled = true;
                break;
        }
    }

    void UpdateDefaultSprite()
    {

        if (z < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (MathF.Abs(z) <= 40)
        {
            render.sprite = book_Sprite[2];
        }
        else if (MathF.Abs(z) <= 120 && MathF.Abs(z) > 40)
        {
            render.sprite = book_Sprite[1];
        }
        else if (MathF.Abs(z) <= 180 && MathF.Abs(z) > 120)
        {
            render.sprite = book_Sprite[0];
        }
    }

    void RotateTowardMouse()
    {

    // แปลงตำแหน่งเมาส์จากจอ → โลก
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // ป้องกันไม่ให้ z ของกล้องมามีผล

        // คำนวณทิศทางจากตำแหน่งหนังสือ → เมาส์
        Vector2 lookDir = (mousePos - transform.position).normalized;

        // คำนวณมุมที่ต้องหมุน
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // หมุนวัตถุตามมุมที่ได้
        transform.rotation = Quaternion.Euler(0, 0, angle);

        z = transform.rotation.eulerAngles.z; ;
        if (z > 180f)
            z -= 360f;
    }


}
