using System.Collections;
using UnityEngine;
using TMPro;

public class DisplayMintAmount : MonoBehaviour
{
    public int targetMint; 
    public float duration = 20f; 
    public TextMeshProUGUI mintAmountTex;

    private MintAmountCalculation mintAmount;
    private BudgetText budgetText;
    private NFTData nftData;
    private PlayersData playersData;

    [HideInInspector]
    public float currentIncome = 0;
    private int currentMint = 0;

    void OnEnable()
    {
        // Delay Start to allow dependent objects to initialize first
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // Wait one frame to allow all other objects to initialize
        yield return null;

        // Now, find all necessary objects
        FindObjects();

        // Initialize the display with the current mint value (starts at 0)
        mintAmountTex.text = currentMint.ToString();

        // Start the coroutine to smoothly increase the minting amount
        StartCoroutine(IncreaseNumberCoroutine());
    }

    IEnumerator IncreaseNumberCoroutine()
    {
        // Use the value from MintAmountCalculation to set the target mint amount
        targetMint = mintAmount.mintAmount; 

        // Timer to track the duration of the animation
        float timer = 0f;

        // Animate the mint amount increasing over the duration
        while (timer < duration)
        {
            currentMint = Mathf.RoundToInt(Mathf.Lerp(0f, targetMint, timer / duration));
            mintAmountTex.text = currentMint.ToString() + " / " + nftData.planedMintAmount;

            // Calculate the current income and update the budget
            currentIncome += currentMint * nftData.mintPrice; // Corrected operator for accumulating income
            budgetText.UpdateMintingBadget(currentIncome); 

            // Increment the timer
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final mint value is set correctly
        currentMint = targetMint;
        mintAmountTex.text = currentMint.ToString() + " / " + nftData.planedMintAmount;

        // Update the player's budget and display the final amount
        playersData.RecalculateBudget(currentIncome);
        budgetText.ONnewMoney(); // Update the UI to reflect the new money

    }

    // Finds necessary objects in the scene
    void FindObjects()
    {
        mintAmount = FindObjectOfType<MintAmountCalculation>(); 
        budgetText = FindObjectOfType<BudgetText>(); 
        nftData = FindObjectOfType<NFTData>(); 
        playersData = FindObjectOfType<PlayersData>(); 
    }
}
