using UnityEngine;

public class AmbasadorsPromotionToggle : MonoBehaviour
{
    public bool isPromotionOn;
    public CastomToggle toggle;
    public GameObject totalAmbassadorsPrice_text;
    AmbassadorsPriceCalculation ambassadorsPriceCalculation;

    void Start()
    {
        ambassadorsPriceCalculation = FindObjectOfType<AmbassadorsPriceCalculation>();
    }

    public void SelectAmbassadorToggle()
    {
        if (toggle.isOn)
        {
            isPromotionOn = true;
            totalAmbassadorsPrice_text.SetActive(true);
            ambassadorsPriceCalculation.CalculateAllAmbassadorsPrice();
        }
        else
        {
            isPromotionOn = false;
            totalAmbassadorsPrice_text.SetActive(false);
            ambassadorsPriceCalculation.DropAmbassadorsPrices();
    }
}
