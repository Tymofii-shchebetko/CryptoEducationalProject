using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SetCalendar : MonoBehaviour
{
    public GameObject nftPlatformPrefab;
    public Transform calendarContainer;
    public PlatformPriceCalculation platformPriceCalculation;
    public NFTPlatformsGenerator nftPlatformsGenerator;
    public GameColors gameColors;
    List<Button> selectedCalendars = new List<Button>(); // List of selected calendars

    public void GenerateCalendar() // Generate calendars based on the blockchain
    {
        // Clean up existing calendars before generating new ones
        foreach (Transform child in calendarContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate new calendar buttons
        for (int i = 0; i < nftPlatformsGenerator.calendars.Count; i++)
        {
            GameObject newCalendar = Instantiate(nftPlatformPrefab, calendarContainer);
            Button button = newCalendar.GetComponent<Button>();

            // Ensure listeners are not added multiple times
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectCalendar(button));

            newCalendar.name = nftPlatformsGenerator.calendars[i];

            // Set the calendar name and price
            newCalendar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nftPlatformsGenerator.calendars[i];
            newCalendar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + nftPlatformsGenerator.calendarsPrice[i];

            // Set platform data
            newCalendar.GetComponent<NFTplatformData>().SetPlatformData("Calendar", newCalendar.name, nftPlatformsGenerator.calendarsPrice[i]);
        }
    }

    // Handle calendar selection
    public void SelectCalendar(Button button)
    {
        // Toggle selection of the calendar
        if (selectedCalendars.Contains(button))
        {
            selectedCalendars.Remove(button);
            button.image.color = gameColors.defaultWhite; // Reset color if deselected
        }
        else
        {
            selectedCalendars.Add(button);
            button.image.color = gameColors.traditional; // Highlight the selected calendar
        }

        // Calculate the total price of all selected calendars
        float totalCalendarPromotionPrice = CalculateTotalSelectedPrice();
        platformPriceCalculation.GetCalendarPrice((int)totalCalendarPromotionPrice); // Recalculate the budget
    }

    // Method to calculate the total price of selected calendars
    private float CalculateTotalSelectedPrice()
    {
        float total = 0;
        foreach (Button calendarButton in selectedCalendars)
        {
            total += calendarButton.GetComponent<NFTplatformData>().platformCost;
        }
        return total;
    }
}
