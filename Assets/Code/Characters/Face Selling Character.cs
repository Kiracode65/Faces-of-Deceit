using UnityEngine;
using UnityEngine.Events;

public class FaceSellingCharacter : Character
{
    public FaceScriptable faceToSell;

    [SerializeField] private DialogueSegmentScriptable priceRefusalReaction;
    [SerializeField] private DialogueSegmentScriptable lowPriceReaction;
    [SerializeField] private DialogueSegmentScriptable expectedPriceReaction;
    [SerializeField] private DialogueSegmentScriptable highPriceReaction;

    [SerializeField] private DialogueSegmentScriptable endingDialogue;

    public UnityEvent onPriceRefused;
    public UnityEvent onLowPrice;
    public UnityEvent onExpectedPrice;
    public UnityEvent onHighPrice;

    [Space]
    public UnityEvent onCompletedProceedureLowPrice;
    public UnityEvent onCompletedProceedureExpectedPrice;
    public UnityEvent onCompletedProceedureHighPrice;

    [Min(0)]
    [SerializeField] private int absoluteMinPrice = 100;
    [Min(0)]
    [SerializeField] private int minExpectedPrice = 200;
    [Min(0)]
    [SerializeField] private int maxExpectedPrice = 400;

    private int setPrice;

    public override void StartStage(int stage)
    {
        base.StartStage(stage);

        switch (stage)
        {
            case 0: // entry dialogue
                DialogueSystem.instance.StartDialogue(entryDialogue);
                break;

            case 1:
                FacePriceSetter.Instance.OpenPopup(faceToSell);
                FacePriceSetter.Instance.onPriceSet.AddListener(PriceSet);

                break;

            case 2:
                if(setPrice < absoluteMinPrice)
                {
                    DialogueSystem.instance.StartDialogue(priceRefusalReaction);
                    onPriceRefused.Invoke();
                } 
                else if (setPrice < minExpectedPrice)
                {
                    DialogueSystem.instance.StartDialogue(lowPriceReaction);
                    onLowPrice?.Invoke();
                }
                else if (setPrice > maxExpectedPrice)
                {
                    DialogueSystem.instance.StartDialogue(highPriceReaction);
                    onHighPrice?.Invoke();
                }
                else
                {
                    DialogueSystem.instance.StartDialogue(expectedPriceReaction);
                    onExpectedPrice?.Invoke();
                }
                break;

            case 3:
                if (setPrice < absoluteMinPrice)
                {
                    Destroy(gameObject);
                }
                else if (setPrice < minExpectedPrice)
                {
                    DialogueSystem.instance.StartDialogue(endingDialogue);
                    AddFaceToInventory();
                    ScoreManager.instance.ChangeScore(setPrice);
                    onCompletedProceedureLowPrice?.Invoke();
                }
                else if (setPrice > maxExpectedPrice)
                {
                    DialogueSystem.instance.StartDialogue(endingDialogue);
                    AddFaceToInventory();
                    ScoreManager.instance.ChangeScore(setPrice);
                    onCompletedProceedureHighPrice?.Invoke();
                }
                else
                {
                    DialogueSystem.instance.StartDialogue(endingDialogue);
                    AddFaceToInventory();
                    ScoreManager.instance.ChangeScore(setPrice);
                    onCompletedProceedureExpectedPrice?.Invoke();
                }
                break;

            default:
                Destroy(gameObject);
                break;
        }
    }

    private void PriceSet(int price)
    {
        setPrice = price;
        FacePriceSetter.Instance.onPriceSet.RemoveListener(PriceSet);
        AdvanceStage();
    }

    private void AddFaceToInventory()
    {
        storedFace faceDetails = new storedFace();
        faceDetails.faceScriptable = faceToSell;
        faceDetails.assignedValue = setPrice;
        InventoryManager.Instance.StoreFace(faceDetails);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        FacePriceSetter.Instance.onPriceSet.RemoveListener(PriceSet);
    }
}
