using UnityEngine;
using TMPro;

public class MintPrice : MonoBehaviour
{
    public TMP_InputField mintPrice;
    public TextMeshProUGUI convertedPrice;

    public TheToggle toggle;
    public NFTData nftData;
    public InputAndHeaderError priceError;
    public LoadPrice loadPrice;
    public InputAndHeaderError inputAndHeaderError;
    public ConvertCurrency convertCurrency;

    public void SetMintPrice()
    {
        if (TryParseMintPrice(out float result))
        {
            nftData.wlMintPrice = result;
            HandleToggleAndPrice(result);
        }
        else
        {
            HandleInvalidPrice();
        }
    }

    private bool TryParseMintPrice(out float result)
    {
        return float.TryParse(mintPrice.text, out result);
    }

    private void HandleToggleAndPrice(float result)
    {
        if (result >= 0f)
        {
            toggle.isOn = result > 0f;  // If the result is 0, toggle is off, otherwise it's on
            toggle.TogglrClicked();
            loadPrice.MarkWLOn();
            priceError.SowDefault(); // Reset any prior error states if input is valid
        }
        else
        {
            HandleInvalidPrice();
        }
    }

    private void HandleInvalidPrice()
    {
        nftData.wlMintPrice = -1;
        toggle.isOn = true;
        toggle.TogglrClicked();
        priceError.SowError();
        loadPrice.MarkWLOff();
    }

    ////////////////////////////////
    // Enable free mint
    public void FreeMint()
    {
        if (toggle.isOn)
        {
            mintPrice.text = "0";
            nftData.wlMintPrice = 0;
            inputAndHeaderError.SowDefault();
            ConvertPrice();
            loadPrice.MarkWLOn();
        }
        else
        {
            nftData.wlMintPrice = -1;
            mintPrice.text = null;
            inputAndHeaderError.SowError();
            loadPrice.MarkWLOff();
        }
    }

    public void ConvertPrice()
    {
        if (nftData.blockchainCurrentPrice <= 0)
        {
            convertedPrice.text = "$0.00";
            return;
        }

        float usdPrice = nftData.wlMintPrice * nftData.blockchainCurrentPrice;
        convertedPrice.text = "$" + usdPrice.ToString("F2");
    }
}
