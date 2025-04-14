using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    [Header("UI Elements")]
    public Image image1;
    public Image image2;

    [Header("Animation Settings")]
    [SerializeField] private float expandedWidth = 145f;
    [SerializeField] private float defaultWidth = 128f;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float delayBetweenAnimations = 1.5f;
    [SerializeField] private float sequenceRestartDelay = 3f;

    private void Start()
    {
        StartCoroutine(AnimationSequence());
    }

    private IEnumerator AnimationSequence()
    {
        while (true)
        {
            yield return AnimateSize(image1, expandedWidth, animationDuration);
            yield return new WaitForSeconds(delayBetweenAnimations);
            yield return AnimateSize(image2, expandedWidth, animationDuration);
            yield return new WaitForSeconds(sequenceRestartDelay);
        }
    }

    private IEnumerator AnimateSize(Image image, float targetWidth, float duration)
    {
        float startTime = Time.time;
        float initialWidth = image.rectTransform.sizeDelta.x;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            SetImageWidth(image, Mathf.Lerp(initialWidth, targetWidth, t));
            yield return new WaitForEndOfFrame();
        }

        SetImageWidth(image, targetWidth);
        yield return AnimateSize(image, defaultWidth, duration);
    }

    private void SetImageWidth(Image image, float width)
    {
        Vector2 size = image.rectTransform.sizeDelta;
        size.x = width;
        image.rectTransform.sizeDelta = size;
    }
}