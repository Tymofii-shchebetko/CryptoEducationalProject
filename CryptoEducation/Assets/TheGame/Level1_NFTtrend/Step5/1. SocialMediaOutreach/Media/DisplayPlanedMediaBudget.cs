using TMPro;
using UnityEngine;

public class DisplayPlannedMediaBudget : MonoBehaviour
{
    public TMP_Text totalPromotionPriceText;
    public CalculatePersentInUSD calculatePersentInUSD;
    public MediaPriceCalculation mediaPriceCalculation;
    public GameColors gameColors;

    // Display the planned media budget
    public void DisplayPlannedBudget()
    {
        totalPromotionPriceText.text = $"Out of ${calculatePersentInUSD.priceInUSD}";
        ShowHighlightAnimation();
    }

    // Show highlight animation if the total promotion price exceeds the planned budget
    private void ShowHighlightAnimation()
    {
        if (mediaPriceCalculation.totalPromotionPrice > calculatePersentInUSD.priceInUSD)
        {
            totalPromotionPriceText.rectTransform.localScale = new Vector3(1.05f, 1.05f, 1f);
            totalPromotionPriceText.color = gameColors.errorColor;
            Invoke("HideHighlightAnimation", 0.27f); // Hide animation after 0.27 seconds
        }
        else
        {
            totalPromotionPriceText.color = gameColors.defaultTextColor; // Reset color if no error
        }
    }

    // Reset the highlight animation
    private void HideHighlightAnimation()
    {
        totalPromotionPriceText.rectTransform.localScale = Vector3.one; // Reset to original scale
    }
}
