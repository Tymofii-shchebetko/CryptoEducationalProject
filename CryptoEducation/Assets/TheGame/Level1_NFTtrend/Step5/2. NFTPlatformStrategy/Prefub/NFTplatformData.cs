using UnityEngine;

public class NFTplatformData : MonoBehaviour
{
    [SerializeField]
    private string platformDirection;  // The direction (Launchpad, Calendar, Marketplace)
    [SerializeField]
    private string platformName;       // The platform's name
    [SerializeField]
    private float platformCost;        // The cost associated with this platform

    public string PlatformDirection => platformDirection;
    public string PlatformName => platformName;
    public float PlatformCost => platformCost;

    public void SetPlatformData(string direction, string name, float cost)
    {
        // You could add additional validation here, e.g., ensure cost is non-negative
        platformDirection = direction;
        platformName = name;
        platformCost = Mathf.Max(0, cost);  // Ensure cost is non-negative
    }

    public void ChangeCost(float cost)
    {
        platformCost = Mathf.Max(0, cost);  // Ensure cost is non-negative
    }
}
