using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Linq;

public class ReadCryptoData : MonoBehaviour
{
    public NFTData nftData;
    public CryptoCurrenciesRate cryptoCurrenciesRate;
    [SerializeField] private EnterPriceManually enterPriceManually;
    public SetBlockchain setBlockchain;

    private readonly Dictionary<string, (string Name, Sprite Logo)> blockchainInfo = new()
    {
        { "sol", ("Solana", null) },
        { "eth", ("Ethereum", null) },
        { "matic", ("Polygon", null) },
        { "btc", ("Bitcoin", null) },
        { "bnb", ("BNB", null) },
        { "apt", ("Aptos", null) }
    };

    public void GetCryptoDataFromFile(string selectedSymbol)
    {
        cryptoCurrenciesRate.ReadCryptoData();

        try
        {
            JArray data = JArray.Parse(cryptoCurrenciesRate.newCryptoData);
            var matchingCoin = data.FirstOrDefault(coin => coin["symbol"]?.ToString() == selectedSymbol);

            if (matchingCoin != null)
            {
                nftData.collectionBlockchain = matchingCoin["name"]?.ToString();
                nftData.blockchainCurrentPrice = matchingCoin["current_price"]?.Value<float>() ?? 0f;
                nftData.blockchainSymbol = selectedSymbol;

                Debug.Log($"Name: {nftData.collectionBlockchain}, " +
                          $"Current Price: {nftData.blockchainCurrentPrice}, " +
                          $"Symbol: {nftData.blockchainSymbol}");
            }
            else
            {
                SetDefaultBlockchainData(selectedSymbol);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error parsing crypto data: {ex.Message}");
            SetDefaultBlockchainData(selectedSymbol);
        }
    }

    private void SetDefaultBlockchainData(string symbol)
    {
        if (blockchainInfo.TryGetValue(symbol, out var blockchain))
        {
            nftData.collectionBlockchain = blockchain.Name;
            nftData.blockchainSymbol = symbol;
            nftData.blockchainLogo = GetBlockchainLogo(symbol);
        }

        // Show manual price entry UI
        enterPriceManually.ShowPriceSetting();
    }

    private Sprite GetBlockchainLogo(string symbol)
    {
        int index = blockchainInfo.Keys.ToList().IndexOf(symbol);
        return (index >= 0 && index < setBlockchain.sourceImages.Length) ? setBlockchain.sourceImages[index] : null;
    }
}
