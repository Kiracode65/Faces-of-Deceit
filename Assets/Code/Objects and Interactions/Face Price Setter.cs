using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FacePriceSetter : MonoBehaviour
{
    [SerializeField] private int minPrice = 50;
    [SerializeField] private int maxPrice = 100000;

    [SerializeField] private FaceDetailsDisplay detailsDisplay;
    [SerializeField] private GameObject popup;

    public UnityEvent onInvalidPrice;
    public UnityEvent<int> onPriceSet;

    public static FacePriceSetter Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void OpenPopup(FaceScriptable faceToSell)
    {
        popup.SetActive(true);

        storedFace storedFace = new storedFace();
        storedFace.faceScriptable = faceToSell;
        detailsDisplay.DisplayFace(storedFace);
    }

    public void SetPrice(int price)
    {
        if (price < minPrice || price > maxPrice)
        {
            onInvalidPrice.Invoke();
            return;
        }

        onPriceSet.Invoke(price);
        popup.SetActive(false);
    }

    public void SetPriceFromString(string input)
    {
        if (int.TryParse(input, out int integerValue))
        {
            SetPrice(integerValue);
        }
    }
}
