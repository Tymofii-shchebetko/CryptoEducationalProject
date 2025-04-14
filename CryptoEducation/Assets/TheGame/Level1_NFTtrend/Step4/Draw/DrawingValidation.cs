using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DrawingValidation : MonoBehaviour
{
    [Header("Drawing State")]
    public bool isDrawingInProgress = false; // Indicates if the user has started drawing in the active layer
    public bool isDrawingComplete = false;   // Indicates if the user has finished drawing in the active layer

    [Header("Layer and UI References")]
    public int activeLayerContainerIndex;   // Stores the index of the last active layer container
    public RectTransform drawingCanvasBorder;  // The border of the drawing canvas (for error highlighting)
    public RectTransform previewButtonBorder;  // The border of the preview button (for error highlighting)

    [Header("Game Components")]
    public GameColors gameColors;             // Game colors for UI feedback (e.g., error colors)
    public BackAndPreviewButton backAndPreviewButton;  // Reference to back and preview button for status updates
    public RarityChanger rarityChanger;      // Reference to rarity changer for interacting with the rarity system
    private ElementSelecter elementSelecter; // Reference to the element selector to handle editing state

    [Header("UI Buttons")]
    public List<RectTransform> addLayerButtons = new List<RectTransform>(); // List of "Add Layer" buttons to highlight during errors

    void Start()
    {
        // No need to call DrawingFalse() here as it seems unnecessary at the start
    }

    #region Layer Container Management
    // Receives the active layer container index to maintain the correct layer context
    public void SetActiveLayerContainer(int index)
    {
        activeLayerContainerIndex = index;
    }
    #endregion

    #region Error Handling for Drawing Canvas Border
    // Shows error state by highlighting the drawing canvas border
    public void ShowDrawingError()
    {
        HighlightDrawingCanvasBorder(gameColors.errorColor);
        AnimateBorderHighlight();
    }

    // Resets the border size after showing the error state
    private void AnimateBorderHighlight()
    {
        drawingCanvasBorder.offsetMin = new Vector2(-16, -16);
        drawingCanvasBorder.offsetMax = new Vector2(16, 16);
        Invoke("RestoreBorderSize", 0.27f);
    }

    // Restores the border to its default size after error highlight
    private void RestoreBorderSize()
    {
        drawingCanvasBorder.offsetMin = new Vector2(-10, -10);
        drawingCanvasBorder.offsetMax = new Vector2(10, 10);
    }

    // Resets the border color to the default color
    public void ResetDrawingError()
    {
        HighlightDrawingCanvasBorder(gameColors.defaultTextColor);
    }

    // Helper method to change border color
    private void HighlightDrawingCanvasBorder(Color color)
    {
        drawingCanvasBorder.gameObject.GetComponent<Image>().color = color;
    }
    #endregion

    #region Error Handling for Add Layer Buttons
    // Highlights all "Add New Layer" buttons in case of error
    public void HighlightAddLayerButtons()
    {
        foreach (RectTransform addButton in addLayerButtons)
        {
            addButton.localScale = new Vector3(1.1f, 1.1f, 1f);
            ChangeButtonBorderColor(addButton, gameColors.errorColor);
        }
        Invoke("RestoreAddLayerButtons", 0.27f);
    }

    // Restores "Add New Layer" buttons to their default state
    private void RestoreAddLayerButtons()
    {
        foreach (RectTransform addButton in addLayerButtons)
        {
            addButton.localScale = Vector3.one;
            ChangeButtonBorderColor(addButton, gameColors.defaultTextColor);
        }
    }

    // Helper method to change the border color of the buttons
    private void ChangeButtonBorderColor(RectTransform button, Color color)
    {
        Transform buttonImage = button.GetChild(1);
        buttonImage.gameObject.GetComponent<Image>().color = color;
    }
    #endregion

    #region Error Handling for Back and Preview Button
    // Highlights the Back and Preview button when there is an error
    public void HighlightBackAndPreviewButton()
    {
        previewButtonBorder.localScale = new Vector3(1.1f, 1.1f, 1f);
        ChangeButtonBorderColor(previewButtonBorder, gameColors.errorColor);
        Invoke("RestoreBackAndPreviewButton", 0.27f);
    }

    // Restores the Back and Preview button to its default state
    private void RestoreBackAndPreviewButton()
    {
        previewButtonBorder.localScale = Vector3.one;
        ResetBackAndPreviewButtonError();
    }

    // Resets the color of the Back and Preview button to its default state
    private void ResetBackAndPreviewButtonError()
    {
        ChangeButtonBorderColor(previewButtonBorder, gameColors.defaultTextColor);
    }
    #endregion

    #region Drawing Validation
    // Marks the drawing as in progress and shows the "Done" button
    public void StartDrawing()
    {
        isDrawingInProgress = true;
        backAndPreviewButton.ShowDone();
    }

    // Resets the drawing state and shows the "Preview" button
    public void CancelDrawing()
    {
        isDrawingInProgress = false;
        backAndPreviewButton.ShowPreview();
        elementSelecter.CompleteEditing();
    }
    #endregion

    #region Element Selection
    // Sets the reference to the active element selector for editing
    public void SetActiveElementSelector(ElementSelecter elementSelector)
    {
        elementSelecter = elementSelector;
    }
    #endregion

    #region Rarity Management
    // Drops the rarity value, resetting any changes made to rarity
    public void DropRarityValue()
    {
        rarityChanger.DropRarity();
    }
    #endregion
}
