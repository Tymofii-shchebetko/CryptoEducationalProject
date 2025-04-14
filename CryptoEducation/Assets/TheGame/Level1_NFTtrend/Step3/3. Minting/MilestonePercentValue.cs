using UnityEngine;
using TMPro;

public class MilestonePercentValue : MonoBehaviour
{
    // Швидкість зміни значень
    public float maxValueChangeRate = 18f;

    // Скейл % при натисканні на джойстік
    public float textHighlightScale = 1.2f;

    // Джойстики для взаємодії
    public Joystick joystick1;
    public Joystick joystick2;

    // Текстові поля для відображення значень
    public TextMeshProUGUI milestone1;
    public TextMeshProUGUI milestone2;

    // Початкові значення
    public float value1 = 100f;
    public float value2 = 0f;

    // Початковий масштаб для текстових полів
    private Vector3 originalScale1;
    private Vector3 originalScale2;

    public bool isDragingPercent;
    PieChart pieChart;
    GetNFTsAmount getNFTsAmount;
    public InputAndHeaderError inputAndHeaderError;
    public NFTData nftData;
    public LoadPrice loadPrice;
    public CloseButton closeButton;

    // Види джойстиків
    private enum JoystickType
    {
        First,
        Second
    }

    private const float BASE_VALUE = 50f;

    public void Start()
    {
        pieChart = FindObjectOfType<PieChart>(); // екземпляр класа
        getNFTsAmount = FindObjectOfType<GetNFTsAmount>();
        isDragingPercent = false;

        // Запам'ятовування початкового масштабу текстових полів
        originalScale1 = milestone1.transform.localScale;
        originalScale2 = milestone2.transform.localScale;

        // Початкове встановлення тексту відповідно до початкових значень
        milestone1.text = value1 + "%";
        milestone2.text = value2 + "%";
        getNFTsAmount.RecalculateNFTmilestoneAmount(value1);
    }

    void Update()
    {
        if (isDragingPercent)
        {
            // Оновлення значень для обох джойстиків
            UpdateJoystickValues(JoystickType.First, ref value1, ref value2);
            UpdateJoystickValues(JoystickType.Second, ref value2, ref value1);

            // Обмеження значень
            value1 = Mathf.Clamp(value1, 0f, 100f);
            value2 = Mathf.Clamp(value2, 0f, 100f);

            // Оновлення тексту з відображенням у відсотках
            milestone1.text = Mathf.Round(value1) + "%";
            milestone2.text = Mathf.Round(value2) + "%";

            // Підсвічування тексту при взаємодії з джойстиками
            UpdateTextScaleForJoystick(joystick1, milestone1, originalScale1);
            UpdateTextScaleForJoystick(joystick2, milestone2, originalScale2);
        }
    }

    private void UpdateJoystickValues(JoystickType joystickType, ref float mainValue, ref float secondaryValue)
    {
        Joystick mainJoystick = (joystickType == JoystickType.First) ? joystick1 : joystick2;

        // Отримання значення горизонтальної взаємодії з джойстиком
        float dirx = mainJoystick.Horizontal;

        // Розрахунок швидкості зміни значення
        float valueChangeRate = Mathf.Abs(dirx) * maxValueChangeRate;

        // Оновлення основного значення відповідно до руху джойстика
        mainValue += Mathf.Sign(dirx) * valueChangeRate * Time.deltaTime;

        // Оновлення вторинного значення пропорційно основному
        secondaryValue = BASE_VALUE - (mainValue - BASE_VALUE);

        // Обмеження значень
        mainValue = Mathf.Clamp(mainValue, 0f, 100f);
        secondaryValue = Mathf.Clamp(secondaryValue, 0f, 100f);

        // Встановлення нових значень у PieChart, якщо обидва джойстики активні
        if (joystick1.gameObject.activeSelf && joystick2.gameObject.activeSelf)
            pieChart.SetValues(new float[] { mainValue, secondaryValue });
    }

    private void UpdateTextScaleForJoystick(Joystick joystick, TextMeshProUGUI text, Vector3 originalScale)
    {
        text.transform.localScale = joystick.Horizontal != 0 ? originalScale * textHighlightScale : originalScale;
    }

    public void OnJoystickTouch()
    {
        isDragingPercent = true;
    }

    public void OnJoystickRelease()
    {
        isDragingPercent = false;
        milestone1.transform.localScale = originalScale1;
        milestone2.transform.localScale = originalScale2;
        getNFTsAmount.RecalculateNFTmilestoneAmount(value1);
        nftData.percent4Fublic = value1;

        if (nftData.wlMintPrice < 0) // Натякаю юзеру що потрібно ввести ціну для WL
        {
            inputAndHeaderError.SowError();
            loadPrice.MarkWLOff();
        }
        else
        {
            inputAndHeaderError.SowDefault();
            loadPrice.MarkWLOn();
        }
        closeButton.UpdateCloseButton();
    }
}
