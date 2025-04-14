using UnityEngine;
using UnityEngine.UI;

public class NFTSpritesCreator : MonoBehaviour
{
    [Header("NFT Layer and Element Prefabs")]
    public GameObject newLayerPrefab; // Prefab for the new layer
    public GameObject newElementPrefab; // Prefab for the new element
    public BackAndPreviewButton backAndPreviewButton;
    public CollectionPreview collectionPreview;
    public RarityChanger rarityChanger;

    [Header("Transform References")]
    public Transform editTool; // Container where the new layer is built
    public int layerNum = 0; // Index of the last created layer

    void Start()
    {
        // Initialize by creating the first layer
        CreateNewLayer();
    }

    // Method to create a new layer in the NFT creation process
    public void CreateNewLayer()
    {
        // Instantiate the new layer and set its name
        GameObject newLayer = Instantiate(newLayerPrefab, transform);
        newLayer.name = "Layer" + layerNum;

        // Move the edit tool (drawing area) to the bottom of the layer stack
        MoveEditToolToBottom();

        // Increment layer number for the next layer
        layerNum++;
    }

    // Helper method to move the edit tool to the bottom of the current layer stack
    private void MoveEditToolToBottom()
    {
        int lastChildIndex = transform.childCount;
        editTool.SetSiblingIndex(lastChildIndex);
    }

    // Method to create a new element (sprite) within a specific layer
    public void CreateNewElement(Sprite source, int addToLayer)
    {
        // Instantiate the new element from the prefab
        GameObject copiedImageObject = Instantiate(newElementPrefab, transform);

        // Set the sprite and transparency of the copied image
        SetElementSpriteAndTransparency(copiedImageObject, source);

        // Parent the new element to the appropriate layer
        copiedImageObject.transform.SetParent(transform.GetChild(addToLayer));

        // Update the layer with the new sprite and its rarity
        UpdateLayerWithNewElement(addToLayer, source);
    }

    // Helper method to set the sprite and transparency of the element
    private void SetElementSpriteAndTransparency(GameObject copiedImageObject, Sprite source)
    {
        Image copiedImage = copiedImageObject.GetComponent<Image>();
        copiedImage.sprite = source;
        copiedImage.color = new Color(copiedImage.color.r, copiedImage.color.g, copiedImage.color.b, 0.2f); // Set transparency to 20%
    }

    // Helper method to update the layer with the new element's sprite and rarity
    private void UpdateLayerWithNewElement(int addToLayer, Sprite source)
    {
        LayerItems layerItems = transform.GetChild(addToLayer).GetComponent<LayerItems>();

        // Add the sprite and rarity to the respective layer's data
        layerItems.layerSprites.Add(source);
        layerItems.layerRarity.Add((int)rarityChanger.value);
    }

    // Method to toggle the visibility of a drawn line within a layer and element
    public void SetOffAndOnTheDrawnLine(int layer, int element, bool isOn)
    {
        if (IsLayerValid(layer))
        {
            Transform childLayer = transform.GetChild(layer);

            if (IsElementValid(childLayer, element))
            {
                Transform elementTransform = childLayer.GetChild(element);
                elementTransform.gameObject.SetActive(isOn);
            }
        }
    }

    // Helper method to check if the layer index is valid
    private bool IsLayerValid(int layer)
    {
        return layer >= 0 && layer < transform.childCount;
    }

    // Helper method to check if the element index is valid
    private bool IsElementValid(Transform layer, int element)
    {
        return element >= 0 && element < layer.childCount;
    }
}
