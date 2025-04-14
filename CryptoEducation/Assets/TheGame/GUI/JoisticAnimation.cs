using UnityEngine;
using System.Collections;

public class JoystickAnimation : MonoBehaviour
{
    [SerializeField] private float moveDistance = 0.3f;
    [SerializeField] private float moveDuration = 0.35f;
    [SerializeField] private float returnDuration = 0.2f;
    [SerializeField] private bool moveRight = true;

    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void StartShaking()
    {
        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(ShakeAnimation());
        }
    }

    public void StopShaking()
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            shakeRoutine = null;
            transform.position = originalPosition;
        }
    }

    private IEnumerator ShakeAnimation()
    {
        Vector3 targetPosition = originalPosition + (moveRight ? Vector3.right : Vector3.left) * moveDistance;

        yield return MoveToPosition(targetPosition, moveDuration);
        yield return MoveToPosition(originalPosition, returnDuration);
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        float elapsedTime = 0f;
        Vector3 start = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
}