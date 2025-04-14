using System.Collections;
using UnityEngine;
using TMPro;

public class BudgetText : MonoBehaviour
{
    private PlayersData playersData;

    [SerializeField] private TextMeshProUGUI budgetText;
    [SerializeField] private TextMeshProUGUI newMoneyText;
    [SerializeField] private TextMeshProUGUI secondaryMoneyText;

    private void Start()
    {
        playersData = FindObjectOfType<PlayersData>(); // Get PlayersData instance
        playersData.SetBudget();

        // Hide income text elements initially
        newMoneyText.gameObject.SetActive(false);
        secondaryMoneyText.gameObject.SetActive(false);

        UpdateBudgetDisplay();
    }

    // Updates the main budget display
    public void UpdateBudgetDisplay()
    {
        budgetText.text = $"${playersData.budget}";
    }

    // Shows and updates income text (for both primary and secondary income)
    private void ShowIncomeText(TextMeshProUGUI incomeText, float amount)
    {
        incomeText.gameObject.SetActive(true);
        incomeText.text = $"+{amount:F2}$";
        StartCoroutine(HideIncomeTextAfterDelay(incomeText));
    }

    // Handles new money income
    public void UpdateMintingBudget(float currentIncome)
    {
        ShowIncomeText(newMoneyText, currentIncome);
    }

    // Handles secondary sale income
    public void UpdateWithSecondarySale(float secondaryIncome)
    {
        ShowIncomeText(secondaryMoneyText, secondaryIncome);
    }

    // Coroutine to hide income text after delay
    private IEnumerator HideIncomeTextAfterDelay(TextMeshProUGUI incomeText)
    {
        yield return new WaitForSeconds(1);
        incomeText.gameObject.SetActive(false);
        UpdateBudgetDisplay();
    }
}
