using UnityEngine;
using TMPro;

public class GetNFTsAmount : MonoBehaviour
{
    // Text fields for displaying amounts
    public TextMeshProUGUI totalNFTamount;
    public TextMeshProUGUI publicAmount_text;
    public TextMeshProUGUI wlAmount_text;

    // Reference to WLChanges script
    public WLChanges wlChanges;

    // Stores the calculated amounts
    private float publicAmount;
    private float wlAmount;

    // Reference to NFTData for access to planned mint amount
    public NFTData nftData;

    private const string NFT_SUFFIX = " NFT"; // To avoid repetition
    private const string TOTAL_MINT_LABEL = "Total mint amount: ";

    public void RecalculateNFTmilestoneAmount(float publicPercent)
    {
        // Ensure publicPercent is within the valid range (0-100)
        publicPercent = Mathf.Clamp(publicPercent, 0f, 100f);

        // Calculate the public and whitelist NFT amounts
        publicAmount = (publicPercent / 100f) * nftData.planedMintAmount;
        wlAmount = nftData.planedMintAmount - publicAmount;

        // Round the amounts to avoid decimals in the UI
        publicAmount = Mathf.Round(publicAmount);
        wlAmount = Mathf.Round(wlAmount);

        // Update the UI texts
        publicAmount_text.text = $"{publicAmount} {NFT_SUFFIX}";
        wlAmount_text.text = $"{wlAmount} {NFT_SUFFIX}";

        // Update NFT data
        nftData.nftAmountForPublic = publicAmount;
        nftData.nftAmountForWL = wlAmount;

        // Apply any necessary changes related to WL
        wlChanges.ApplyChangesWithWL();

        // Update total amount text
        totalNFTamount.text = $"{TOTAL_MINT_LABEL} {nftData.planedMintAmount} {NFT_SUFFIX}";
    }
}
