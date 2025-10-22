using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    #region  ref
    [Header("UI ref")]

    [SerializeField] private TMP_Text textBox;


    [Header("Name")]
    [SerializeField] private TMP_Text l_NameText;
    [SerializeField] private TMP_Text r_NameText;
    [Header("Icon")]
    [SerializeField] private Image l_Icon;
    [SerializeField] private Image r_Icon;
    [Header("Parent")]
    [SerializeField] private GameObject uiParent;
    [SerializeField] private GameObject l_Parent;
    [SerializeField] private GameObject r_Parent;
    #endregion
    void Start()
    {
        l_Parent.SetActive(false);
        r_Parent.SetActive(false);
        uiParent.SetActive(false);
    }

    public void UpdateText(string dialogue)
    {
        textBox.text = dialogue;
    }

    public void UpdateLeftSpeaker(Sprite icon, string name_)
    {
        l_Parent.SetActive(true);
        SetIconBlack(r_Icon);
        SetNormal(l_Icon);

        l_Icon.sprite = icon;
        l_NameText.text = name_;
    }

    public void UpdateRightSpeaker(Sprite icon, string name_)
    {
        r_Parent.SetActive(true);
        SetIconBlack(l_Icon);
        SetNormal(r_Icon);

        r_Icon.sprite = icon;
        r_NameText.text = name_;
    }

    private void SetIconBlack(Image icon)
    {
        icon.color = new Color(0.25f, 0.25f, 0.25f, 1f);
    }

    private void SetNormal(Image icon)
    {
        icon.color = new Color(1f, 1f, 1f, 1f);
    }

    public void OpenBox()
    {
        uiParent.SetActive(true);
    }

        public void CloseBox()
    {
        uiParent.SetActive(false);
    }
    



}
