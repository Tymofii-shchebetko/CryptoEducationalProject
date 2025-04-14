using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InputAndHeaderError : MonoBehaviour
{
    public GameColors theColor;

    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TextMeshProUGUI placeholder;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image bottomLine;  // Fixed typo from "buttomLine"
    [SerializeField] private TextMeshProUGUI hint;

    public void ShowError()
    {
        SetColors(theColor.errorColor);
        if (hint != null)
        {
            hint.text = "1 - 10 000 NFTs";
        }
    }

    public void ShowDefault()
    {
        SetColors(theColor.defaultTextColor);
    }

    private void SetColors(Color color)
    {
        if (text != null) text.color = color;
        if (header != null) header.color = color;
        if (placeholder != null) placeholder.color = color;
        if (bottomLine != null) bottomLine.color = color;
        if (hint != null) hint.color = color;
    }
}
