using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.IO;

public class CryptoCurrenciesRate : MonoBehaviour
{
    [HideInInspector]
    public string newCryptoData;
    _ExternalFiles externalFiles;

    private string apiUrl = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=" + "bitcoin,"+ "ethereum," + "binancecoin," + "solana," + "matic-network," + "aptos," + "arbitrum," + "flow," + "tezos," + "sui" + "&sparkline=false";

    private IEnumerator Start()
    {
        externalFiles = FindObjectOfType<_ExternalFiles>();

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Не вдається підтягнути дані блокчей нів по АПІ");
            // Читаю попередні
            ReadCryptoData();
        }
        else
        {
            newCryptoData = request.downloadHandler.text;
            SaveNewCryptoData();
        }
    }


    public void SaveNewCryptoData()
    {
        File.WriteAllText(externalFiles.cryptoDataPath, newCryptoData);
        // Читаю перезаписаний файл
        ReadCryptoData();
    }
    public void ReadCryptoData()
    {
        newCryptoData = File.ReadAllText(externalFiles.cryptoDataPath);
    }
}

