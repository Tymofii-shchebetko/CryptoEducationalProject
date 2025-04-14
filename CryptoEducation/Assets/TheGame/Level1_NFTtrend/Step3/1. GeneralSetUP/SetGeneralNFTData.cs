using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SetGeneralNFTData : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] private TMP_InputField collectionName;
    [SerializeField] private InputFieldError nameError;

    [Header("Tag")]
    [SerializeField] private TMP_InputField collectionTag;
    [SerializeField] private InputFieldError tagError;

    [Header("Supply")]
    [SerializeField] private TMP_InputField mintAmount;
    [SerializeField] private InputAndHeaderError supplyError;
    private int amount;
    public GetNFTsAmount getNFTsAmount;

    [Header("Price")]
    [SerializeField] private TMP_InputField collectionPrice;
    [SerializeField] private InputAndHeaderError priceError;
    public TheToggle toggle;
    public GameObject milestonePercent_text;
    public AddPreSaleAnimation addPreSaleAnimation;

    [Header("Fee")]
    [SerializeField] private TMP_InputField creatorFee;
    [SerializeField] private GameObject feeHint;

    private NFTData nftData;

    void Start()
    {
        nftData = FindObjectOfType<NFTData>();
        if (nftData == null)
        {
            Debug.LogError("NFTData component not found in the scene!");
            return;
        }
        milestonePercent_text.SetActive(false);
        feeHint.SetActive(false);
    }

    public void SetNameNFT()
    {
        if (string.IsNullOrWhiteSpace(collectionName.text))
            nameError.ShowError();
        else
        {
            nameError.ShowDefault();
            nftData.collectionName = collectionName.text;
        }
    }

    public void SetTagNFT()
    {
        if (string.IsNullOrWhiteSpace(collectionTag.text))
            tagError.ShowError();
        else
        {
            tagError.ShowDefault();
            nftData.collectionTag = collectionTag.text;
        }
    }

    public void SetAmountNFT()
    {
        if (int.TryParse(mintAmount.text, out amount) && amount > 0 && amount <= 10000)
        {
            supplyError.ShowDefault();
            nftData.planedMintAmount = amount;
            milestonePercent_text.SetActive(nftData.mintPrice >= 0);

            if (amount >= 2)
            {
                addPreSaleAnimation.ShowPreSaleButton();
                if (getNFTsAmount != null && nftData.percent4Fublic > 0)
                    getNFTsAmount.RecalculateNFTmilestoneAmount(nftData.percent4Fublic);
            }
            else
                addPreSaleAnimation.HidePreSaleButton();
        }
        else
        {
            supplyError.ShowError();
            addPreSaleAnimation.HidePreSaleButton();
            milestonePercent_text.SetActive(false);
        }
    }

    public void SetMintPrice()
    {
        if (float.TryParse(collectionPrice.text, out float result))
        {
            toggle.isOn = result > 0f;
            toggle.ToggleClicked();
            nftData.mintPrice = result;

            milestonePercent_text.SetActive(true);
            if (nftData.planedMintAmount >= 2 && result > 0f)
                addPreSaleAnimation.ShowPreSaleButton();
            else
                addPreSaleAnimation.HidePreSaleButton();
        }
        else
        {
            priceError.ShowError();
            nftData.mintPrice = -1;
            addPreSaleAnimation.HidePreSaleButton();
            milestonePercent_text.SetActive(false);
        }
    }

    public void SetCreatorFee()
    {
        if (!float.TryParse(creatorFee.text, out float fee) || fee < 0f || fee > 100f)
        {
            feeHint.SetActive(true);
            creatorFee.text = "0.0 %";
        }
        else
        {
            creatorFee.text = fee.ToString("F1") + " %";
            feeHint.SetActive(false);
            nftData.creatorFee = Mathf.Round(fee * 10f) / 10f;
        }
    }

    public void CheckValidation()
    {
        SetNameNFT();
        SetTagNFT();
        SetAmountNFT();
    }
}