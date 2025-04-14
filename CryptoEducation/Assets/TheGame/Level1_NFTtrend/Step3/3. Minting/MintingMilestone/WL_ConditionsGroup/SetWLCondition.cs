using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class SetWLCondition : MonoBehaviour
{
    public GameObject conditionsGroup;
    public GameObject hint;
    public TextMeshProUGUI fullRequirment;
    public GameColors gameColors;
    private Button[] buttons;
    List<Button> selectedWLActivities = new List<Button>();
    private Coroutine hideHintCoroutine;
    bool showingWarning;

    void Start()
    {
        hint.SetActive(false);
        buttons = conditionsGroup.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
            button.onClick.AddListener(() => HandleButtonClick(button));
    }

    // Handle button click events
    private void HandleButtonClick(Button button)
    {
        if (selectedWLActivities.Contains(button))
        {
            DeselectButton(button);
        }
        else
        {
            SelectButton(button);
        }
    }

    // Add the button to the selected list and change color
    private void SelectButton(Button button)
    {
        selectedWLActivities.Add(button);
        button.image.color = gameColors.wlNFT;
        ShowRequirmentDescription(button.name);
    }

    // Remove the button from the selected list and reset color
    private void DeselectButton(Button button)
    {
        selectedWLActivities.Remove(button);
        button.image.color = gameColors.defaultWhite;
    }

    public void ShowRequirmentDescription(string buttonNum)
    {
        if (buttonNum == "stop_coroutine" && fullRequirment.color == gameColors.errorColor)
        {
            hint.SetActive(false); // Hide hint if condition met
        }
        else
        {
            DisplayHint(buttonNum);
        }
    }

    private void DisplayHint(string buttonNum)
    {
        hint.SetActive(true);
        fullRequirment.color = gameColors.defaultTextColor;

        if (hideHintCoroutine != null)
            StopCoroutine(hideHintCoroutine);

        hideHintCoroutine = StartCoroutine(HideHint());

        switch (buttonNum)
        {
            case "1":
                fullRequirment.text = "Following the project's official social media accounts (Twitter, Instagram, Discord, etc.).\nLiking, retweeting, and commenting on posts related to the whitelist process";
                break;
            case "2":
                fullRequirment.text = "Actively participating in project discussions, providing feedback, helping to answer questions from other community members";
                break;
            case "3":
                fullRequirment.text = "Referring friends or other potential buyers to the project using referral links";
                break;
            case "4":
                fullRequirment.text = "Being an early supporter at the project launch stage";
                break;
            case "5":
                fullRequirment.text = "Creating and sharing content about the project, such as blog posts, videos, or artwork";
                break;
            case "6":
                fullRequirment.text = "Holding a Specific token or NFT on the blockchain wallet";
                break;
            case "public_wl_price_notification":
                fullRequirment.text = "Just note that WL price is not so favorable compared to the Public price\nBut, you can launch the project on any terms";
                fullRequirment.color = gameColors.errorColor;
                showingWarning = true;
                break;
            case "wl_requirments":
                fullRequirment.text = "You can plan some activities for your project that will reward users with access to WL.\nTo do this, select some categories of activities";
                break;
            case "stop_coroutine":
                OffHint(); // Close hint if user presses close
                break;
            default:
                break;
        }
    }

    private IEnumerator HideHint()
    {
        yield return new WaitForSeconds(13f);
        OffHint();
    }

    public void OffHint()
    {
        if (!showingWarning)
        {
            hint.SetActive(false); // Hide the hint if not showing a warning
        }
        else
        {
            showingWarning = false; // Reset the warning flag
        }
    }
}
