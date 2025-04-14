using UnityEngine;
using TMPro;

public class BudgetPlaningTextUpdate : MonoBehaviour
{
    public TextMeshProUGUI totalPriceInUSD_Text;
    public TextMeshProUGUI totalPriceInCrypto_Text;
    NFTData nftData;

    public void UpdatePlaningBadget(float totalInUSD, float totalInCrypto)
    {
        nftData = FindObjectOfType<NFTData>();

        totalPriceInUSD_Text.text = "$" + totalInUSD.ToString("F2");
        totalPriceInCrypto_Text.text = totalInCrypto + " " + nftData.blockchainSymbol;
    }
}
