using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SetDescriptionlNFTData : MonoBehaviour
{
    [SerializeField] public GameObject utilityGroup;
    [SerializeField] private GameObject utilityError;
    private Button[] utilityButtons;
    private Button selectedButton;

    [SerializeField] private TMP_InputField collectionDescription;
    [SerializeField] private GameObject descriptionError;

    [SerializeField] private GameObject mediaGroup;
    [SerializeField] private GameObject mediaError;
    private Button[] mediaButtons;
    private List<Button> selectedMediaButtons = new List<Button>();

    private NFTData nftData;

    void Start()
    {
        nftData = FindObjectOfType<NFTData>(); // екземпляр класа
        if (nftData == null)
        {
            Debug.LogError("NFTData instance not found in the scene!");
            return;
        }

        utilityButtons = utilityGroup.GetComponentsInChildren<Button>();
        foreach (Button button in utilityButtons)
            button.onClick.AddListener(() => SelectUtility(button));

        mediaButtons = mediaGroup.GetComponentsInChildren<Button>();
        foreach (Button button in mediaButtons)
            button.onClick.AddListener(() => SelectMedia(button));
    }

    #region Utility
    // Select utility button
    public void SelectUtility(Button button)
    {
        if (selectedButton != null)
            selectedButton.image.color = Color.white;

        selectedButton = button;
        selectedButton.image.color = Color.green;
        utilityError.SetActive(false);
    }

    // Set selected utility to nftData
    public void SetUtilityNFT()
    {
        if (selectedButton != null)
        {
            nftData.collectionUtility = selectedButton.name;
            utilityError.SetActive(false);
        }
        else
        {
            utilityError.SetActive(true);
        }
    }
    #endregion

    #region Description
    // Set collection description
    public void SetDescriptionNFT()
    {
        if (string.IsNullOrWhiteSpace(collectionDescription.text))
        {
            descriptionError.SetActive(true);
        }
        else
        {
            descriptionError.SetActive(false);
            nftData.collectionDescription = collectionDescription.text;
        }
    }
    #endregion

    #region Media
    // Select media button
    public void SelectMedia(Button button)
    {
        if (selectedMediaButtons.Contains(button))
        {
            selectedMediaButtons.Remove(button);
            button.image.color = Color.white;
        }
        else
        {
            selectedMediaButtons.Add(button);
            button.image.color = Color.green;
            mediaError.SetActive(false);
        }
    }

    // Set selected media to nftData
    public void SetDescriptionTab()
    {
        if (selectedMediaButtons.Count > 0)
        {
            foreach (Button button in selectedMediaButtons)
            {
                nftData.collectionMedias.Add(button.name);
            }
            mediaError.SetActive(false);
        }
        else
        {
            mediaError.SetActive(true);
        }
    }
    #endregion

    // Check if all fields are validated and ready to proceed
    public void CheckValidation()
    {
        SetUtilityNFT();
        SetDescriptionNFT();
        SetDescriptionTab();

        if (!utilityError.activeSelf && !descriptionError.activeSelf && !mediaError.activeSelf)
        {
            nftData.ShowDescription(); // All fields are filled. -> Proceed to next tab
        }
    }
}
