using UnityEngine;

public class CalculatePrice4PlatformsMarketing : MonoBehaviour
{
    // Generalized function for random value calculation with success rate
    private float GetBiasedRandomValue(float min, float max, float successRate, float randomFactorMin = 0.8f, float randomFactorMax = 1.2f)
    {
        float rate = successRate / 100f;
        float biasedValue = Mathf.Lerp(max, min, rate);

        // Add random variation and clamp the result
        float result = Random.Range(biasedValue * randomFactorMin, biasedValue * randomFactorMax);
        result = Mathf.Clamp(result, min, max);

        return result;
    }

    // Calculate random value based on success rate (integer result, rounded to nearest multiple of 5)
    public int GetRandomValueBasedOnSuccessRate(int min, int max, float successRate)
    {
        // Get the biased random value
        int result = Mathf.RoundToInt(GetBiasedRandomValue(min, max, successRate));

        // Ensure the result is a multiple of 5
        result = Mathf.RoundToInt(result / 5f) * 5;
        return result;
    }

    // Calculate random value based on success rate (float result, rounded to nearest 0.05)
    public float GetRandomFloatBasedOnSuccessRate(float min, float max, float successRate)
    {
        // Get the biased random value
        float result = GetBiasedRandomValue(min, max, successRate);

        // Round to nearest 0.05
        result = Mathf.Round(result / 0.05f) * 0.05f;
        return result;
    }
}
