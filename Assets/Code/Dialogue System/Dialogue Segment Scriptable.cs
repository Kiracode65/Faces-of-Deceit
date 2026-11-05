using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueSegment", menuName = "Scriptable Objects/DialogueSegment")]
public class DialogueSegmentScriptable : ScriptableObject
{
    public DialoguePiece[] dialoguePieces;
}

[Serializable]
public struct DialoguePiece
{
    public string characterName;
    [TextArea]
    public string text;

    [Header("Leave empty for none")]
    public DialogueOption[] playerChoices;
}

[Serializable]
public struct DialogueOption
{
    public string id;
    public string text;
    public DialogueSegmentScriptable dialogueToTrigger;
}
