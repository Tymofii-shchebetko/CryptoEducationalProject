using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SetTraditional : MonoBehaviour
{
    [SerializeField]
    private Button[] traditionalButtons;
    [SerializeField]
    private GameObject[] traditionalPrices;

    private List<Button> selectedTraditionalButtons = new List<Button>();
    public List<GameObject> PricesForSelectedTraditional = new List<GameObject>();

    public bool isPromotionOn;

    private MediaPriceCalculation mediaPriceCalculation;

    void Start()
    {
        mediaPriceCalculation = FindObjectOfType<MediaPriceCalculation>();

        foreach (Button button in traditionalButtons)
        {
            button.onClick.AddListener(() => SelectTraditional(button));
        }
    }

    public void SelectTraditional(Button button)
    {
        int buttonIndex = System.Array.IndexOf(traditionalButtons, button); // Get the index of the button in the array
        GameObject priceObject = traditionalPrices[buttonIndex]; // Get the corresponding price GameObject

        if (selectedTraditionalButtons.Contains(button))
        {
            // Deselect button and remove it from the list
            selectedTraditionalButtons.Remove(button);
            button.image.color = Color.white;

            // Reset price and deactivate the corresponding price object
            priceObject.GetComponent<BoostPriceChanger>().boostPrice = 0;
            priceObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "0$";
            priceObject.SetActive(false);

            PricesForSelectedTraditional.Remove(priceObject); // Remove price object from the list
            mediaPriceCalculation.CalculateAllMediaPrice(); // Recalculate the total media price
        }
        else
        {
            // Select button and add it to the list
            selectedTraditionalButtons.Add(button);
            button.image.color = Color.green;

            PricesForSelectedTraditional.Add(priceObject); // Add the price object to the list
            if (isPromotionOn)
            {
                MediaPricesActivator(true);
            }
        }
    }

    public void MediaPricesActivator(bool setActive)
    {
        // Activate or deactivate all selected prices based on the passed parameter
        foreach (GameObject price in PricesForSelectedTraditional)
        {
            price.SetActive(setActive);
        }
    }
}
