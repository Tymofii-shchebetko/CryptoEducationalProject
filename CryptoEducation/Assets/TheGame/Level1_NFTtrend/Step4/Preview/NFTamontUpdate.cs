using UnityEngine;
using TMPro;

public class NFTamontUpdate : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public NFTData nftData;
    int num = 1;

    public void ChangeNFTnum()
    {
        num++;
        DisplayNFTnum();
    }
    public void StartNFTnum()
    {
        num = 1;
        DisplayNFTnum();
    }

    public void DisplayNFTnum()
    {
        if (nftData.planedMintAmount > 0)
        {
            amountText.gameObject.SetActive(true);

            if (num > nftData.planedMintAmount)
                num = 1;
            amountText.text = num + " / " + nftData.planedMintAmount;
        }
        else
            amountText.gameObject.SetActive(false);
    }

}
