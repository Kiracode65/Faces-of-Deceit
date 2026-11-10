using System.Collections.Generic;
using UnityEngine;

public class FacePurchasingCharacter : Character
{
    [SerializeField] private List<FaceAttribute> desiredAttributes;
    [SerializeField] private List<FaceAttribute> unWantedAttributes;

    [SerializeField] private DialogueSegmentScriptable successDialogue;
    [SerializeField] private DialogueSegmentScriptable failureDialogue;

    private FaceObject receivedFace = null;
    private bool satisfied = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Face") && other.TryGetComponent(out FaceObject face))
        {
            ReceiveFace(face);
        }
    }

    private void ReceiveFace(FaceObject face) // triggers when we get a face
    {
        receivedFace = face;

        if (HasCorrectFaceAttributes(face))
        {
            DialogueSystem.instance.StartDialogue(successDialogue);
            face.SellFace();
            Debug.Log("Satisfied Customer");
        }
        else
        {
            DialogueSystem.instance.StartDialogue(failureDialogue);
            face.ReturnToShelf();
            Debug.Log("Unhappy Customer");
        }
    }

    public override void DialogueCompleted()
    {
        if(receivedFace == null) return;

        if (satisfied)
        {
            ScoreManager.instance.ChangeScore(receivedFace.Value);
        }
        else
        {
            // not implemented
        }

        Destroy(gameObject);
    }

    private bool HasCorrectFaceAttributes(FaceObject face)
    {

        for (int i = 0; i < face.attributes.Count; i++) // if the face has any unwanted attributes
        {
            if (unWantedAttributes.Contains(face.attributes[i]))
            {
                Debug.Log("Face contained unwanted attributes");
                return false;
            }
             
        }

        for (int i = 0; i < desiredAttributes.Count; i++)// if the face does not contain any desired attributes
        {
            if (!face.attributes.Contains(desiredAttributes[i]))
            {
                Debug.Log("Face Did not have all required attributes");
                return false;
            }
        }

        return true;
    }
}
