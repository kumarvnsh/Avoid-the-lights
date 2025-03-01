using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Moves a UI light element randomly around the screen,
/// changing its alpha over time.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class LightMover : MonoBehaviour
{
    [Header("References")]
    public Canvas canvas;            // Canvas for boundary reference
    private RectTransform lightElement;
    private Image lightImage;

    [Header("Movement Settings")]
    public float moveSpeed = 100f;
    private Vector2 randomTargetPosition;

    [Header("Alpha Settings")]
    public float alphaChangeSpeed = 2f;
    private float currentAlpha = 1f;
    private float targetAlpha;

    private void Start()
    {
        lightElement = GetComponent<RectTransform>();
        lightImage = GetComponent<Image>();

        if (!lightElement || !canvas || !lightImage)
        {
            Debug.LogError("LightMover: Missing required components (RectTransform, Canvas, Image).");
            return;
        }

        // Initialize random target values
        SetRandomTargetPosition();
        targetAlpha = Random.Range(0.2f, 1f);
    }

    private void Update()
    {
        if (!lightElement || !canvas || !lightImage) return;

        // Move toward the random target position
        Vector2 currentPosition = lightElement.anchoredPosition;
        lightElement.anchoredPosition = Vector2.MoveTowards(
            currentPosition,
            randomTargetPosition,
            moveSpeed * Time.deltaTime
        );

        // Change alpha toward the target alpha
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, alphaChangeSpeed * Time.deltaTime);
        Color currentColor = lightImage.color;
        currentColor.a = currentAlpha;
        lightImage.color = currentColor;

        // If the UI element reaches the random target, pick a new target
        if (Vector2.Distance(currentPosition, randomTargetPosition) < 1f)
        {
            SetRandomTargetPosition();
        }

        // If the alpha is near the target, pick a new alpha
        if (Mathf.Abs(currentAlpha - targetAlpha) < 0.01f)
        {
            targetAlpha = Random.Range(0.2f, 1f);
        }
    }

    /// <summary>
    /// Sets a random target position within the canvas.
    /// </summary>
    private void SetRandomTargetPosition()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float xMin = canvasRect.rect.xMin;
        float xMax = canvasRect.rect.xMax;
        float yMin = canvasRect.rect.yMin;
        float yMax = canvasRect.rect.yMax;

        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);

        randomTargetPosition = new Vector2(randomX, randomY);
    }
}
