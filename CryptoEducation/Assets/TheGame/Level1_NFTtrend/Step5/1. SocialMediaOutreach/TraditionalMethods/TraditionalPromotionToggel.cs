using UnityEngine;
using TMPro;

public class TraditionalPromotionToggle : MonoBehaviour
{
    public CustomToggle toggle; // Fixed typo: 'Castom' should be 'Custom'
    private SetTraditional setTraditional;
    private TraditionalPriceCalculation traditionalPriceCalculation;

    public TMP_Text totalPromotionPriceText; // Renamed for consistency

    private void Start()
    {
        // Cache references to avoid calling FindObjectOfType repeatedly
        setTraditional = FindObjectOfType<SetTraditional>();
        traditionalPriceCalculation = FindObjectOfType<TraditionalPriceCalculation>();
    }

    // Fixed typo in method name: 'Ptomotion' to 'Promotion'
    public void SelectPromotionToggle()
    {
        if (toggle.isOn)
        {
            setTraditional.isPromotionOn = true;
            setTraditional.MediaPricesActivator(true); // Activate media prices
            totalPromotionPriceText.gameObject.SetActive(true); // Show the total promotion price
            traditionalPriceCalculation.CalculateAllTraditionalPrice(); // Calculate the price based on selected media
        }
        else
        {
            setTraditional.isPromotionOn = false;
            setTraditional.MediaPricesActivator(false); // Deactivate media prices
            totalPromotionPriceText.gameObject.SetActive(false); // Hide the total promotion price
            traditionalPriceCalculation.DropTraditionalPrices(); // Reset the prices
        }
    }
}
