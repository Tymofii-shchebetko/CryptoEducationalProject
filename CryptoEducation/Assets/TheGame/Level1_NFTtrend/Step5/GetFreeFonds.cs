using UnityEngine;
using TMPro;

public class GetFreeFonds : MonoBehaviour
{
    [Header("References")]
    public Budget4NFT budget4NFT;
    public TextMeshProUGUI freeBudget_text;
    public GameColors gameColors;

    [Header("Budget Info")]
    [SerializeField] private float availableBudget;
    [SerializeField] private float allFonds;
    [SerializeField] private float usedFonds;

    private void Start()
    {
        allFonds = budget4NFT.GetStartingNFTBudget();
        UpdateFreeBudgetDisplay();
    }

    public void GetUsedFonds(float receivedUsedFonds)
    {
        usedFonds = receivedUsedFonds;
    }

    public void UpdateFreeBudgetDisplay()
    {
        availableBudget = allFonds - usedFonds;
        freeBudget_text.text = $"${availableBudget:F2}";
        freeBudget_text.color = availableBudget < 0 ? gameColors.errorColor : gameColors.defaultTextColor;
    }
}
