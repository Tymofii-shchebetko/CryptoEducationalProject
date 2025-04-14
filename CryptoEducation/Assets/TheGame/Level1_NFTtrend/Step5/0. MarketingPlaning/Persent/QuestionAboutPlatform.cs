using UnityEngine;

public class QuestionAboutPlatform : MonoBehaviour
{
    [SerializeField] private GameObject marketingSource;

    public void DisableMedia()
    {
        if (marketingSource != null)
            marketingSource.SetActive(false);
    }

    public void EnableMedia()
    {
        if (marketingSource != null)
            marketingSource.SetActive(true);
    }
}
