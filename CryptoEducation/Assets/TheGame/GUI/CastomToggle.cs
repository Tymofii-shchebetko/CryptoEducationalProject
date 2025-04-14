using UnityEngine;

public class CastomToggle : MonoBehaviour
{
    public bool isOn = false;
    public GameObject off;
    public GameObject on;

    public void TogglePressed()
    {
        isOn = !isOn;
        off.SetActive(!isOn);
        on.SetActive(isOn);
    }
}
