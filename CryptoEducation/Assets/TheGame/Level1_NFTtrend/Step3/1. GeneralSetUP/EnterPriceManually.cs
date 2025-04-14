using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class EnterPriceManually : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TMP_InputField manuallyPriceInput;
    [SerializeField] private Image targetImage; // Reference to the Image component
    [SerializeField] private InputFieldError showError;

    public float fadeDuration = 0.1f; // Duration of the fade animation
    private NFTData nftData;

    void Start()
    {
        nftData = FindObjectOfType<NFTData>();
        header.gameObject.SetActive(false);
        manuallyPriceInput.gameObject.SetActive(false);
        targetImage.gameObject.SetActive(false);
    }

    // Sets the blockchain price manually based on user input.
    public void SetPriceManually()
    {
        if (string.IsNullOrWhiteSpace(manuallyPriceInput.text))
        {
            showError.ShowError();
            return;
        }

        if (float.TryParse(manuallyPriceInput.text, out float price))
        {
            nftData.blockchainCurrentPrice = price;
            Debug.Log("The price is set: " + price);
            StartCoroutine(HidePriceSettingCoroutine());
        }
        else
        {
            showError.ShowError();
        }
    }

    public void DeselectPriceInput()
    {
        manuallyPriceInput.image.color = Color.white;
    }

    // Displays the manual price input UI with a fade-in effect.
    public void ShowPriceSetting()
    {
        StartCoroutine(ShowPriceSettingCoroutine());
    }

    private IEnumerator ShowPriceSettingCoroutine()
    {
        header.text = $"Oops... Unfortunately, we are unable to load the {nftData.collectionBlockchain} price.\nEnter the current price manually";
        header.gameObject.SetActive(true);
        manuallyPriceInput.gameObject.SetActive(true);
        yield return StartCoroutine(FadeImage(targetImage, 0f, 1f));
    }

    private IEnumerator HidePriceSettingCoroutine()
    {
        header.gameObject.SetActive(false);
        manuallyPriceInput.gameObject.SetActive(false);
        manuallyPriceInput.text = "";
        yield return StartCoroutine(FadeImage(targetImage, 1f, 0f));
        targetImage.gameObject.SetActive(false);
    }

    // Smoothly fades an image's alpha from startAlpha to endAlpha over fadeDuration.
    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = image.color;

        image.gameObject.SetActive(true);

        while (timer < fadeDuration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            image.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }
}
