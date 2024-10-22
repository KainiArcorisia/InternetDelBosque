using UnityEngine;
using System.Collections;

public class BurbujaDialogo : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Visibility")]
    [SerializeField] private bool startVisible = false;

    [Header("Blink Configuration")]
    [SerializeField] private float blinkDuration = 1f;
    [SerializeField, Range(0f, 1f)] private float minAlpha = 0.2f;
    [SerializeField, Range(0f, 1f)] private float maxAlpha = 1f;
    [SerializeField] private AnimationCurve blinkCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Float Configuration")]
    [SerializeField] private float floatAmplitude = 0.1f;
    [SerializeField] private float floatFrequency = 1f;

    [Header("Trigger")]
    [SerializeField] private string playerTag = "Player";

    private Vector3 startPosition;
    private Coroutine blinkCoroutine;
    private Coroutine floatCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
            enabled = false;
            return;
        }

        startPosition = transform.position;
        SetVisibility(startVisible);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SetVisibility(true);
            StartEffects();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SetVisibility(false);
            StopEffects();
        }
    }

    private void SetVisibility(bool isVisible)
    {
        spriteRenderer.enabled = isVisible;
    }

    private void StartEffects()
    {
        if (blinkCoroutine == null)
        {
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
        if (floatCoroutine == null)
        {
            floatCoroutine = StartCoroutine(FloatCoroutine());
        }
    }

    private void StopEffects()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        if (floatCoroutine != null)
        {
            StopCoroutine(floatCoroutine);
            floatCoroutine = null;
        }
        transform.position = startPosition;
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return FadeSprite(maxAlpha, minAlpha);
            yield return FadeSprite(minAlpha, maxAlpha);
        }
    }

    private IEnumerator FadeSprite(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / blinkDuration;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, blinkCurve.Evaluate(t));
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    private IEnumerator FloatCoroutine()
    {
        while (true)
        {
            float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = startPosition + new Vector3(0f, yOffset, 0f);
            yield return null;
        }
    }
}