using System.Collections.Generic;
using UnityEngine;

public class _ExternalFiles : MonoBehaviour
{
    public string cryptoDataPath;

    public void Awake()
    {
        ///// Application.dataPath (Юнити),  Application.persistentDataPath (Андроид),  Application.streamingAssetsPath (мб Иос)
#if UNITY_EDITOR
        cryptoDataPath = Application.dataPath + "/CryptoData.json";

#elif UNITY_ANDROID
        cryptoDataPath = Application.persistentDataPath + "/CryptoData.json";
#elif UNITY_IPHONE
        cryptoDataPath = Application.persistentDataPath + "/CryptoData.json";
#endif
    }
}

[System.Serializable]
public class CryptoData
{
    public string rawCryptoData;
}