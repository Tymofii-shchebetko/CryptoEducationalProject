using UnityEngine;
using System.Collections;

public class MintAmountCalculation : MonoBehaviour
{
    // Дані, які потрібно тягнути з PlayersData.cs (Тимчасово тут)
    public float followers;

    private NFTData nftData;
    private PlayersData playersData;
    private SecondarySale secondarySale;

    private int realMintAmount;
    public int mintAmount;

    // Start is called before the first frame update
    void Start()
    {
        nftData = FindObjectOfType<NFTData>(); // екземпляр класа
        playersData = FindObjectOfType<PlayersData>(); // екземпляр класа
        if (nftData != null && playersData != null)
        {
            CalculateMintAmount();
        }
        else
        {
            Debug.LogError("NFTData or PlayersData not found!");
        }
    }

    // Calculates the mint amount based on success rate and followers.
    public void CalculateMintAmount()
    {
        // Ensure that successRate and followers are valid.
        if (playersData.successRate <= 0 || followers <= 0)
        {
            Debug.LogError("Invalid success rate or followers count!");
            return;
        }

        // Calculate the real mint amount using success rate, followers, and mint price.
        float localSuccessRate = playersData.successRate / 100f;  // Ensure proper division by using float
        realMintAmount = (int)(((localSuccessRate) * followers) / (nftData.mintPrice + 1));

        // Apply randomness to the mint amount (±10%).
        mintAmount = Random.Range((int)(realMintAmount * 0.9f), (int)(realMintAmount * 1.1f));
        Debug.Log($"Calculated Mint Amount: {mintAmount}");

        // If the projected sale is more than planned, calculate secondary sales
        if (nftData.planedMintAmount < mintAmount)
        {
            nftData.secondarySales = mintAmount - nftData.planedMintAmount;
            Debug.Log($"Secondary sales calculated: {nftData.secondarySales}");

            // Limit mint amount to planned amount
            mintAmount = nftData.planedMintAmount;

            // Start secondary sales after a delay
            StartCoroutine(StartSecondarySales());
        }
    }

    // Starts secondary sales after a delay.
    IEnumerator StartSecondarySales()
    {
        // Wait for 12 seconds before starting the secondary sale
        yield return new WaitForSeconds(12);

        // Initialize secondary sale logic if it's not yet initialized
        secondarySale = FindObjectOfType<SecondarySale>();
        if (secondarySale != null)
        {
            secondarySale.StartSaleCoroutine();
        }
        else
        {
            Debug.LogError("SecondarySale not found!");
        }
    }
}
