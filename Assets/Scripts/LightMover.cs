using UnityEngine;
using UnityEngine.UI;

public class LightMover : MonoBehaviour
{
    private RectTransform lightElement; // The RectTransform of the UI element
    public Canvas canvas;              // Assign the canvas to keep movement within bounds
    public float moveSpeed = 100f;     // Speed of movement
    public float alphaChangeSpeed = 2f; // Speed of alpha change

    private Vector2 randomTargetPosition;
    private float currentAlpha = 1f;
    private float targetAlpha;
    private Image lightImage;

    private void Start()
    {
        lightElement = GetComponent<RectTransform>();

        if (lightElement == null || canvas == null)
        {
            Debug.LogError("UI Element or Canvas is not assigned properly!");
            return;
        }

        // Initialize random position and alpha
        SetRandomTargetPosition();
        targetAlpha = Random.Range(0.2f, 1f);

        lightImage = GetComponent<Image>();
        if (lightImage == null)
        {
            Debug.LogError("The UI Element does not have an Image component!");
        }
    }

    private void Update()
    {
        if (lightElement == null || canvas == null || lightImage == null) return;

        // Move towards the random target position
        Vector2 currentPosition = lightElement.anchoredPosition;
        lightElement.anchoredPosition = Vector2.MoveTowards(currentPosition, randomTargetPosition, moveSpeed * Time.deltaTime);

        // Change alpha towards the target alpha
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, alphaChangeSpeed * Time.deltaTime);
        Color currentColor = lightImage.color;
        currentColor.a = currentAlpha;
        lightImage.color = currentColor;

        // If the UI element reaches the random target position, set a new target
        if (Vector2.Distance(currentPosition, randomTargetPosition) < 1f)
        {
            SetRandomTargetPosition();
        }

        // If the current alpha reaches the target alpha, set a new random target alph
        if (Mathf.Abs(currentAlpha - targetAlpha) < 0.01f)
        {
            targetAlpha = Random.Range(0.2f, 1f);
        }
    }

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
