using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Set the color in which to draw
/// </summary>
public class SetColorButton : MonoBehaviour
{
    /// <summary>
    /// Color to set drawing to
    /// </summary>
    public Color theColor;
    /// <summary>
    /// Reference to drawingCanvas
    /// </summary>
    public DrawingCanvas drawingCanvas;

    /// <summary>
    /// On button cilck, set the drawing color to <see cref="color"/>
    /// </summary>
    public void OnClick(Color color)
    {
        if (drawingCanvas == null)
        {
            Debug.LogError("No DrawingCanvas assigned to button", this.gameObject);
            return;
        }
        drawingCanvas.SetColor(color);
        theColor = color;
    }

    public void ChoosePensil()
    {
        if (drawingCanvas == null)
        {
            Debug.LogError("No DrawingCanvas assigned to button", this.gameObject);
            return;
        }
        drawingCanvas.SetColor(theColor);
    }




    /*
    /// <summary>
    // Просто для бекапа робочий код з іншого скрипта код
    /// </summary>
    public Image NFT;
    void Start()
    {
        ChooseColorButtonClick();
    }
    public void ChooseColorButtonClick()
    {
        ColorPicker.Create(NFT.color, "Choose the cube's color!", SetColor, ColorFinished, true);
    }
    private void SetColor(Color currentColor)
    {
        NFT.color = currentColor;
    }

    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }
    */
}
