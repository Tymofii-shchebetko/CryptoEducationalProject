using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TheToggle : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image background;
    [SerializeField] private Image handle;

    private RectTransform handleRect;
    private float handleWidth;
    private float backgroundWidth;
    private bool isOn;

    private void Start()
    {
        InitializeToggle();
    }

    private void InitializeToggle()
    {
        handleRect = handle.GetComponent<RectTransform>();
        handleWidth = handleRect.rect.width;
        backgroundWidth = background.rectTransform.rect.width;
        UpdateHandlePosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localCursor))
        {
            float clampedX = Mathf.Clamp(localCursor.x - handleWidth / 2, 0, backgroundWidth - handleWidth);
            handleRect.anchoredPosition = new Vector2(clampedX, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float middle = backgroundWidth / 2;
        isOn = handleRect.anchoredPosition.x >= middle;
        UpdateHandlePosition();
    }

    public void ToggleClicked()
    {
        isOn = !isOn;
        UpdateHandlePosition();
    }

    private void UpdateHandlePosition()
    {
        float targetX = isOn ? backgroundWidth - handleWidth : 0;
        handleRect.anchoredPosition = new Vector2(targetX, 0);
    }
}
