using UnityEngine;

public class OnOffMetadata : MonoBehaviour
{
    public GameObject metadata;

    public void MetadataOn()
    {
        metadata.SetActive(true);
    }
    public void MetadataOff()
    {
        Invoke("SetOff", 0.5f);
    }
    void SetOff()
    {
        metadata.SetActive(false);
    }
}
