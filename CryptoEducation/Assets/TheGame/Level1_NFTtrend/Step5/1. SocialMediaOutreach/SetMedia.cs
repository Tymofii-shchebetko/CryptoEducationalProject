using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SetMedia : MonoBehaviour
{
    [SerializeField] private Button[] mediaButtons;
    [SerializeField] private GameObject[] mediaPrices;

    private List<Button> selectedMediaButtons = new();
    public List<GameObject> PricesForSelectedMedia = new();

    public bool isPromotionOn;
    public GameColors gameColors;

    private NFTData nftData;
    private MediaPriceCalculation mediaPriceCalculation;

    void Start()
    {
        nftData = FindObjectOfType<NFTData>();
        mediaPriceCalculation = FindObjectOfType<MediaPriceCalculation>();

        foreach (Button button in mediaButtons)
        {
            button.onClick.AddListener(() => SelectMedia(button));
        }
    }

    public void SelectMedia(Button button)
    {
        int index = System.Array.IndexOf(mediaButtons, button);
        if (index < 0 || index >= mediaPrices.Length) return;

        GameObject priceObject = mediaPrices[index];

        if (selectedMediaButtons.Contains(button))
        {
            DeselectMedia(button, priceObject);
        }
        else
        {
            SelectNewMedia(button, priceObject);
        }

        mediaPriceCalculation.CalculateAllMediaPrice();
    }

    private void SelectNewMedia(Button button, GameObject priceObject)
    {
        selectedMediaButtons.Add(button);
        PricesForSelectedMedia.Add(priceObject);

        button.image.color = gameColors.publicNFT;

        if (isPromotionOn)
        {
            priceObject.SetActive(true);
        }
    }

    private void DeselectMedia(Button button, GameObject priceObject)
    {
        selectedMediaButtons.Remove(button);
        PricesForSelectedMedia.Remove(priceObject);

        button.image.color = gameColors.defaultWhite;

        var boostChanger = priceObject.GetComponent<BoostPriceChanger>();
        if (boostChanger != null)
        {
            boostChanger.boostPrice = 0;
        }

        TMP_Text priceText = priceObject.transform.GetChild(0).GetComponent<TMP_Text>();
        if (priceText != null)
        {
            priceText.text = "0$";
        }

        priceObject.SetActive(false);
    }

    public void MediaPricesActivator(bool setActive)
    {
        foreach (GameObject price in PricesForSelectedMedia)
        {
            price.SetActive(setActive);
        }
    }

    public void SetMediaTab()
    {
        foreach (Button button in selectedMediaButtons)
        {
            if (!nftData.collectionMedias.Contains(button.name))
            {
                nftData.collectionMedias.Add(button.name);
            }
        }
    }
}
