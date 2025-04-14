using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    // Array of images to represent pie chart sectors
    public Image[] imagesPieChart;

    // Array of values for each sector of the pie chart
    public float[] values;

    void Start()
    {
        SetValues(values);
    }

    public void SetValues(float[] valuesToSet)
    {
        // Calculate the total of all values
        float totalValues = 0;
        foreach (float value in valuesToSet)
        {
            totalValues += value;
        }

        // Set the fill amount for each image based on the proportion of the total
        float accumulatedFillAmount = 0;
        for (int i = 0; i < imagesPieChart.Length; i++)
        {
            float fillAmount = valuesToSet[i] / totalValues;
            accumulatedFillAmount += fillAmount;
            imagesPieChart[i].fillAmount = accumulatedFillAmount;
        }
    }
}
