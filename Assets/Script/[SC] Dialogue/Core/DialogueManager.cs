using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    DialogueBoxManager dialogueBox;
    [SerializeField] protected int currentLine;
    public Dialogue currentDialogue;

    [SerializeField] private bool isDialogue;
    [SerializeField] GameObject[] otherUi;

    void Start()
    {
        instance = this;
        isDialogue = false;
        currentLine = 0;

        instance = this;
        dialogueBox = GameObject.FindWithTag("DialogueBox").GetComponent<DialogueBoxManager>();
    }


    void HideOtherCanvas()
    {
        foreach (GameObject ui in otherUi)
        {
            CanvasGroup cg = ui.GetComponent<CanvasGroup>();
            cg.alpha = 0;            // ทำให้มองไม่เห็น
            cg.interactable = false; // ป้องกันการคลิก
            cg.blocksRaycasts = false; // ไม่ให้รับ event
        }
    }
    
    void UnHideOtherCanvas()
    {
        foreach(GameObject ui in otherUi)
        {
            CanvasGroup cg = ui.GetComponent<CanvasGroup>();
            cg.alpha = 1;            // ทำให้มองไม่เห็น
            cg.interactable = true; // ป้องกันการคลิก
            cg.blocksRaycasts = true; // ไม่ให้รับ event
        }
    }

    void Update()
    {
        if (!isDialogue)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }
    
    public static void SetDialogue(Dialogue dialogue)
    {
        instance.currentDialogue = dialogue;
        instance.StartDialougue();
    }

    public void StartDialougue()
    {
        HideOtherCanvas();
        GameStateManager.instance.ChangeState(GameStateManager.GameState.DialogueSequence);
        isDialogue = true;

        if (dialogueBox == null)
        {
            return;
        }
        dialogueBox.OpenBox();

        //เช็คว่า icon กับชื่อ ควรอยู่ซ้ายหรือขวา
        if (currentDialogue.isLeft)
        {
            dialogueBox.UpdateLeftSpeaker(currentDialogue.speakerIcon, currentDialogue.speakerName);
        }
        else
        {
            dialogueBox.UpdateRightSpeaker(currentDialogue.speakerIcon, currentDialogue.speakerName);
        }

        dialogueBox.UpdateText(currentDialogue.dialogueLine[currentLine]);
    }

    private void NextLine()
    {
        currentLine++;
        if(currentLine < currentDialogue.dialogueLine.Length)
        {
            dialogueBox.UpdateText(currentDialogue.dialogueLine[currentLine]);
        }
        else
        {
            EndDialougue();
        }
    }
    
    public void EndDialougue()
    {
        if (currentDialogue.nextLine != null)
        {
            currentDialogue = currentDialogue.nextLine;
            currentLine = 0;
            StartDialougue();
        }
        else
        {
            isDialogue = false;
            currentLine = 0;
            currentDialogue = null;
            dialogueBox.CloseBox();
            GameStateManager.instance.ChangeState(GameStateManager.GameState.Default);
            UnHideOtherCanvas();
            
        }

    }
}
