using UnityEngine;
using TMPro;

public class AmbassadorsPriceCalculation : MonoBehaviour
{
    private BudgetPlanning budgetPlanning;
    public BoostPriceChanger boostPrice;
    public TMP_Text totalPromoPriceText;
    private float totalPromotionPrice;

    void Start()
    {
        // Cache the BudgetPlanning instance to avoid repeated searches
        budgetPlanning = FindObjectOfType<BudgetPlanning>();
        if (budgetPlanning == null)
        {
            Debug.LogError("BudgetPlanning not found in the scene.");
        }
    }

    public void CalculateAllAmbassadorsPrice()
    {
        // Reset total promotion price and calculate new price
        totalPromotionPrice = Mathf.Round(boostPrice.boostPrice);

        // Update the total price UI and budget
        totalPromoPriceText.text = $"{totalPromotionPrice}$";
        budgetPlanning.ambassadorsPrices = totalPromotionPrice;

        // Recalculate promotion
        budgetPlanning.CalculateAmbassadorsPromotion();
    }

    public void DropAmbassadorsPrices()
    {
        // Reset boost price and total promotion price to zero
        boostPrice.boostPrice = 0;
        totalPromotionPrice = 0;

        // Update the UI and budget
        totalPromoPriceText.text = $"{totalPromotionPrice}$";
        budgetPlanning.ambassadorsPrices = totalPromotionPrice;

        // Recalculate promotion
        budgetPlanning.CalculateAmbassadorsPromotion();
    }
}
