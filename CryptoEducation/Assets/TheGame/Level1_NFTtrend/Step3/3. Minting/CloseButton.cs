using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button closeButton;
    public GameObject no;
    public GameObject yes;
    public NFTData nftData;
    public GameColors gameColors;

    // Update the button state based on NFT data
    public void UpdateCloseButton()
    {
        // Set the color and active state based on the conditions
        if (nftData.wlMintPrice >= 0 && nftData.nftAmountForWL > 0)
        {
            SetButtonState(yes, no, gameColors.wlNFT); // Active "Yes", set pressed color to WL color
        }
        else
        {
            SetButtonState(no, yes, gameColors.publicNFT); // Active "No", set pressed color to Public color
        }
    }

    // Helper method to update the button state
    private void SetButtonState(GameObject showActive, GameObject hideInactive, Color pressedColor)
    {
        showActive.SetActive(true);  // Show the active button (yes/no)
        hideInactive.SetActive(false);  // Hide the inactive button
        UpdateButtonColor(pressedColor);  // Update button color
    }

    // Helper method to update the close button's pressed color
    private void UpdateButtonColor(Color pressedColor)
    {
        ColorBlock colors = closeButton.colors;
        colors.pressedColor = pressedColor; // Change the pressed color
        closeButton.colors = colors; // Apply the new color block
    }
}
