using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceDetailsDisplay : MonoBehaviour
{
    [SerializeField] private Image faceIconSlot;
    [SerializeField] private TMP_Text faceNameSlot;
    [SerializeField] private TMP_Text faceDescriptionSlot;
    [SerializeField] private TMP_Text faceAttributesSlot;
    [SerializeField] private TMP_Text faceValueSlot;

    public void DisplayFace(storedFace face)
    {
        if(faceIconSlot != null) faceIconSlot.sprite = face.faceScriptable.sprite;
        if(faceNameSlot != null) faceNameSlot.text = face.faceScriptable.name;
        if(faceDescriptionSlot != null) faceDescriptionSlot.text = face.faceScriptable.description;

        if (faceValueSlot != null) faceValueSlot.text = face.assignedValue.ToString();

        if(faceAttributesSlot != null)
        {
            string attributes = "";

            for (global::System.Int32 i = 0; i < face.faceScriptable.attributes.Count; i++)
            {
                attributes += face.faceScriptable.attributes[i].ToString();
                attributes += ", ";
            }

            faceAttributesSlot.text = attributes;
        }
    }
}
