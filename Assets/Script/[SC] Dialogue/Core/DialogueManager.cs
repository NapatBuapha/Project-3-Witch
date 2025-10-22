using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    DialogueBoxManager dialogueBox;
    [SerializeField] protected int currentLine;
    public Dialogue currentDialogue;

    [SerializeField] private bool isDialogue;

    void Start()
    {
        isDialogue = false;
        currentLine = 0;
        dialogueBox = GameObject.FindWithTag("DialogueBox").GetComponent<DialogueBoxManager>();
    }

    void Update()
    {
        if(!isDialogue)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            NextLine();
        }
    }

    public void StartDialougue()
    {
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
            
        }

    }
}
