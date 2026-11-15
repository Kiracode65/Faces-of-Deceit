using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private DialogueSegmentScriptable entryDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueSystem.instance.StartDialogue(entryDialogue);
        DialogueSystem.instance.onDialogueComplete.AddListener(DialogueCompleted);
    }

    public virtual void DialogueCompleted()
    {
        Destroy(gameObject);
    }
}
