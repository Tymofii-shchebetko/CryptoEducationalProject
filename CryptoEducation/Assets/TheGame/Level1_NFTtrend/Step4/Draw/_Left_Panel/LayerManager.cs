using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject newLayerButtonPrefab;    // Prefab for the new layer button
    public GameObject addNewLayerButton;      // Button to add a new layer (moved to the bottom of the side panel)
    public NFTSpritesCreator nftSpritesCreator;
    public DrawingValidation drawingValidation;
    public DrawingCanvas drawingCanvas;

    #region Layer Creation
    /// <summary>
    /// Creates a new layer in the Layers_Panel.
    /// </summary>
    public void CreateNewLayer()
    {
        // Instantiate a new layer button and set its name based on the layer number
        GameObject newLayer = Instantiate(newLayerButtonPrefab, transform);
        newLayer.name = "Layer" + nftSpritesCreator.layerNum;

        // Move the "Add New Layer" button to the bottom of the panel
        int lastChildIndex = transform.childCount;
        addNewLayerButton.transform.SetSiblingIndex(lastChildIndex - 1);

        // Update the layer name with the correct number
        newLayer.GetComponent<LayerName>().ChangeLayerName(nftSpritesCreator.layerNum);

        // Pass the current layer number to the DrawingValidation component
        drawingValidation.GetLayerContainerNum(nftSpritesCreator.layerNum);

        // Add the plus button from the new layer to the list of add buttons in DrawingValidation
        Transform addButton = newLayer.transform.GetChild(newLayer.transform.childCount - 1);
        Transform plusButton = addButton.GetChild(0);
        drawingValidation.addButtons.Add(plusButton.GetComponent<RectTransform>());
    }
    #endregion

    #region Layer Validation
    /// <summary>
    /// Checks the validation before adding a new layer.
    /// </summary>
    public void AddLayerValidation()
    {
        if (drawingValidation.wasDrawing)
        {
            // Save and add layer only through the "✓" button
            drawingValidation.HighlightBackAndPreviewButton();
        }
        else if (drawingValidation.drawingDone)
        {
            // Create a new layer if drawing is done
            CreateNewLayer();
            nftSpritesCreator.CreateNewLayer();
            drawingValidation.drawingDone = false;

            // Reset the Rarity value
            drawingValidation.DropRarityValue();
        }
        else
        {
            // Show error if drawing is not completed or validated
            drawingValidation.ShowError();
        }
    }
    #endregion
}
