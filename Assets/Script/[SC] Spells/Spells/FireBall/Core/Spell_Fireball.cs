using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//ใช้สร้าง scriptable object ใช้เสร็จเเล้วปิดทิ้งซะกันเปลืองที่
[CreateAssetMenu(fileName = "NewFireball", menuName = "Spells/Fireball")]
public class Spell_Fireball : SpellBase
{
    #region ค่าทั่วไป
    [Header("Spell Value")]
    [SerializeField] private string spellID_ = "01";
    [SerializeField] private string thisName_ = "Fire Ball";
    [SerializeField] private int manaCost_ = 2;
    [SerializeField] private float maxCD_ = 2f;
    [SerializeField] private float castingDura_ = 0.25f;
    [SerializeField] private Sprite icon_;
    [SerializeField] [TextArea] private string desc_;


    #endregion

    #region ค่าสำหรับลูกไฟ
    [Header("Fireball Effect Value")]
    [SerializeField] private float speed = 10f;
        [SerializeField] private GameObject spellPrefab;
    #endregion



    private void OnEnable()
    {
        init(spellID_, thisName_, manaCost_, maxCD_,castingDura_,icon_,desc_);
    }

    public override void UseSpell()
    {
        base.UseSpell();
        //หาตำเเหน่ง player กับ mouse
        Transform playerPos = player.transform;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 direction = (mouseWorldPos - playerPos.position).normalized;

        //สร้างเเล้วส่งเเรงดันให้เคลื่อนที่
        GameObject fb = Instantiate(spellPrefab, playerPos.position, quaternion.identity);
        Rigidbody2D fbRb = fb.GetComponent<Rigidbody2D>();
        fbRb.AddForce(direction * speed , ForceMode2D.Impulse);
    }

    public override void Penalty()
    {
        base.Penalty();
    }
}
