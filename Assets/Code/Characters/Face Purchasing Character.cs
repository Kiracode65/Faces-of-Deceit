using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class FacePurchasingCharacter : Character
{
    [SerializeField] private List<FaceAttribute> desiredAttributes;
    [SerializeField] private List<FaceAttribute> unWantedAttributes;

    [SerializeField] private DialogueSegmentScriptable successDialogue;
    [SerializeField] private DialogueSegmentScriptable failureDialogue;

    public UnityEvent onSuccess;
    public UnityEvent onFailure;

    [SerializeField] private bool satisfied = false;
    [SerializeField] private bool canReceiveFace = false;



    public override void StartStage(int stage)
    {
        base.StartStage(stage);

        switch (stage)
        {
            case 0: // entry dialogue
                DialogueSystem.instance.StartDialogue(entryDialogue);
                canReceiveFace = false;
                break;

            case 1:
                canReceiveFace = true;
                break;

            case 2: // has received face
                canReceiveFace = false;

                if (satisfied)
                {
                    DialogueSystem.instance.StartDialogue(successDialogue);
                    onSuccess?.Invoke();
                }
                else
                {
                    DialogueSystem.instance.StartDialogue(failureDialogue);
                    onFailure?.Invoke();
                }
                break;

            default:
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canReceiveFace && other.gameObject.CompareTag("Face") && other.TryGetComponent(out FaceObject face))
        {
            ReceiveFace(face);
        }
    }

    private void ReceiveFace(FaceObject face) // triggers when we get a face
    {
        if (HasCorrectFaceAttributes(face))
        {
            satisfied = true;
            ScoreManager.instance.ChangeScore(face.FaceDetails.assignedValue);
            face.SellFace();
            Debug.Log("Satisfied Customer");
        }
        else
        {
            satisfied = false;
            face.StoreFace();
            Debug.Log("Unhappy Customer");
        }

        AdvanceStage();
    }

    private bool HasCorrectFaceAttributes(FaceObject face)
    {

        for (int i = 0; i < face.FaceDetails.faceScriptable.attributes.Count; i++) // if the face has any unwanted attributes
        {
            if (unWantedAttributes.Contains(face.FaceDetails.faceScriptable.attributes[i]))
            {
                Debug.Log("Face contained unwanted attributes");
                return false;
            }
             
        }

        for (int i = 0; i < desiredAttributes.Count; i++)// if the face does not contain any desired attributes
        {
            if (!face.FaceDetails.faceScriptable.attributes.Contains(desiredAttributes[i]))
            {
                Debug.Log("Face Did not have all required attributes");
                return false;
            }
        }

        return true;
    }
}
