using UnityEngine;
using UnityEngine.UI;

public class MakeTheNFTcover : MonoBehaviour
{
    [Header("NFT Cover Elements")]
    public Transform sourceCover; 
    public Transform targetCover;  
    public Image coverImage;      
    public Sprite newCoverSprite; 

    // Method to update the cover image and apply new layers from the source cover
    public void ChangeTheCover()
    {
        // Set the new sprite on the cover image
        coverImage.sprite = newCoverSprite;

        // Remove previous cover layers in the target cover
        RemovePreviousCoverLayers();

        // Add new layers from the source cover to the target cover
        CopyCoverLayers();
    }

    // Removes all previous cover layers from the target cover
    private void RemovePreviousCoverLayers()
    {
        foreach (Transform child in targetCover)
        {
            Destroy(child.gameObject);
        }
    }

    // Instantiates new cover layers into the target cover
    private void CopyCoverLayers()
    {
        foreach (Transform child in sourceCover)
        {
            Instantiate(child, targetCover);
        }
    }
}
