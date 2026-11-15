using UnityEngine;

public class FaceSellingCharacter : Character
{
    public FaceScriptable faceToSell;

    [SerializeField] private DialogueSegmentScriptable highPriceReaction;
    [SerializeField] private DialogueSegmentScriptable lowPriceReaction;
    [SerializeField] private DialogueSegmentScriptable expectedPriceReaction;

    [Min(0)]
    [SerializeField] private int minExpectedPrice = 0;
    [Min(0)]
    [SerializeField] private int maxExpectedPrice = 400;

    [SerializeField] private bool hasPricedFace;

    public override void DialogueCompleted()
    {
        if (hasPricedFace)
        {
            Destroy(gameObject);
        }
        else
        {
            FacePriceSetter.Instance.OpenPopup(faceToSell);
            FacePriceSetter.Instance.onPriceSet.AddListener(PriceSet);
        }
    }

    private void PriceSet(int price)
    {
        hasPricedFace = true;

        if(price < minExpectedPrice)
        {
            DialogueSystem.instance.StartDialogue(lowPriceReaction);
        }
        else if (price > maxExpectedPrice)
        {
            DialogueSystem.instance.StartDialogue(highPriceReaction);
        }
        else
        {
            DialogueSystem.instance.StartDialogue(expectedPriceReaction);
        }

        storedFace faceDetails = new storedFace();
        faceDetails.faceScriptable = faceToSell;
        faceDetails.assignedValue = price;
        InventoryManager.Instance.StoreFace(faceDetails);
    }

    private void OnDestroy()
    {
        FacePriceSetter.Instance.onPriceSet.RemoveListener(PriceSet);
    }
}
