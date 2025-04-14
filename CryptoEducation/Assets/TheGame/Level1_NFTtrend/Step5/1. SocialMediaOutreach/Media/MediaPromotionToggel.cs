using UnityEngine;

public class MediaPromotionToggle : MonoBehaviour
{
    public TheToggle toggle;
    private SetMedia setMedia;
    private MediaPriceCalculation mediaPriceCalculation;
    public GameObject totalPromotionPrice;

    private void Start()
    {
        setMedia = FindObjectOfType<SetMedia>();
        mediaPriceCalculation = FindObjectOfType<MediaPriceCalculation>();
    }

    // Handles the promotion toggle change
    public void SelectPromotionToggle()
    {
        bool isPromotionEnabled = toggle.isOn;

        // Activate or deactivate promotion
        setMedia.isPromotionOn = isPromotionEnabled;
        setMedia.MediaPricesActivator(isPromotionEnabled);
        totalPromotionPrice.SetActive(isPromotionEnabled);

        // Recalculate prices or drop them based on promotion status
        if (isPromotionEnabled)
        {
            mediaPriceCalculation.CalculateAllMediaPrice();
        }
        else
        {
            mediaPriceCalculation.DropMediaPrices();
        }
    }
}
