using UnityEngine;
using TMPro;

public class FreeMint : MonoBehaviour
{
    public TheToggle toggle;
    public NFTData nftData;
    public InputAndHeaderError inputAndHeaderError;
    public ConvertCurrency convertCurrency;
    public TMP_InputField mintPrice;
    public GameObject milestonePercent_text;
    public AddPreSaleAnimation addPreSaleAnimation;

    public void FreeMintToggle()
    {
        if (toggle == null || mintPrice == null || inputAndHeaderError == null || convertCurrency == null || addPreSaleAnimation == null)
        {
            Debug.LogError("One or more critical references are missing.");
            return;
        }

        if (toggle.isOn)
        {
            mintPrice.text = "0";
            nftData.mintPrice = 0;
            inputAndHeaderError.SowDefault();
            convertCurrency.ConvertPrice();

            if (nftData.planedMintAmount > 0)
            {
                milestonePercent_text.SetActive(true); // Show that 100% goes to public mint
                addPreSaleAnimation.HidePreSaleButton();
            }
        }
        else
        {
            nftData.mintPrice = -1;
            mintPrice.text = string.Empty; // Use empty string instead of null
            inputAndHeaderError.SowError();

            addPreSaleAnimation.HidePreSaleButton();
            milestonePercent_text.SetActive(false);
        }
    }
}
