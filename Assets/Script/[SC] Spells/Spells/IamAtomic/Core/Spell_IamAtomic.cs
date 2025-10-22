using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//�����ҧ scriptable object ����������ǻԴ��駫Сѹ���ͧ���
[CreateAssetMenu(fileName = "New_I am Atomic!", menuName = "Spells/I am Atomic!")]
public class Spell_IamAtomic : SpellBase
{
    #region ��ҷ����
    [Header("Spell Value")]
    [SerializeField] private string spellID_ = "06";
    [SerializeField] private string thisName_ = "I am Atomic!";
    [SerializeField] private int manaCost_ = 80;
    [SerializeField] private float maxCD_ = 20f;
    [SerializeField] private float castingDura_ = 1f;
    [SerializeField] private Sprite icon_;
    [SerializeField][TextArea] private string desc_;


    #endregion

    #region �������Ѻ���Դ
    [Header("Bomb Effect Value")]
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private int hpCost;
    #endregion



    private void OnEnable()
    {
        init(spellID_, thisName_, manaCost_, maxCD_, castingDura_, icon_, desc_);
    }

    public override void BeforeCasting()
    {
        base.BeforeCasting();
        //���ҧ�������Դ
        GameObject fb = Instantiate(spellPrefab, player.transform.position, quaternion.identity);
    }

    public override void UseSpell()
    {
        base.UseSpell();
        Penalty();
    }

    public override void Penalty()
    {
        base.Penalty();
        PlayerHpManager p_HP = player.GetComponent<PlayerHpManager>();

        if (p_HP.hp > hpCost)
            p_HP.PayHealth(hpCost);
        
          B_and_DB_Manager debuffManager = player.GetComponent<B_and_DB_Manager>();
          debuffManager.FindDBB("903");

        
        
    }
}
