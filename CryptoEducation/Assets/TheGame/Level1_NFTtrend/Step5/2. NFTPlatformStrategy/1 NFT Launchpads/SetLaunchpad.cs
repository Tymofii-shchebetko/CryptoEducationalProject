using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SetLaunchpad : MonoBehaviour
{
    private Button selectedButton;

    public GameObject nftPlatformPrefab;
    public Transform launchpadContainer;
    public PlatformPriceCalculation platformPriceCalculation;
    public NFTPlatformsGenerator nftPlatformsGenerator;
    public SetMarketplaces setMarketplaces;
    public GameColors gameColors;

    public void GenerateLaunchpads() // Generate launchpads based on the blockchain
    {
        // Clean up existing launchpads before generating new ones
        foreach (Transform child in launchpadContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new launchpads
        for (int i = 0; i < nftPlatformsGenerator.launchpads.Count; i++)
        {
            GameObject newLaunchpad = Instantiate(nftPlatformPrefab, launchpadContainer);
            Button button = newLaunchpad.GetComponent<Button>();

            // Clear any existing listeners to prevent multiple subscriptions
            button.onClick.RemoveAllListeners();

            // Add listener for the button click
            button.onClick.AddListener(() => SelectLaunchpad(button));

            // Set up the launchpad name and price
            newLaunchpad.name = nftPlatformsGenerator.launchpads[i];
            newLaunchpad.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nftPlatformsGenerator.launchpads[i];
            newLaunchpad.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + nftPlatformsGenerator.launchpadPrice[i];

            // Set platform data
            newLaunchpad.GetComponent<NFTplatformData>().SetPlatformData("Launchpad", newLaunchpad.name, nftPlatformsGenerator.launchpadPrice[i]);
        }
    }

    // Handle launchpad selection
    public void SelectLaunchpad(Button button)
    {
        // Reset previous selection
        if (selectedButton != null)
        {
            selectedButton.image.color = gameColors.defaultWhite; // Reset previous button color
        }

        // Update the selected button's color
        selectedButton = button;
        selectedButton.image.color = gameColors.influensers;

        // Update the total launchpad price in the budget calculation
        platformPriceCalculation.GetLaunchpadPrice((int)button.GetComponent<NFTplatformData>().platformCost);

        // Call marketplace selection based on the selected launchpad
        setMarketplaces.CallSelectMarketplace(button.name);
    }
}
