using UnityEngine;

public class WLPriceWarning : MonoBehaviour
{
    public SetWLCondition wlPriceNotification;
    public NFTData nftData;

    public void CheckWLPriceAndNotify()
    {
        // Validate the mint prices and ensure nftAmountForPublic is not zero
        if (nftData.wlMintPrice < 0 || nftData.mintPrice < 0 || nftData.nftAmountForPublic == 0)
        {
            // Notify about invalid or missing data
            wlPriceNotification.ShowRequirmentDescription("invalid_price_data");
            return;
        }

        // Compare whitelist price with public price and notify accordingly
        if (nftData.wlMintPrice >= nftData.mintPrice)
        {
            wlPriceNotification.ShowRequirmentDescription("public_wl_price_notification");
        }
        else
        {
            wlPriceNotification.ShowRequirmentDescription("stop_coroutine");
        }
    }
}
