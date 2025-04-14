using UnityEngine;
using TMPro;

public class TraditionalPriceCalculation : MonoBehaviour
{
    private BadgetPlaning badgetPlaning;
    private SetTraditional setTraditional;

    public TMP_Text totalPromoPriceText; // Renamed to follow C# naming conventions
    private float totalPromotionPrice;

    void Start()
    {
        // Cache references to optimize performance
        badgetPlaning = FindObjectOfType<BadgetPlaning>();
        setTraditional = FindObjectOfType<SetTraditional>();
    }

    public void CalculateAllTraditionalPrice()
    {
        totalPromotionPrice = 0;

        // Loop through all selected prices and calculate the total promotion price
        foreach (GameObject price in setTraditional.PricesForSelectedTraditional)
        {
            BoostPriceChanger priceChanger = price.GetComponent<BoostPriceChanger>();
            if (priceChanger != null)
            {
                float thisPrice = priceChanger.boostPrice;
                totalPromotionPrice += Mathf.Round(thisPrice);
            }
        }

        // Update the total promotion price on the UI
        totalPromoPriceText.text = totalPromotionPrice + "$";

        // Update the budget for traditional promotion
        badgetPlaning.traditionalPrices = totalPromotionPrice;
        badgetPlaning.CalculateTraditionalPromotion();
    }

    public void DropTraditionalPrices()
    {
        // Reset the prices of all selected traditional media
        foreach (GameObject price in setTraditional.PricesForSelectedTraditional)
        {
            BoostPriceChanger priceChanger = price.GetComponent<BoostPriceChanger>();
            if (priceChanger != null)
            {
                priceChanger.boostPrice = 0; // Reset the price to default
                price.transform.GetChild(0).GetComponent<TMP_Text>().text = "0$"; // Update the text display
            }
        }

        // Reset the total promotion price and update the UI
        totalPromotionPrice = 0;
        totalPromoPriceText.text = totalPromotionPrice + "$";

        // Update the budget for traditional promotion
        badgetPlaning.traditionalPrices = totalPromotionPrice;
        badgetPlaning.CalculateTraditionalPromotion();
    }
}
