using System.Collections.Generic;
using UnityEngine;

public class NFTData : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string collectionName;
    [SerializeField] private string collectionTag;
    [SerializeField] private int plannedMintAmount;
    [SerializeField] private string collectionBlockchain;
    [SerializeField] private float blockchainCurrentPrice;
    [SerializeField] private string blockchainSymbol;
    [SerializeField] private Sprite blockchainLogo;

    [Header("Minting")]
    [SerializeField] private float mintPrice = -1f;
    [SerializeField] private float wlMintPrice = -1f;
    [SerializeField] private float percentForPublic;
    [SerializeField] private float nftAmountForPublic;
    [SerializeField] private float nftAmountForWL;
    [SerializeField] private float creatorFee;

    [Header("Description")]
    [SerializeField, TextArea] private string collectionUtility;
    [SerializeField, TextArea] private string collectionDescription;
    [SerializeField] private List<string> collectionMedias = new List<string>();

    [Header("Other Calculation")]
    [SerializeField] private int secondarySales;
    [SerializeField] private float secondaryFee;

    // Public properties to get data
    public string CollectionName => collectionName;
    public string CollectionTag => collectionTag;
    public int PlannedMintAmount => plannedMintAmount;
    public string CollectionBlockchain => collectionBlockchain;
    public float BlockchainCurrentPrice => blockchainCurrentPrice;
    public string BlockchainSymbol => blockchainSymbol;
    public Sprite BlockchainLogo => blockchainLogo;
    public float MintPrice => mintPrice;
    public float WlMintPrice => wlMintPrice;
    public float PercentForPublic => percentForPublic;
    public float NftAmountForPublic => nftAmountForPublic;
    public float NftAmountForWL => nftAmountForWL;
    public float CreatorFee => creatorFee;
    public string CollectionUtility => collectionUtility;
    public string CollectionDescription => collectionDescription;
    public IReadOnlyList<string> CollectionMedias => collectionMedias;
    public int SecondarySales => secondarySales;
    public float SecondaryFee => secondaryFee;

    public void ShowDescription()
    {
        Debug.Log($"Utility: {collectionUtility}\nDescription: {collectionDescription}");
        for (int i = 0; i < collectionMedias.Count; i++)
        {
            Debug.Log($"Media {i + 1}: {collectionMedias[i]}");
        }
    }

    public void ShowMilestone()
    {
        Debug.Log($"Mint Price: {mintPrice}, Creator Fee: {creatorFee}");
    }
}
