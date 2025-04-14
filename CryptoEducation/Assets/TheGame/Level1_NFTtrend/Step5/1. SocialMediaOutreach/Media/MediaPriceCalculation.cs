using UnityEngine;
using TMPro;

public class MediaPriceCalculation : MonoBehaviour
{
    private BadgetPlaning badgetPlaning;
    private SetMedia setMedia;

    public TMP_Text totalPromoPriceText;
    public float totalPromotionPrice;

    void Start()
    {
        badgetPlaning = FindObjectOfType<BadgetPlaning>();
        setMedia = FindObjectOfType<SetMedia>();
    }

    // Calculates the total price of selected media
    public void CalculateAllMediaPrice()
    {
        totalPromotionPrice = 0;

        foreach (GameObject priceObject in setMedia.Prices4selectedMedia)
        {
            float price = priceObject.GetComponent<BoostPriceChanger>().boostPrice;
            totalPromotionPrice += Mathf.Round(price);
        }

        // Update total budget and display it
        totalPromoPriceText.text = $"Used ${totalPromotionPrice}";
        badgetPlaning.mediaPrices = totalPromotionPrice;
        badgetPlaning.CalculateMediaPromotion();

        // Show animation if the total promotion price exceeds the planned budget
        GetComponent<DisplayPlanedMediaBudget>().ShowHighlightAnimation();
    }

    // Resets all media prices
    public void DropMediaPrices()
    {
        foreach (GameObject priceObject in setMedia.Prices4selectedMedia)
        {
            BoostPriceChanger boostPriceChanger = priceObject.GetComponent<BoostPriceChanger>();
            boostPriceChanger.boostPrice = 0; // Reset boost price to default
            priceObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "0$"; // Reset text display
        }

        // Reset the total promotion price and update budget
        totalPromotionPrice = 0;
        totalPromoPriceText.text = $"Used ${totalPromotionPrice}";
        badgetPlaning.mediaPrices = totalPromotionPrice;
        badgetPlaning.CalculateMediaPromotion();
    }
}
