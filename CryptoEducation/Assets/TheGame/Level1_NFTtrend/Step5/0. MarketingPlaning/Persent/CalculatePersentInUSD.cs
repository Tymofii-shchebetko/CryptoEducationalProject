using UnityEngine;
using TMPro;

public class CalculatePersentInUSD : MonoBehaviour
{
    public GetFreeFonds getFreeFonds;
    public ShowSetUpButton showSetUpButton;
    public TextMeshProUGUI priceInUSD_text;
    public int priceInUSD;

    public void CalculatePercentInUSD()
    {
        priceInUSD = Mathf.RoundToInt((getFreeFonds.availableBudget * showSetUpButton.pecent) / 100f);
        if (priceInUSD > 1)
            priceInUSD_text.text = "~$" + priceInUSD;
        else
            priceInUSD_text.text = "$0";
    }
}
