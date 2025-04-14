using UnityEngine;

public class ShowSetUpButton : MonoBehaviour
{
    public AddPreSaleAnimation addPreSaleAnimation;
    public float percent; // Corrected the typo from "pecent"

    // Updates the percent value
    public void SetPercent(float newPercent)
    {
        percent = newPercent;
    }

    // Triggers the pre-sale button animation
    public void ShowSetUp()
    {
        if (addPreSaleAnimation != null)
            addPreSaleAnimation.ShowPreSaleButton();
    }

    // Hides the button if the value is below 1%
    public void HideSetUp()
    {
        if (percent < 1f && addPreSaleAnimation != null)
            addPreSaleAnimation.HidePreSaleButton();
    }
}
