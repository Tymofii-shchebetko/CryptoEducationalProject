using UnityEngine;

public class AddElement : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject newElementButton;  // Prefab for the new element button
    public GameObject[] addElementButtons;
    public LayerName layerName;
    private DrawingCanvas drawingCanvas;
    private DrawingValidation drawingValidation;
    public int localElementNum = 1;

    #region Element Creation
    /// <summary>
    /// Creates a new element in the UI and updates its name.
    /// </summary>
    public void CreateNewElement()
    {
        // Instantiate the new element and set its name
        GameObject newElement = Instantiate(newElementButton, transform);
        newElement.name = localElementNum.ToString();
        localElementNum++;

        // Move the Free Space and "Add Element" button to the bottom
        int lastChildIndex = transform.childCount;
        addElementButtons[0].transform.SetSiblingIndex(lastChildIndex - 1);
        addElementButtons[1].transform.SetSiblingIndex(lastChildIndex - 1);

        // Update the element's name in the UI
        newElement.GetComponent<ElementName>().ChangeElementName(localElementNum);
    }
    #endregion

    #region Drawing Canvas Integration
    /// <summary>
    /// Sets the drawing canvas element by copying the sprite.
    /// </summary>
    public void SetTheDrawingCanvasElement()
    {
        drawingCanvas = FindObjectOfType<DrawingCanvas>();
        drawingCanvas.CopySprite(drawingValidation.theLayerContainer);
    }
    #endregion

    #region Element Validation
    /// <summary>
    /// Validates the creation of a new element based on the drawing state.
    /// </summary>
    public void AddElementValidation()
    {
        drawingValidation = FindObjectOfType<DrawingValidation>();

        if (drawingValidation.wasDrawing)
        {
            // Save and add the new element only through the ✓ button
            drawingValidation.HighlightBackAndPreviewButton();
        }
        else if (drawingValidation.drawingDone)
        {
            // Create the new element if the drawing is done
            drawingValidation.GetLayerContainerNum(layerName.theLayerNum);
            CreateNewElement();
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
