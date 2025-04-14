using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionPreview : MonoBehaviour
{
    public Transform drawingCanvas;
    public Transform theNFT;
    public GameColors gameColors;
    public GameObject nftLayerPrefab; // Prefab for the new element
    public TextMeshProUGUI theRarity; // Displays the rarity level
    public Image questionImg;
    public Image bottomLinel;

    int layrs;
    int averageRarity; // Total Rarity for the collection
    int nftRarity;

    public void AssignImage()
    {
        nftRarity = 0;

        // Remove previously generated NFTs, if any
        foreach (Transform child in theNFT)
            Destroy(child.gameObject);

        // Create layers for the NFT based on the number of layers
        for (int i = 1; i <= layrs; i++)
        {
            GameObject theLayer = Instantiate(nftLayerPrefab, transform);
            theLayer.transform.SetParent(theNFT); // Set them under the same parent
            theLayer.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            theLayer.GetComponent<RectTransform>().offsetMax = Vector2.zero;

            // Select a random NFT item from the layer
            LayerItems layerItem = drawingCanvas.GetChild(i - 1).GetComponent<LayerItems>();
            int theRandomItem = Random.Range(0, layerItem.layerSprites.Count);
            theLayer.GetComponent<Image>().sprite = layerItem.layerSprites[theRandomItem];

            nftRarity += layerItem.layerRarity[theRandomItem]; // Add rarity for the NFT item
        }
        nftRarity /= layrs; // Calculate average rarity per NFT
        Debug.Log("The rarity of the NFT is: " + nftRarity);

        DisplayRarityInfoOnPreview();
    }

    public void CalculateAverageRarityForCollection()
    {
        averageRarity = 0;
        layrs = drawingCanvas.GetComponent<NFTSpritesCreator>().layerNum;

        for (int i = 1; i <= layrs; i++)
        {
            int averageRarityForLayer = 0;
            LayerItems layerItem = drawingCanvas.GetChild(i - 1).GetComponent<LayerItems>();

            averageRarityForLayer = layerItem.CalculateMediumRarity(); // Calculate average rarity for each layer
            averageRarity += averageRarityForLayer; // Add average rarity for all layers
        }
        averageRarity /= layrs; // Calculate overall average rarity
        Debug.Log("Average value: " + averageRarity);
    }

    public void DisplayRarityInfoOnPreview()
    {
        if (nftRarity >= averageRarity * 0.8f)
        {
            theRarity.text = "Rarity: Ordinary";
            questionImg.color = gameColors.defaultTextColor;
            bottomLinel.color = gameColors.defaultTextColor;
        }
        else if (nftRarity < averageRarity * 0.8f && nftRarity >= averageRarity * 0.3f)
        {
            theRarity.text = "Rarity: Rare";
            questionImg.color = gameColors.purpleA;
            bottomLinel.color = gameColors.purpleA;
        }
        else if (nftRarity < averageRarity * 0.3f && nftRarity >= averageRarity * 0.05f)
        {
            theRarity.text = "Rarity: Very Rare";
            questionImg.color = gameColors.blue;
            bottomLinel.color = gameColors.blue;
        }
        else
        {
            theRarity.text = "Rarity: Super Rare";
            questionImg.color = gameColors.darkBlue;
            bottomLinel.color = gameColors.darkBlue;
        }
    }
}
