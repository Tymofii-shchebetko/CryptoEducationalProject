using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InputFieldError : MonoBehaviour
{
    public GameColors theColor;

    [SerializeField] private TextMeshProUGUI placeholder;
    [SerializeField] private Image bottomLine; // Fixed typo

    public void ShowError()
    {
        if (placeholder != null) placeholder.color = theColor.errorColor;
        if (bottomLine != null) bottomLine.color = theColor.errorColor;
    }

    public void ShowDefault()
    {
        if (placeholder != null) placeholder.color = theColor.defaultTextColor;
        if (bottomLine != null) bottomLine.color = theColor.defaultTextColor;
    }
}
