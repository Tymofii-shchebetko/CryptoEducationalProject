using TMPro;
using UnityEngine;

public class UtilityDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI utilityDescription_Text;
    [SerializeField]
    private GameObject utilityDescription_Button;

    // Using an array to simplify handling of multiple utility descriptions
    [SerializeField]
    private string[] utilityDescriptions;

    // Hides the utility description button
    public void HideUtilityDescription()
    {
        utilityDescription_Button.SetActive(false);
    }

    // Updates the utility description based on the given index
    public void UpdateUtilityDescription(int index)
    {
        if (index < 0 || index >= utilityDescriptions.Length)
        {
            Debug.LogError("Invalid index for utility description.");
            return;
        }

        utilityDescription_Button.SetActive(true);
        utilityDescription_Text.text = utilityDescriptions[index];
    }
}
