using TMPro;
using UnityEngine;

public class LayerName : MonoBehaviour
{
    public TMP_Text layerName;
    public int theLayerNum = 0; // Номер поточного слою, щоб розуміти до якого з усіх слоїв зберігати NFT

    public void ChangeLayerName(int layerNum)
    {
        layerName.text = "Trait " + layerNum;
        theLayerNum = layerNum;
    }
}
