using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public int currentStage = 0;
    public DialogueSegmentScriptable entryDialogue;

    public UnityEvent onDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialise(int jumpToStage = 0)
    {
        DialogueSystem.instance.onDialogueComplete.AddListener(DialogueCompleted);

        currentStage = jumpToStage;
        StartStage(currentStage);
    }

    public virtual void StartStage(int stage)
    {
        SaveData.instance.currentData.currentCharacterStage = stage;
    }

    public void AdvanceStage()
    {
        currentStage++;
        if(SaveData.instance != null && SaveData.instance.hasLoadedData) SaveData.instance.currentData.currentCharacterStage = currentStage;
        StartStage(currentStage);
    }

    public virtual void DialogueCompleted()
    {
        AdvanceStage();
    }

    public virtual void OnDestroy()
    {
        onDestroy.Invoke();
    }
}
