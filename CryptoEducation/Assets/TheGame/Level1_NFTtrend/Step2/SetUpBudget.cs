using UnityEngine;
using UnityEngine.UI;

public class SetUpBudget : MonoBehaviour
{
    public Budget4NFT budget4NFT;
    public Slider budgetSlider;
    public DisplayNFTbudhet displayNFTbudhet;

    public void BudgetChanged()
    {
        budget4NFT.SetStartingNFTBudget(budgetSlider.value);
        displayNFTbudhet.DisplayStartingBudget();
    }

    public void SetHard()
    {
        budgetSlider.value = 100000f;
    }
    public void SetEasy()
    {
        budgetSlider.value = 1000f;
    }
}