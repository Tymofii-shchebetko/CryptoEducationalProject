using UnityEngine;
using UnityEngine.UI;

public class ElementSelecter : MonoBehaviour
{
    [Header("References")]
    private GameColors gameColors;
    private DrawingValidation drawingValidation;
    private NFTSpritesCreator nftSpritesCreator;
    public ElementName elementName;

    [Header("UI Elements")]
    public GameObject selected;       // The frame around the button when selected
    public GameObject dis_selected;   // The bottom bar for the unselected state
    public GameObject isEditingGO;    // The dot indicating the currently edited element

    private Button buttonComponent;
    public bool isOn = true;          // Button is active
    public bool isEditing;            // Indicates if the button is currently being edited

    void Start()
    {
        // Initialize necessary components
        gameColors = FindObjectOfType<GameColors>();
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(ToggleButtonColor);
        ToggleButtonColor();

        // Handle editing state
        drawingValidation = FindObjectOfType<DrawingValidation>();
        drawingValidation.GetTheIsEditingElement(this);
        Invoke("EditingIsStarted", 0.05f);
    }

    void EditingIsStarted()
    {
        isEditing = true;
        isEditingGO.SetActive(true);
    }

    public void EditingIsFinished()
    {
        isEditing = false;
        isEditingGO.SetActive(false);
    }

    void ToggleButtonColor()
    {
        if (!isEditing) // Prevent selection if the button is being edited
        {
            isOn = !isOn;
            if (isOn)
            {
                buttonComponent.image.color = gameColors.blue;
                selected.SetActive(true);
                dis_selected.SetActive(false);
            }
            else
            {
                buttonComponent.image.color = gameColors.defaultWhite;
                selected.SetActive(false);
                dis_selected.SetActive(true);
            }
        }
        else
        {
            drawingValidation.ShowHighLightedBoarderSize();
        }
    }

    public void SetOffOROnTheDrawnLine()
    {
        if (!isEditing) // Prevent toggling if editing is active
        {
            nftSpritesCreator = FindObjectOfType<NFTSpritesCreator>();
            nftSpritesCreator.SetOffAndOnTheDrawnLine(
                GetComponentInParent<LayerName>().theLayerNum,
                elementName.elementNumber,
                !isOn);
        }
    }
}
