using UnityEngine;
using System.Collections;

/// <summary>
/// The Player class manages the player character.
/// It implements IMovable, IDamageable, and IPowerUpReceiver
/// to separate responsibilities.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : GameEntity, IMovable, IDamageable, IPowerUpReceiver
{
    [Header("Player Configuration")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int health = 100;

    [Header("UI References")]
    [SerializeField] private PlayerUI playerUI;

    [Header("Canvas Constraint")]
    [SerializeField] private Canvas gameCanvas; 
    private RectTransform canvasRect;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    private Rigidbody2D rb;
    private Vector2 movement;
    private int maxHealth = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Initialize UI display with current health
        if (playerUI != null)
        {
            playerUI.UpdateHealthBar(health);
        }

        if (gameCanvas != null)
        {
            canvasRect = gameCanvas.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        // Capture input for horizontal and vertical axes
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // Actual movement in physics step
        Move(movement);
        ConstrainToScreen();
    }

    /// <summary>
    /// Implementation of IMovable interface. Moves the player using a Rigidbody2D.
    /// </summary>
    public void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private void ConstrainToScreen()
{
    // Grab the player's current position
    Vector3 position = transform.position;
    Camera mainCamera = Camera.main;

    // Convert screen coordinates to world coordinates 
    // at the max screen width/height
    Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    // Clamp the player's X within the screen edges (with a small buffer)
    position.x = Mathf.Clamp(position.x, -screenBounds.x + 0.5f, screenBounds.x - 0.5f);

    // Clamp the player's Y within the screen edges (with a small buffer)
    position.y = Mathf.Clamp(position.y, -screenBounds.y + 0.5f, screenBounds.y - 2f);

    // Reassign the clamped position back to the player
    transform.position = position;
}


    /// <summary>
    /// Implementation of IDamageable interface. Reduces health.
    /// Negative damage effectively heals the player.
    /// </summary>
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (playerUI != null)
        {
            playerUI.UpdateHealthBar(health);
        }

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// Implementation of IPowerUpReceiver interface. 
    /// Applies a power-up to this player instance.
    /// </summary>
    public void ApplyPowerUp(BasePowerUp powerUp)
    {
        powerUp.ApplyEffect(this);
    }

    /// <summary>
    /// The UpdateState method from GameEntity.
    /// Not used directly here, but required as an abstract method.
    /// </summary>
    public override void UpdateState()
    {
        // Could handle any extra, per-frame state updates if needed.
    }
}
