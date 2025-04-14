using UnityEngine;

public class SelectIPFS : MonoBehaviour
{
    public CastomToggle toggle;          // Reference to the custom toggle
    private BadgetPlaning badgetPlaning; // Reference to BadgetPlaning class
    private bool operation = false;      // To track the current toggle state

    void Start()
    {
        badgetPlaning = FindObjectOfType<BadgetPlaning>();
    }

    // Method to handle the toggle state change
    public void SelectIPFSToggle()
    {
        // Set the operation based on the toggle's state and pass it to CalculateIPFS
        operation = toggle.isOn;
        badgetPlaning.CalculateIPFS(operation);
    }
}
