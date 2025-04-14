using System.Collections.Generic;
using UnityEngine;

public enum BlockchainType
{
    Solana,
    Ethereum,
    BinanceSmartChain
}

public class NFTPlatformsGenerator : MonoBehaviour
{
    public BlockchainType blockchain;

    public SuccessRate4NFT successRateScript;
    public CalculatePrice4PlatformsMarketing generatedPrice;
    public SetLaunchpad setLaunchpad;
    public SetCalendar setCalendar;
    public SetMarketplaces setMarketplaces;

    [HideInInspector] public List<string> launchpads = new List<string>();
    [HideInInspector] public List<int> launchpadPrice = new List<int>();

    [HideInInspector] public List<string> calendars = new List<string>();
    [HideInInspector] public List<int> calendarsPrice = new List<int>();

    [HideInInspector] public List<string> marketplaces = new List<string>();
    [HideInInspector] public List<float> marketplacesPercent = new List<float>();

    public List<float> marketplacesRecalculatedPercent = new List<float>();

    private void Start()
    {
        GeneratePlatformData(blockchain);
    }

    // Generate data for a specific blockchain
    private void GeneratePlatformData(BlockchainType selectedBlockchain)
    {
        float successRate = successRateScript.GetSuccessRate();
        switch (selectedBlockchain)
        {
            case BlockchainType.Solana:
                SetSolanaPlatformData(successRate);
                break;

            case BlockchainType.Ethereum:
                Debug.Log("Not ready for Ethereum yet");
                break;

            case BlockchainType.BinanceSmartChain:
                Debug.Log("Not ready for Binance Smart Chain yet");
                break;

            default:
                Debug.Log("Not ready for other blockchains yet");
                break;
        }

        // Call the necessary methods to update the UI/Generate platform data
        setLaunchpad.GenerateLaunchpads();
        setCalendar.GenerateCalendar();
        setMarketplaces.GenerateMarketplace();
    }

    // Set platform data for Solana
    private void SetSolanaPlatformData(float successRate)
    {
        launchpads = new List<string> { "Own Mint Page", "Magic Eden", "Solanart", "Exchange.Art" };
        launchpadPrice = new List<int>
        {
            0,
            generatedPrice.GetRandomValueBasedOnSuccessRate(5000, 50000, successRate),
            generatedPrice.GetRandomValueBasedOnSuccessRate(2000, 20000, successRate),
            generatedPrice.GetRandomValueBasedOnSuccessRate(2000, 20000, successRate)
        };

        calendars = new List<string> { "NFTCalendar.io", "NFT Drops Calendar", "NFT SOLANA Calendar", "Moonly", "NFT Evening" };
        calendarsPrice = new List<int>
        {
            generatedPrice.GetRandomValueBasedOnSuccessRate(150, 500, successRate),
            generatedPrice.GetRandomValueBasedOnSuccessRate(120, 700, successRate),
            generatedPrice.GetRandomValueBasedOnSuccessRate(150, 1100, successRate),
            generatedPrice.GetRandomValueBasedOnSuccessRate(100, 900, successRate),
            generatedPrice.GetRandomValueBasedOnSuccessRate(500, 1500, successRate)
        };

        marketplaces = new List<string> { "Magic Eden", "Solanart", "SolSea", "Exchange.Art", "Tensor" };
        marketplacesPercent = new List<float>
        {
            generatedPrice.GetRandomFloatBasedOnSuccessRate(0.2f, 5f, successRate),
            generatedPrice.GetRandomFloatBasedOnSuccessRate(0.2f, 5f, successRate),
            generatedPrice.GetRandomFloatBasedOnSuccessRate(0.2f, 5f, successRate),
            generatedPrice.GetRandomFloatBasedOnSuccessRate(0.2f, 5f, successRate),
            generatedPrice.GetRandomFloatBasedOnSuccessRate(0.2f, 5f, successRate)
        };
    }
}
