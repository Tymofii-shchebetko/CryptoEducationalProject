using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SelectHosting : MonoBehaviour
{
    [SerializeField]
    public GameObject hostingGroup;
    public float getHostingPrice;
    bool isHostingSelected = false;

    private Button[] buttons;
    private Button selectedButton;

    //TimeConsuming timeConsuming;
    BadgetPlaning badgetPlaning;

    void Start()
    {
        //timeConsuming = FindObjectOfType<TimeConsuming>();
        badgetPlaning = FindObjectOfType<BadgetPlaning>();

        buttons = hostingGroup.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => GetHosting(button));
        }
    }

    public void GetHosting(Button button)
    {
        // Deselect previous button if there's any
        if (selectedButton != null)
            selectedButton.image.color = Color.white;

        selectedButton = button;
        selectedButton.image.color = Color.green;

        // Try to parse the price
        if (float.TryParse(selectedButton.transform.GetChild(1).name, out float price))
        {
            getHostingPrice = price;
            badgetPlaning.hostingPrice = getHostingPrice;
            badgetPlaning.CalculateHosting();
        }
        else
        {
            Debug.LogError("Invalid hosting price format.");
        }

        // If hosting is not selected, trigger the first-time selection process
        if (!isHostingSelected)
        {
            //timeConsuming.CreateNewCells(1, "Hosting");
            isHostingSelected = true;
        }
    }

    // Proper event cleanup using a separate helper method
    private void OnDestroy()
    {
        foreach (Button button in buttons)
        {
            // Remove the button listener correctly
            button.onClick.RemoveListener(() => GetHosting(button));  // Cannot directly use this in RemoveListener
        }
    }
}
