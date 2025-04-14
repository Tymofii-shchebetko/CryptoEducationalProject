using TMPro;
using UnityEngine;

public class DisplayNFTbudhet : MonoBehaviour
{
    public Budget4NFT budget4NFT;
    [SerializeField]
    TextMeshProUGUI budget_Text;

    public void DisplayStartingBudget()
    {
        budget_Text.text = "$" + budget4NFT.GetStartingNFTBudget();
    }
}
