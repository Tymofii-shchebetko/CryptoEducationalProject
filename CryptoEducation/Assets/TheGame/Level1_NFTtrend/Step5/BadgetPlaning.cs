using UnityEngine;
using TMPro;

public class BadgetPlaning : MonoBehaviour
{
    PlayersData playersData;
    NFTData nftData;
    GetFreeFonds getFreeFonds;

    [Header("Tech Budget")]
    public float domainPrice = 0; // USD
    public float hostingPrice = 0; // USD
    public float websitePrice = 0; // USD
    public float deployPrice = 0; // in crypto
    public float ipsfPrice = 0; // in crypto

    public TMP_Text deployFee;
    public TMP_Text mintingWebSiteFee;
    public TMP_Text IPFSFee;

    [Header("Marketing Budget")]
    public float mediaPrices = 0; // USD
    public float platformsPrices = 0; // USD
    public float traditionalPrices = 0; // USD
    public float ambassadorsPrices = 0; // USD

    [Header("Total Budget")]
    public float totalPriceInUSD = 0;
    public float totalPriceInCrypto = 0;

    void Start()
    {
        playersData = FindObjectOfType<PlayersData>();
        nftData = FindObjectOfType<NFTData>();
        getFreeFonds = FindObjectOfType<GetFreeFonds>();

        CalculateWebSite();
        CalculateDeploy();
        DisplayIPFS();
    }

    #region Tech

    public void CalculateDomain()
    {
        UpdateSum();
    }

    public void CalculateHosting()
    {
        UpdateSum();
    }

    public void CalculateWebSite()
    {
        websitePrice = 10 + ((100f - playersData.successRate) / 100f) * 20f;
        mintingWebSiteFee.text = $"Price for minting website: {websitePrice:F2} USD";
        UpdateSum();
    }

    public void CalculateDeploy()
    {
        float minCommission = nftData.planedMintAmount * (100 - playersData.successRate / 10) / 100f;
        float maxCommission = nftData.planedMintAmount;
        deployPrice = Random.Range(minCommission, maxCommission);

        switch (nftData.collectionBlockchain)
        {
            case "Solana":
            case "Polygon":
                deployPrice *= 0.01f;
                break;
            case "Ethereum":
            case "BNB":
                deployPrice *= 0.02f;
                break;
            case "Bitcoin":
                deployPrice *= 0.00115385f;
                break;
            case "Aptos":
                deployPrice *= 0.15f;
                break;
        }

        deployFee.text = $"{deployPrice:F4} {nftData.blockchainSymbol}";
        UpdateSum();
    }

    public void CalculateIPFS(bool operation)
    {
        ipsfPrice = operation ? deployPrice * 0.009f : 0f;
        UpdateSum();
    }

    public void DisplayIPFS()
    {
        IPFSFee.text = $"{deployPrice * 0.01f:F4} {nftData.blockchainSymbol}";
    }

    public void CalculateRCP()
    {
        // Placeholder if RCP pricing is introduced
    }

    #endregion

    #region Marketing

    public void CalculateMediaPromotion()
    {
        UpdateSum();
    }

    public void CalculatePlatformsPromotion()
    {
        UpdateSum();
    }

    public void CalculateTraditionalPromotion()
    {
        UpdateSum();
    }

    public void CalculateAmbassadorsPromotion()
    {
        UpdateSum();
    }

    #endregion

    public void UpdateSum()
    {
        float cryptoRate = nftData.blockchainCurrentPrice;

        totalPriceInUSD = domainPrice + hostingPrice + websitePrice +
                          (deployPrice + ipsfPrice) * cryptoRate +
                          mediaPrices + platformsPrices + traditionalPrices + ambassadorsPrices;

        totalPriceInCrypto = deployPrice + ipsfPrice +
                             domainPrice / cryptoRate +
                             hostingPrice / cryptoRate +
                             websitePrice / cryptoRate +
                             mediaPrices / cryptoRate +
                             platformsPrices / cryptoRate +
                             traditionalPrices / cryptoRate +
                             ambassadorsPrices / cryptoRate;

        getFreeFonds.GetUsedFonds(totalPriceInUSD);
        getFreeFonds.DisplayFreeBudget();
    }
}
