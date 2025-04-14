using UnityEngine;
using TMPro;

public class ConvertCurrency : MonoBehaviour
{
    public TextMeshProUGUI inUSDprice;
    public NFTData nftData;

    public void ConvertPrice()
    {
        if (nftData != null && inUSDprice != null)
        {
            // Validate that prices are greater than 0 to avoid errors in calculation
            if (nftData.mintPrice > 0 && nftData.blockchainCurrentPrice > 0)
            {
                float usdPrice = nftData.mintPrice * nftData.blockchainCurrentPrice;
                inUSDprice.text = "$" + usdPrice.ToString("F2");
            }
            else
            {
                inUSDprice.text = "$0.00"; // Default text for invalid price data
            }
        }
        else
        {
            Debug.LogError("NFTData or TextMeshProUGUI reference is null.");
        }
    }
}
