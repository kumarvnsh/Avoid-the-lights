using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class Player : GameEntity
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int health = 100;
    [SerializeField] private Image healthBarImage; // Reference to the health bar UI Image

    private Rigidbody2D rb;
    private Vector2 movement;
    private Camera mainCamera;
    private float initialSpeed;
    private bool isSpeedBoostActive = false; // Flag to track speed boost state

    private void Start()
    {
        initialSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // Reference to the main camera
        UpdateHealthBar(); // Initialize health bar
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            GameManager.Instance.GameOver();
            return;
        }
        Move();
        ConstrainToScreen();
    }

    private void HandleInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        rb.velocity = movement * speed;
    }

    private void ConstrainToScreen()
    {
        Vector3 position = transform.position;
        Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Clamp position to the camera's visible area
        position.x = Mathf.Clamp(position.x, -screenBounds.x + 0.5f, screenBounds.x - 0.5f);
        position.y = Mathf.Clamp(position.y, -screenBounds.y + 0.5f, screenBounds.y - 2);

        transform.position = position;
    }

    public override void UpdateState()
    {
        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100); // Prevent health from going below 0
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100); // Prevent health from exceeding max
        UpdateHealthBar();
    }

    public void BoostSpeed(float multiplier, float duration)
    {
        if (isSpeedBoostActive) return; // Do not apply boost if already active

        isSpeedBoostActive = true;
        speed *= multiplier;
        Invoke(nameof(ResetSpeed), duration);
    }

    private void ResetSpeed()
    {
        speed = initialSpeed; // Reset to default speed
        isSpeedBoostActive = false; // Reset the flag
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = health / 100f; // Scale health (0-100) to 0-1 for Image fill
        }
    }
}
