using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private DialogueSegmentScriptable dialogueToInvoke;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueSystem.instance.StartDialogue(dialogueToInvoke);
        DialogueSystem.instance.onDialogueComplete.AddListener(DialogueCompleted);
    }

    private void DialogueCompleted()
    {
        Destroy(gameObject);
    }
}
