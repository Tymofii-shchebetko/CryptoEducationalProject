using System.Collections;
using UnityEngine;
using TMPro;

public class SecondarySale : MonoBehaviour
{
    public float duration = 10f; // Duration of the secondary sales increase in seconds
    public TextMeshProUGUI secondarySaleAmountTex;

    [HideInInspector]
    public float currentSecondaryIncome = 0;
    private int currentSecondarySale = 0;

    NFTData nftData;
    BudgetText budgetText;
    PlayersData playersData;

    void Start()
    {
        // Finding required objects
        nftData = FindObjectOfType<NFTData>();
        budgetText = FindObjectOfType<BudgetText>();
        playersData = FindObjectOfType<PlayersData>();

        secondarySaleAmountTex.gameObject.SetActive(false); // Hide the text initially
    }

    public void StartSaleCorutine()
    {
        secondarySaleAmountTex.gameObject.SetActive(true); // Show the secondary sale amount UI
        StartCoroutine(DisplaySecondarySales());
    }

    // Smoothly animate the increase in secondary sales
    IEnumerator DisplaySecondarySales()
    {
        float timer = 0f;

        // Loop to animate the increase in secondary sales over the given duration
        while (timer < duration)
        {
            // Smooth transition of sales amount using Lerp
            currentSecondarySale = Mathf.RoundToInt(Mathf.Lerp(0f, nftData.secondarySales, timer / duration));
            secondarySaleAmountTex.text = "+" + currentSecondarySale;

            // Update secondary income by calculating fee from mint price
            currentSecondaryIncome += currentSecondarySale * (nftData.secondaryFee / 100 * nftData.mintPrice); // Fixed income accumulation

            // Update the UI with the current secondary sale income
            budgetText.UpdateWithSecondarySale(currentSecondaryIncome);

            // Increment timer by time passed each frame
            timer += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure final income and sales amount is correctly set
        Debug.Log(currentSecondaryIncome);

        // Final update to the player's budget after all secondary sales are completed
        playersData.RecalculateBudget(currentSecondaryIncome);
        budgetText.ONsecondaryMoney(); // Update budget display for secondary income
    }
}
