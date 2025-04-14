using UnityEngine;
using TMPro;

public class WLChanges : MonoBehaviour 
{
    public TextMeshProUGUI preSale_text;
    public TextMeshProUGUI milestonePercent_text;
    public GameObject mintingTab;
    public NFTData nftData;
    public SetWLCondition setWLCondition; // Щоб вимкнути корутину з відображенням пояснень

    public void ApplyChangesWithWL()
    {
        if (nftData.wlMintPrice >= 0 && nftData.nftAmountForWL > 0)
        {
            preSale_text.text = "Change Pre-Sale?";
            milestonePercent_text.text = $"{nftData.nftAmountForPublic} NFT for public sale\n{nftData.nftAmountForWL} NFT for WL sale";
        }
        else
        {
            preSale_text.text = "Add Pre-Sale?";
            milestonePercent_text.text = "100% Public sale";
        }

        if (mintingTab != null && mintingTab.activeSelf) // Prevent errors when calling coroutine
        {
            setWLCondition.ShowRequirmentDescription("stop_coroutine");
        }
    }
}
