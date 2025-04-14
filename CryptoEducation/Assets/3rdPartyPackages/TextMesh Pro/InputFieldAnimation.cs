using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputFieldAnimation : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
{
    private TMP_InputField inputField;
    private string placeholderText;
    public Transform buttomLine;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        placeholderText = inputField.placeholder.GetComponent<TextMeshProUGUI>().text;
    }

    public void OnSelect(BaseEventData eventData)
    {
        // When selected, hide the placeholder text
        inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // When deselected, restore the placeholder text if the input field is empty
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = placeholderText;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttomLine.localScale = new Vector3(transform.localScale.x, 1.5f, transform.localScale.z);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttomLine.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
    }
}