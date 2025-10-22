using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New_Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea] public string[] dialogueLine;

    public Dialogue nextLine;
    public string speakerName;
    public Sprite speakerIcon;
    public bool isLeft;
    
}
