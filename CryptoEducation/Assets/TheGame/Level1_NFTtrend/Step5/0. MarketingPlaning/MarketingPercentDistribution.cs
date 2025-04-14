using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class MarketingPercentDistribution : MonoBehaviour
{
    [Header("Configuration")]
    public float maxValueChangeRate = 18f;
    public float textHighlightScale = 1.2f;

    [Header("References")]
    public Joystick[] joysticks = new Joystick[6];
    public TextMeshProUGUI[] percentTexts = new TextMeshProUGUI[6];

    [Header("Runtime Data")]
    public float[] values = new float[6];
    public bool isDraggingPercent;

    private Vector3[] originalScales = new Vector3[6];
    private float unassignedValue = 100f;
    private float[] roundedValues = new float[5]; // values[1..5] normalized to [0..1]
    private MarketingChart pieChart;

    void Start()
    {
        pieChart = FindObjectOfType<MarketingChart>();
        isDraggingPercent = false;

        values[0] = unassignedValue;
        for (int i = 1; i < values.Length; i++) values[i] = 0;

        for (int i = 0; i < percentTexts.Length; i++)
        {
            originalScales[i] = percentTexts[i].transform.localScale;
            percentTexts[i].text = Mathf.Round(values[i]) + "%";
        }
    }

    void Update()
    {
        if (!isDraggingPercent) return;

        float totalAllocated = 0f;

        // Update joystick inputs
        for (int i = 1; i < joysticks.Length; i++)
        {
            ApplyJoystickInput(i, ref values[i], ref unassignedValue);
            totalAllocated += values[i];
        }

        // Clamp unassigned value
        unassignedValue = Mathf.Clamp(100f - totalAllocated, 0f, 100f);
        values[0] = unassignedValue;

        // Overflow fix
        float total = values.Sum();
        if (total > 100f)
        {
            float overflow = total - 100f;
            values[0] = Mathf.Max(0f, values[0] - overflow);
            unassignedValue = values[0];
        }

        // Update UI and pie chart
        percentTexts[0].text = Mathf.Round(values[0]) + "%";

        for (int i = 1; i < values.Length; i++)
        {
            float val = values[i];
            roundedValues[i - 1] = (float)Math.Round(val / 100f, 2);
            percentTexts[i].text = Mathf.Round(val) + "%";

            percentTexts[i].transform.localScale = (joysticks[i].Horizontal != 0)
                ? originalScales[i] * textHighlightScale
                : originalScales[i];

            joysticks[i].GetComponent<ShowSetUpButton>().GetPercent(Mathf.Round(val));
        }

        pieChart.DisplayMarketingPercents(roundedValues);
    }

    private void ApplyJoystickInput(int index, ref float value, ref float remaining)
    {
        float dir = joysticks[index].Horizontal;
        float rate = Mathf.Abs(dir) * maxValueChangeRate;
        float delta = Mathf.Sign(dir) * rate * Time.deltaTime;

        if (dir > 0 && remaining > 0)
        {
            float add = Mathf.Min(delta, remaining);
            value += add;
            remaining -= add;
        }
        else if (dir < 0 && value > 0)
        {
            float sub = Mathf.Min(-delta, value);
            value -= sub;
            remaining += sub;
        }

        value = Mathf.Clamp(value, 0f, 100f);
        remaining = Mathf.Clamp(remaining, 0f, 100f);
    }

    public void OnJoystickTouch() => isDraggingPercent = true;

    public void OnJoystickRelease()
    {
        isDraggingPercent = false;
        ResetTextScales();
    }

    private void ResetTextScales()
    {
        for (int i = 0; i < percentTexts.Length; i++)
        {
            percentTexts[i].transform.localScale = originalScales[i];
        }
    }
}
