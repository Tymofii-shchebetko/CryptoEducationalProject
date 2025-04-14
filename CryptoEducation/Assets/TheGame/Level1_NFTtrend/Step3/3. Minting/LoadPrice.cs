using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadPrice : MonoBehaviour
{
    public NFTData nftData;
    public GameColors gameColors;
    public ConvertCurrency convertPublicCurrency;
    public MintPrice mintPrice;

    public TextMeshProUGUI amountPublic;
    public TextMeshProUGUI blockchainSymbol;
    public TMP_InputField inpunWL;
    public Image publicImage;
    public Image wlImage;
    public JoisticAnimation publicAnimation, wlAnimation;

    private const float PUBLIC_ANIMATION_DELAY = 0.5f;
    private const float WL_ANIMATION_DELAY = 1.1f;

    void Start()
    {
        UpdatePriceandSymbol();
        Invoke("CallPublicAnimation", PUBLIC_ANIMATION_DELAY);
        Invoke("CallWLAnimation", WL_ANIMATION_DELAY);
    }

    // Updates the price and symbol for public and WL minting
    public void UpdatePriceandSymbol()
    {
        UpdatePublicPrice();
        UpdateWLPrice();
        blockchainSymbol.text = nftData.blockchainSymbol;
    }

    // Updates the price and state of the public mint
    private void UpdatePublicPrice()
    {
        if (nftData.mintPrice >= 0)
        {
            amountPublic.text = nftData.mintPrice + " " + nftData.blockchainSymbol;
            convertPublicCurrency.ConvertPrice();
            publicImage.color = gameColors.publicNFT; // Mark Public On
        }
        else
        {
            publicImage.color = gameColors.defaultWhite; // Mark Public Off
        }
    }

    // Updates the price and state of the WL mint
    private void UpdateWLPrice()
    {
        if (nftData.wlMintPrice > 0)
        {
            inpunWL.text = nftData.wlMintPrice.ToString();
            mintPrice.ConvertPrice();
            MarkWLOn();
        }
        else
        {
            MarkWLOff();
        }
    }

    // Marks the WL mint as active
    public void MarkWLOn()
    {
        wlImage.color = gameColors.wlNFT;
    }

    // Marks the WL mint as inactive
    public void MarkWLOff()
    {
        wlImage.color = gameColors.defaultWhite;
    }

    // Triggers the public mint animation
    public void CallPublicAnimation()
    {
        publicAnimation.StartShaking();
    }

    // Triggers the WL mint animation
    public void CallWLAnimation()
    {
        wlAnimation.StartShaking();
    }
}
