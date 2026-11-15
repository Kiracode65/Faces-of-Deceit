using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private DialogueSegmentScriptable entryDialogue;
    public UnityEvent onDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialise()
    {
        DialogueSystem.instance.StartDialogue(entryDialogue);
        DialogueSystem.instance.onDialogueComplete.AddListener(DialogueCompleted);
    }

    public virtual void DialogueCompleted()
    {
        Destroy(gameObject);
    }

    public virtual void OnDestroy()
    {
        onDestroy.Invoke();
    }
}
