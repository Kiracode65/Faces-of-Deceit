using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text characterNameTextField;
    [SerializeField] private SlowTextDisplay characterSpeechText;
    [SerializeField] private Button continueSkipButton;
    [SerializeField] private TMP_Text continueSkipButtonText;

    [SerializeField] private DialogueSegmentScriptable currentDialogueSegment;
    [SerializeField] private int currentDialogueSegmentIndex;

    public UnityEvent onDialogueComplete;

    public static DialogueSystem instance;
    //

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


    public void StartDialogue(DialogueSegmentScriptable segment)
    {
        currentDialogueSegment = segment;
        currentDialogueSegmentIndex = 0;
        DisplayDialoguePiece();
    }

    #region Internal Functions

    private void DisplayDialoguePiece()
    {
        characterNameTextField.text = currentDialogueSegment.dialoguePieces[currentDialogueSegmentIndex].characterName;
        characterSpeechText.StartDisplayTextBySpeed(currentDialogueSegment.dialoguePieces[currentDialogueSegmentIndex].text);
        continueSkipButtonText.text = "Skip";
    }

    private void ContinueDialogue()
    {
        currentDialogueSegmentIndex++;

        if (currentDialogueSegmentIndex >= currentDialogueSegment.dialoguePieces.Length - 1)
        {
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
    }

    private void CharacterTextCompleted()
    {
        continueSkipButtonText.text = "Continue";
    }

    #endregion
}
