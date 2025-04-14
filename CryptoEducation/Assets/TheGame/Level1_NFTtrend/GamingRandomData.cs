using UnityEngine;

public class GamingRandomData : MonoBehaviour
{
    [HideInInspector] public int LostMoney { get; private set; }
    [HideInInspector] public int AllowedToMissAmount { get; private set; }
    [HideInInspector] public int MaxLostMoney { get; private set; }
    [HideInInspector] public int MinLostMoney { get; private set; }

    private PlayersData _playersData;
    private BudgetText _budgetText;

    private void Start()
    {
        _playersData = FindObjectOfType<PlayersData>();
        _budgetText = FindObjectOfType<BudgetText>();
        InitializeValues();
    }

    private void InitializeValues()
    {
        AllowedToMissAmount = GenerateRandomMissAmount();
    }

    public void RandomizeAllowedToMiss()
    {
        AllowedToMissAmount = GenerateRandomMissAmount();
    }

    private int GenerateRandomMissAmount()
    {
        return Random.Range(2, 10);
    }

    public void RandomizeLostMoney()
    {
        _playersData.SetStartingBudget();
        MaxLostMoney = (int)_playersData.startingBudget - _playersData.successRate;
        MinLostMoney = MaxLostMoney / 2;
        LostMoney = Mathf.Clamp(Random.Range(MinLostMoney, MaxLostMoney), 0, (int)_playersData.budget);
        UpdateBudgetText();
    }

    private void UpdateBudgetText()
    {
        _budgetText.nweMoney_Text.text = "-" + LostMoney;
    }
}