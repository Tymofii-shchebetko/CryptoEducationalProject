using UnityEngine;

public class BackAndPreviewButton : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform boarder;
    public GameObject back;
    public GameObject preview;
    public GameObject done;

    [Header("Dependencies")]
    public SwitchSteps switchSteps;
    public DrawingCanvas drawingCanvas;
    public DrawingValidation drawingValidation;
    public CollectionPreview collectionPreview;
    public NFTamontUpdate nftAmontUpdate;
    public RarityChanger rarityChanger;

    private const float ShowButtonAnimDuration = 0.27f;
    private const float InitialDelay = 0.05f;

    private void Start()
    {
        // Delay showing the "Back" button after the start of the scene
        Invoke(nameof(ShowBack), InitialDelay);
    }

    #region Show / Hide Buttons
    public void ShowDone()
    {
        ToggleButtonsVisibility(false, false, true);
        ShowButtonAnim();
    }

    public void ShowPreview()
    {
        ToggleButtonsVisibility(false, true, false);
        ShowButtonAnim();
    }

    public void ShowBack()
    {
        ToggleButtonsVisibility(true, false, false);
    }

    private void ToggleButtonsVisibility(bool showBack, bool showPreview, bool showDone)
    {
        back.SetActive(showBack);
        preview.SetActive(showPreview);
        done.SetActive(showDone);
    }

    public void ShowButtonAnim()
    {
        boarder.localScale = new Vector3(1.05f, 1.05f, 1f);
        Invoke(nameof(HideButtonAnim), ShowButtonAnimDuration);
    }

    public void HideButtonAnim()
    {
        boarder.localScale = Vector3.one;
    }
    #endregion

    public void ButtonClickedDrow()
    {
        if (back.activeSelf)
        {
            // Step 2: Transition from Step 3 to Step 2
            switchSteps.ToStep2From3();
        }
        else if (done.activeSelf)
        {
            // Save the drawing layer and reset some states
            SaveDrawingLayer();
        }
        else if (preview.activeSelf && drawingValidation.drawingDone)
        {
            // Transition to the Preview Tab
            GoToPreview();
        }
        else
        {
            // Show an error if conditions are not met
            ShowDrawingError();
        }
    }

    private void SaveDrawingLayer()
    {
        drawingValidation.DefaultError4BackAndPreview_Button();
        drawingValidation.drawingDone = true;
        drawingCanvas.CopiSprite(drawingValidation.theLayerContainer);
        drawingValidation.DrawingFalse();
        rarityChanger.DropRarity(); // Reset Rarity when saving the layer
    }

    private void GoToPreview()
    {
        switchSteps.ToPreviewFromDraw();
        collectionPreview.CalculateAverageRarityForCollection();
        collectionPreview.AssignImage();
        nftAmontUpdate.StartNFTnum(); // Show NFT number (1/10)
    }

    private void ShowDrawingError()
    {
        drawingValidation.ShowError();
        Debug.LogError("Drawing validation failed. Please ensure the drawing is completed.");
    }

    #region Preview Tab
    public void ButtonClickedPreviewBack()
    {
        switchSteps.ToDrawFromPreview();
    }

    public void ButtonClickedPreviewDone()
    {
        switchSteps.ToStep2From3();
    }
    #endregion
}
