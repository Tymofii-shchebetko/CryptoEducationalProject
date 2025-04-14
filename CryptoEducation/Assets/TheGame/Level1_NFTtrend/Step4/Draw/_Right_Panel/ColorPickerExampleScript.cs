using UnityEngine;
using UnityEngine.UI;

public class ColorPickerExampleScript : MonoBehaviour
{
    public Image NFT;
    public Button pencil;
    public SetColorButton setColorButton;

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
        setColorButton.OnClick(currentColor);
        NFT.color = currentColor;
    }

    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }

    public void SwitchToPencil() // Switch From Erase To Pencil
    {
        pencil.onClick.Invoke();
        Debug.Log("Selected");
    }
}
