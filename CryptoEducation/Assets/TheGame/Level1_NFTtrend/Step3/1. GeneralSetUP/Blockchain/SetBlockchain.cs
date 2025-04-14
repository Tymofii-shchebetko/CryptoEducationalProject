using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetBlockchain : MonoBehaviour
{
    public List<Sprite> sourceImages = new List<Sprite>();
    public List<string> sourceTexts = new List<string>();

    public List<Image> blockchainLogos = new List<Image>();
    public List<TextMeshProUGUI> blockchainNames = new List<TextMeshProUGUI>();
    public List<Button> blockchainButtons = new List<Button>();

    public BlockchainDropDown blockchainDropDown;
    public NFTData nftData;
    public ReadCryptoData readCryptoData;
    public TextMeshProUGUI priceSymbol;
    public ConvertCurrency convertCurrency;

    [SerializeField] private Image blockchainLogo;
    [SerializeField] private TextMeshProUGUI blockchainSymbol;

    private int blockchainIndex = 0;

    private void Start()
    {
        if (sourceTexts.Count > 0)
            Invoke(nameof(CallCryptoDataReader), 0.03f);
    }

    public void GetButtonName(int theIndex)
    {
        if (theIndex < 0 || theIndex >= blockchainButtons.Count)
            return; // Prevent out-of-range errors

        // Show the previous blockchain button
        if (blockchainIndex >= 0 && blockchainIndex < blockchainButtons.Count)
            blockchainButtons[blockchainIndex].gameObject.SetActive(true);

        // Update index and hide selected button
        blockchainIndex = theIndex;
        blockchainButtons[blockchainIndex].gameObject.SetActive(false);

        ChangeBlockchainAttributes();
    }

    public void CallCryptoDataReader()
    {
        if (sourceTexts.Count <= blockchainIndex || sourceImages.Count <= blockchainIndex)
            return; // Prevent errors when accessing lists

        readCryptoData.GetCryptoDataFromFile(sourceTexts[blockchainIndex]);
        nftData.blockchainLogo = sourceImages[blockchainIndex];
        priceSymbol.text = sourceTexts[blockchainIndex];

        // Optional: Add an animation for explanation popup about blockchain choices
    }

    public void ChangeBlockchainAttributes()
    {
        if (sourceImages.Count > blockchainIndex && sourceTexts.Count > blockchainIndex)
        {
            blockchainLogo.sprite = sourceImages[blockchainIndex];
            blockchainSymbol.text = sourceTexts[blockchainIndex];

            CallCryptoDataReader();
            blockchainDropDown.CloseContainer();
            convertCurrency.ConvertPrice(); // Convert price after changing blockchain
        }
    }
}
