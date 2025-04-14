using UnityEngine;
using UnityEngine.UI;

public class SelectVisualisation : MonoBehaviour
{
    [Header("UI Elements")]
    public GameColors gameColors;             // Holds the game color settings
    public RectTransform[] pencilSizes;       // Array holding different pencil sizes
    public RectTransform[] tools;             // Array holding different tools (pencil, eraser, sticker)
    public RectTransform stickerButton;       // Sticker button reference

    void Start()
    {
        // Set default pencil size to the largest one (index 2)
        SetPencilSize(pencilSizes[2]);

        // Set default tool to the first tool (pencil)
        ChangeTool(tools[0]);

        // Disabling sticker functionality temporarily due to the issue with large sticker size
        // DisSelectSticker();
    }

    #region Pencil Size Management
    /// <summary>
    /// Sets the selected pencil size and updates the UI elements accordingly.
    /// </summary>
    /// <param name="selectedPencilSize">The pencil size RectTransform to be selected.</param>
    public void SetPencilSize(RectTransform selectedPencilSize)
    {
        // Reset all pencil sizes to default appearance
        foreach (var pencilSize in pencilSizes)
        {
            pencilSize.localScale = Vector3.one; // Reset scale to default
            pencilSize.GetComponent<Image>().color = gameColors.defaultTextColor; // Reset color to default
        }

        // Highlight the selected pencil size
        selectedPencilSize.localScale = new Vector3(1.22f, 1.22f, 1f);   // Slightly enlarge the selected pencil size
        selectedPencilSize.GetComponent<Image>().color = gameColors.purpleA;  // Change color to purple
    }
    #endregion

    #region Tool Selection
    /// <summary>
    /// Changes the selected tool (e.g., pencil, eraser, sticker) and updates the UI.
    /// </summary>
    /// <param name="newTool">The new tool RectTransform to be selected.</param>
    public void ChangeTool(RectTransform newTool)
    {
        // Reset all tools to default appearance
        foreach (var tool in tools)
        {
            tool.localScale = Vector3.one; // Reset scale to default
            tool.GetComponent<Image>().color = gameColors.defaultTextColor; // Reset color to default
        }

        // Highlight the newly selected tool
        newTool.localScale = new Vector3(1.2f, 1.2f, 1f);   // Slightly enlarge the selected tool
        newTool.GetComponent<Image>().color = gameColors.purpleA;  // Change color to purple
    }
    #endregion
}
