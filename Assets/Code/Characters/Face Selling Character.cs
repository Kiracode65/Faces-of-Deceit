using UnityEngine;
using UnityEngine.Events;

public class FaceSellingCharacter : Character
{
    public FaceScriptable faceToSell;

    [SerializeField] private DialogueSegmentScriptable highPriceReaction;
    [SerializeField] private DialogueSegmentScriptable lowPriceReaction;
    [SerializeField] private DialogueSegmentScriptable expectedPriceReaction;

    public UnityEvent onLowPrice;
    public UnityEvent onHighPrice;
    public UnityEvent onExpectedPrice;

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
            onLowPrice?.Invoke();
        }
        else if (price > maxExpectedPrice)
        {
            DialogueSystem.instance.StartDialogue(highPriceReaction);
            onHighPrice?.Invoke();
        }
        else
        {
            DialogueSystem.instance.StartDialogue(expectedPriceReaction);
            onExpectedPrice?.Invoke();
        }

        storedFace faceDetails = new storedFace();
        faceDetails.faceScriptable = faceToSell;
        faceDetails.assignedValue = price;
        InventoryManager.Instance.StoreFace(faceDetails);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        FacePriceSetter.Instance.onPriceSet.RemoveListener(PriceSet);
    }
}
