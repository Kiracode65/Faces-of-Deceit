using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueSegmentScriptable currentDialogueSegment;
    [SerializeField] private int currentDialogueSegmentIndex;
    [Space]
    [SerializeField] private TMP_Text characterNameTextField;
    [SerializeField] private SlowTextDisplay characterSpeechText;
    [SerializeField] private Button continueSkipButton;
    [SerializeField] private TMP_Text continueSkipButtonText;
    [SerializeField] private GameObject dialogueBox;

    public UnityEvent onDialogueComplete;

    public static DialogueSystem instance;
  

    #region UnityMessages
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        characterSpeechText.onComplete.AddListener(CharacterTextCompleted);
        continueSkipButton.onClick.AddListener(ContinueButtonClicked);
    }

    private void OnDestroy()
    {
        //SaveData.instance.currentData.currentDialogueSegment = currentDialogueSegment;
        //SaveData.instance.currentData.currentDialogueIndex = currentDialogueSegmentIndex;

        characterSpeechText.onComplete.RemoveListener(CharacterTextCompleted);
        continueSkipButton.onClick.RemoveListener(ContinueButtonClicked);
    }

    #endregion

    private void ContinueButtonClicked()
    {
        if (characterSpeechText.Displaying)
        {
            SkipTextDisplay();
        }
        else
        {
            ContinueDialogue();
        }
    }

    private void ToggleDialogueBox(bool toggle)
    {
        dialogueBox.SetActive  (toggle);
    }



    public void StartDialogue(DialogueSegmentScriptable segment)
    {
        currentDialogueSegment = segment;
        currentDialogueSegmentIndex = 0;

        if (SaveData.instance != null && SaveData.instance.hasLoadedData)
        {
            if (SaveData.instance.currentData.currentDialogue != null && segment == SaveData.instance.currentData.currentDialogue) // loading the index if possible
            {
                currentDialogueSegmentIndex = SaveData.instance.currentData.currentDialogueIndex;
            }

            SaveData.instance.currentData.currentDialogue = currentDialogueSegment; // setting the segment
        }

        ToggleDialogueBox (true);
        DisplayDialoguePiece();

    }

    #region Internal Functions

    private void DisplayDialoguePiece()
    {
        if(currentDialogueSegment.dialoguePieces == null || currentDialogueSegment.dialoguePieces.Length == 0)
        {
            Debug.LogError("No dialogue pieces inside scriptable");
            return;
        }

        if (SaveData.instance != null && SaveData.instance.hasLoadedData)
        {

            SaveData.instance.currentData.currentDialogueIndex = currentDialogueSegmentIndex;
        }

        characterNameTextField.text = currentDialogueSegment.dialoguePieces[currentDialogueSegmentIndex].characterName;
        characterSpeechText.StartDisplayTextBySpeed(currentDialogueSegment.dialoguePieces[currentDialogueSegmentIndex].text);
        continueSkipButton.gameObject.SetActive(true);
        continueSkipButtonText.text = "Skip";
    }

    private void ContinueDialogue()
    {
        currentDialogueSegmentIndex++;

        if (currentDialogueSegmentIndex >= currentDialogueSegment.dialoguePieces.Length)
        {
            ToggleDialogueBox(false);
            CompleteDialogue();
            
        }
        else
        {
            DisplayDialoguePiece();
        }
    }

    private void SkipTextDisplay()
    {
        characterSpeechText.SkipText();
    }

    private void CompleteDialogue()
    {
        Debug.Log("Dialogue Complete");
        characterSpeechText.ClearText();
        onDialogueComplete?.Invoke();
        continueSkipButton.gameObject.SetActive(false);
    }

    private void CharacterTextCompleted()
    {
        continueSkipButtonText.text = "Continue";
    }

    #endregion
}
