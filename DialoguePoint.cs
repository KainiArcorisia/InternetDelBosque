using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialoguePoint nextDialogue;
}

[CreateAssetMenu(fileName = "New Dialogue Point", menuName = "Dialogue System/Dialogue Point")]
public class DialoguePoint : ScriptableObject
{
    public Sprite backgroundImage;
    public Sprite characterImage;
    public string title;
    [TextArea(3, 10)]
    public string content;
    public List<DialogueChoice> choices;
}