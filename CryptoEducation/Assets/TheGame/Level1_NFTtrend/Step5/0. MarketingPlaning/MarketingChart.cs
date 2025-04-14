using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChart : MonoBehaviour
{
    public Image[] sectionImages;

    public void DisplayMarketingPercents(float[] fills)
    {
        float cumulativeFill = fills.Sum();

        for (int i = 0; i < sectionImages.Length; i++)
        {
            sectionImages[i].fillAmount = cumulativeFill;
            cumulativeFill -= fills[i];
        }
    }
}
