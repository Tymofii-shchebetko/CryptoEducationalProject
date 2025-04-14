using UnityEngine;
using UnityEngine.UI;

public class ToggleName : MonoBehaviour
{
    private BadgetPlaning badgetPlaning;
    private SelectDomain selectDomain;
    private Toggle toggle;            // Cache the toggle component
    public float getDomainPrice;

    void Start()
    {
        selectDomain = FindObjectOfType<SelectDomain>();
        badgetPlaning = FindObjectOfType<BadgetPlaning>();
        toggle = gameObject.GetComponent<Toggle>(); // Cache the Toggle component here
    }

    // Handles toggling and passes the domain price to BadgetPlaning if selected
    public void GetDomainPrice()
    {
        if (toggle.isOn)
        {
            // Ensure the name of the object can be parsed to float
            if (float.TryParse(gameObject.name, out getDomainPrice))
            {
                badgetPlaning.domainPrice = getDomainPrice;
                badgetPlaning.CalculateDomain();
                selectDomain.CreateTimeCell();
            }
            else
            {
                Debug.LogError("Invalid domain price format.");
            }
        }
    }
}
