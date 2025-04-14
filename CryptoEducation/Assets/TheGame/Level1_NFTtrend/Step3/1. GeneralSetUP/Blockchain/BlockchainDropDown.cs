using UnityEngine;

public class BlockchainDropDown : MonoBehaviour
{
    public RectTransform container;
    public RectTransform blockchainAria;
    private bool isOpen;
    private float transitionSpeed = 12f;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        Vector3 scale = container.localScale;
        scale.y = Mathf.MoveTowards(scale.y, isOpen ? 1f : 0f, Time.deltaTime * transitionSpeed);
        container.localScale = scale;

        if (Input.GetMouseButtonDown(0))
        {
            if (blockchainAria != null && !IsMouseOverBlockingArea())
                CloseContainer();
        }
    }

    public void ToggleContainer()
    {
        isOpen = !isOpen;
    }

    public void CloseContainer()
    {
        isOpen = false;
    }

    private bool IsMouseOverBlockingArea()
    {
        return blockchainAria != null &&
               RectTransformUtility.RectangleContainsScreenPoint(blockchainAria, Input.mousePosition, Camera.main);
    }
}
