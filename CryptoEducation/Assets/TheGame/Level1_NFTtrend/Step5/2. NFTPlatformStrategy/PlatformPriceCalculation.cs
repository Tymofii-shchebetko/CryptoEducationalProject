using UnityEngine;

public class PlatformPriceCalculation : MonoBehaviour
{
    private BudgetPlanning budgetPlanning;  // Corrected spelling of 'BudgetPlanning'

    public float totalPromotionPrice;
    public float totalMarketplacePercent; // Percentage taken by marketplaces for promotion

    private int launchpadPrice; // Price for promotion on the launchpad
    private int calendarPrice; // Price for promotion through calendars

    void Start()
    {
        // Find BudgetPlanning object in the scene
        budgetPlanning = FindObjectOfType<BudgetPlanning>();
    }

    public void SetLaunchpadPrice(int price)
    {
        launchpadPrice = price;
        RecalculateTotalPrice();
    }

    public void SetCalendarPrice(int price)
    {
        calendarPrice = price;
        RecalculateTotalPrice();
    }

    // Recalculate the total price for all platforms
    private void RecalculateTotalPrice()
    {
        totalPromotionPrice = launchpadPrice + calendarPrice;

        // Update the total platform prices in the budget planning
        budgetPlanning.platformsPrices = totalPromotionPrice;
        budgetPlanning.CalculatePlatformsPromotion();
    }

    // Reset the promotion prices (used when toggling promotion off)
    public void ResetPromotionPrice()
    {
        totalPromotionPrice = 0;
        budgetPlanning.platformsPrices = totalPromotionPrice;
        budgetPlanning.CalculatePlatformsPromotion();
    }

    // Set the percentage for marketplace promotion fee
    public void SetMarketplacePercent(float percent)
    {
        totalMarketplacePercent = percent;
        Debug.Log($"Marketplace promotion fee set to: {totalMarketplacePercent}%");
    }
}
