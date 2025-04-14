using TMPro;
using UnityEngine;

public class ElementName : MonoBehaviour
{
    public TMP_Text elementName;
    public int elementNumber;

    public void ChangeElementName(int elementNum)
    {
        elementNumber = elementNum; // Публічна змінна з номером елементу по рахунку
        elementNumber--;
        elementName.text = "Value " + elementNum; // Міняю назву
    }
}
