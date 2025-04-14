using System.Collections;
using UnityEngine;
using TMPro;

public class MintDayTimer : MonoBehaviour
{
    public TextMeshProUGUI secondsText;
    public GameObject displayMintAmount; // Tab that includes the mint process and triggers it
    private int secondsForMint;

    void Start()
    {
        // Initially hide the mint process
        displayMintAmount.SetActive(false);

        // Set a random number of seconds for the countdown (between 3 and 7)
        secondsForMint = Random.Range(3, 7);

        // Update the UI text to show the starting value
        secondsText.text = secondsForMint.ToString();

        // Start the countdown coroutine
        StartCoroutine(CountSeconds());
    }

    // Coroutine that counts down from the secondsForMint
    IEnumerator CountSeconds()
    {
        // Wait for one second
        yield return new WaitForSeconds(1);

        // Decrease the seconds left
        secondsForMint--;

        // Update the UI text
        secondsText.text = secondsForMint.ToString();

        // Check if countdown is finished
        if (secondsForMint <= 0)
        {
            // Display the mint process once the timer reaches zero
            displayMintAmount.SetActive(true);
        }
        else
        {
            // If time is not up, keep the countdown going
            StartCoroutine(CountSeconds());
        }
    }
}
