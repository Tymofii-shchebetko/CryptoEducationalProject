using UnityEngine;
using TMPro;

public class RarityChanger : MonoBehaviour
{
    public float maxValueChangeRate; // Максимальна швидкість зміни float value
    public float textHighlightScale = 1.2f; // Коефіцієнт збільшення scale тексту при натисканні на джойстик
    public float value = 100;
    public Joystick joystick;
    public TextMeshProUGUI milestone;
    private Vector3 originalScale; // Початковий scale тексту

    void Start()
    {
        originalScale = milestone.transform.localScale;
        milestone.text = value.ToString();
    }

    void Update()
    {
        float dirx = joystick.Horizontal;
        float valueChangeRate = Mathf.Abs(dirx) * maxValueChangeRate;
        value += Mathf.Sign(dirx) * valueChangeRate * Time.deltaTime;

        // Обмеження значення float value в межах 1 і 100
        value = Mathf.Clamp(value, 1f, 100f);

        // Відображаємо float value в TextMeshProUGUI
        milestone.text = Mathf.Round(value).ToString() + "%";

        // Підсвітка тексту при натисканні на джойстик
        if (dirx != 0)
        {
            milestone.transform.localScale = originalScale * textHighlightScale;
        }
        else
        {
            milestone.transform.localScale = originalScale;
        }
    }

    public void DropRarity()
    {
        value = 100;
    }
}
