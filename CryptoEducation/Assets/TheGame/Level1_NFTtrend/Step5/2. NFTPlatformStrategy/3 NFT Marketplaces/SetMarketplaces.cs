using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SetMarketplaces : MonoBehaviour
{
    public GameObject nftPlatformPrefab;
    public Transform marketplacesContainer;
    public PlatformPriceCalculation platformPriceCalculation;
    public NFTPlatformsGenerator nftPlatformsGenerator;
    public GameColors gameColors;
    List<Button> selectedMarketplaces = new List<Button>(); // List of selected marketplaces

    public void GenerateMarketplace() // Generate marketplaces based on the blockchain
    {
        // Clean up existing marketplaces before generating new ones
        foreach (Transform child in marketplacesContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate new marketplace buttons
        for (int i = 0; i < nftPlatformsGenerator.marketplaces.Count; i++)
        {
            GameObject newMarketplace = Instantiate(nftPlatformPrefab, marketplacesContainer);
            Button button = newMarketplace.GetComponent<Button>();

            // Ensure listeners are not added multiple times
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectMarketplace(button));

            newMarketplace.name = nftPlatformsGenerator.marketplaces[i];

            // Set the marketplace name and percentage
            newMarketplace.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nftPlatformsGenerator.marketplaces[i];
            newMarketplace.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = nftPlatformsGenerator.marketplacesPercent[i] + "%";

            // Set platform data
            newMarketplace.GetComponent<NFTplatformData>().SetPlatformData("Marketplace", newMarketplace.name, nftPlatformsGenerator.marketplacesPercent[i]);
        }
    }

    // Handle marketplace selection
    public void SelectMarketplace(Button button)
    {
        if (selectedMarketplaces.Contains(button))
        {
            selectedMarketplaces.Remove(button);
            button.image.color = gameColors.defaultWhite; // Reset color if deselected
        }
        else
        {
            selectedMarketplaces.Add(button);
            button.image.color = gameColors.platform; // Highlight the selected marketplace
        }

        RecalculateTotalPercent(); // Recalculate the total percentage after selection
    }

    // Recalculate the total cost for all selected marketplaces
    private void RecalculateTotalPercent()
    {
        float totalSelectedMarketplacesCost = 0;
        foreach (Button marketplaceButton in selectedMarketplaces)
        {
            totalSelectedMarketplacesCost += marketplaceButton.GetComponent<NFTplatformData>().platformCost;
        }
        platformPriceCalculation.GetMarketplacePercent(totalSelectedMarketplacesCost); // Recalculate the budget
    }

    // Call when selecting a marketplace from Launchpad (to set the commission to 0%)
    public void CallSelectMarketplace(string platformName)
    {
        // Recalculate marketplace percentages based on user selection
        nftPlatformsGenerator.marketplacesRecalculatedPercent = new List<float>(nftPlatformsGenerator.marketplacesPercent);

        for (int i = 0; i < marketplacesContainer.childCount; i++) // Start from 0 to include the first child
        {
            Transform child = marketplacesContainer.GetChild(i);
            int listIndex = i;

            // If the user selected the marketplace, set its commission to 0%
            if (child.name == platformName)
                nftPlatformsGenerator.marketplacesRecalculatedPercent[listIndex] = 0;

            // Update the percentage text
            TextMeshProUGUI textComponent = child.GetChild(2).GetComponent<TextMeshProUGUI>();
            child.GetComponent<NFTplatformData>().ChangeCost(nftPlatformsGenerator.marketplacesRecalculatedPercent[listIndex]);
            textComponent.text = nftPlatformsGenerator.marketplacesRecalculatedPercent[listIndex] + "%";
        }

        // Recalculate the total percentage after adjustments
        RecalculateTotalPercent();
    }
}
