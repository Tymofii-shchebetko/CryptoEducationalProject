using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct LayerElement
{
    public Sprite sprite;
    public int rarity;
}

public class LayerItems : MonoBehaviour
{
    [Header("Layer Elements")]
    public List<LayerElement> layerElements = new List<LayerElement>(); // List of drawn elements with rarity

    // Calculates the average rarity for the layer, handles the case where the list is empty
    public int CalculateAverageRarity()
    {
        if (layerElements.Count == 0)
        {
            Debug.LogWarning("No elements available to calculate the average rarity.");
            return 0; // Default value when there are no elements
        }

        return (int)layerElements.Average(element => element.rarity);
    }
}
