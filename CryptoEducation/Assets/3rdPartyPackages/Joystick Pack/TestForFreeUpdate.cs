using UnityEngine;
using TMPro;

public class TestForFreeUpdate : MonoBehaviour
{
    public float boostPrice = 0;
    private float maxValueChangeRate = 18; // Максимальна швидкість зміни float value
    private float textHighlightScale = 1.2f; // Коефіцієнт збільшення scale тексту при натисканні на джойстик

    private Joystick joystick;
    private TextMeshProUGUI boostPrice_text;
    private Vector3 originalScale; // Початковий scale тексту

    public bool isJoystickActive = false;


    void Start()
    {
        joystick = GetComponent<FixedJoystick>();
        boostPrice_text = transform.GetComponentInChildren<TextMeshProUGUI>();

        originalScale = boostPrice_text.transform.localScale;
        boostPrice_text.text = boostPrice.ToString();
    }

    private void Update()
    {
        float dirx = joystick.Horizontal;

        if (isJoystickActive)
        {
            float valueChangeRate = Mathf.Abs(dirx) * maxValueChangeRate;
            boostPrice += Mathf.Sign(dirx) * valueChangeRate * Time.deltaTime;

            // Limit the value
            boostPrice = Mathf.Clamp(boostPrice, 0f, 100f);

            // Display the value
            boostPrice_text.text = Mathf.Round(boostPrice).ToString();

            boostPrice_text.transform.localScale = originalScale * textHighlightScale;
        }
    }

    // Call this method when the joystick is touched (e.g., in response to a button press)
    public void OnJoystickTouch()
    {
        isJoystickActive = true;
    }

    // Call this method when the joystick is released (e.g., in response to a button release)
    public void OnJoystickRelease()
    {
        isJoystickActive = false;
        boostPrice_text.transform.localScale = originalScale;
    }
}