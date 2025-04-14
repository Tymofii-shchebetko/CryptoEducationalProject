using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFader : MonoBehaviour
{
    public Image targetImage; // Reference to the Image component
    public float fadeDuration = 0.5f; // Duration of the fade animation
    private GameObject off, on;

    public void FadeImage(GameObject setInactive, GameObject setActive)
    {
        off = setInactive;
        on = setActive;
        StartCoroutine(FadeImageCoroutine());
    }

    IEnumerator FadeImageCoroutine()
    {
        // Set the image active
        targetImage.gameObject.SetActive(true);

        // Fade in
        float timer = 0f;
        while (timer < fadeDuration)
        {
            Color color = targetImage.color;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            targetImage.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the image is fully visible
        Color finalColor = targetImage.color;
        finalColor.a = 1f;
        targetImage.color = finalColor;

        // ВИКЛЮЧАЮ НЕ ПОТРІБНЕ МЕНЮ І ВКЛЮЧАЮ ПОТРІБНЕ
        off.SetActive(false);
        on.SetActive(true);
        
        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            Color color = targetImage.color;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            targetImage.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the image is fully transparent
        finalColor = targetImage.color;
        finalColor.a = 0f;
        targetImage.color = finalColor;

        // Set the image inactive
        targetImage.gameObject.SetActive(false);
    }
}