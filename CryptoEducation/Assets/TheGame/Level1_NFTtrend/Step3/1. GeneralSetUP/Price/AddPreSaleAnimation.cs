using System.Collections;
using UnityEngine;

public class AddPreSaleAnimation : MonoBehaviour
{
    public RectTransform preSaleButton;
    public float duration = 0.2f;
    private Coroutine currentCoroutine;

    void Start()
    {
        preSaleButton.localScale = new Vector3(preSaleButton.localScale.x, 0f, preSaleButton.localScale.z);
    }

    public void ShowPreSaleButton()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ScaleOverTime(0f, 1f));
    }

    public void HidePreSaleButton()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ScaleOverTime(1f, 0f));
    }

    IEnumerator ScaleOverTime(float startScale, float targetScale)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = new Vector3(preSaleButton.localScale.x, startScale, preSaleButton.localScale.z);
        Vector3 target = new Vector3(preSaleButton.localScale.x, targetScale, preSaleButton.localScale.z);
        
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            preSaleButton.localScale = Vector3.Lerp(initialScale, target, t);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        preSaleButton.localScale = target;
    }
}
