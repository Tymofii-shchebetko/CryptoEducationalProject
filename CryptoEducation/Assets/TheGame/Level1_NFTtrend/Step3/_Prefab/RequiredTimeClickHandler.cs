using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RequiredTimeClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private Color originalColor;
    private Color pressedColor;
    private bool isPointerDown = false;
    private TopicsHighliter topicsHighliter;

    private void Start()
    {
        topicsHighliter = FindObjectOfType<TopicsHighliter>();
        button = GetComponent<Button>();

        // Set the original and pressed colors from button color block
        originalColor = button.colors.normalColor;
        pressedColor = button.colors.pressedColor == originalColor ? Color.gray : button.colors.pressedColor; // Ensure it's distinguishable
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        ChangeButtonColor(pressedColor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        ChangeButtonColor(originalColor);
    }

    private void ChangeButtonColor(Color color)
    {
        // Change the color of the button itself (no need to loop over children)
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        button.colors = colorBlock;

        // Notify the TopicsHighliter that the button state has changed
        topicsHighliter.HighliteHeader(button.name, isPointerDown);
    }
}
