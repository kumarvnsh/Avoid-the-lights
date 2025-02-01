using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Player : GameEntity
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int health = 100;
    [SerializeField] private Image healthBarImage;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Camera mainCamera;
    private float initialSpeed;
    private bool isSpeedBoostActive = false;

    private void Start()
    {
        initialSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        UpdateHealthBar();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
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

        position.x = Mathf.Clamp(position.x, -screenBounds.x + 0.5f, screenBounds.x - 0.5f);
        position.y = Mathf.Clamp(position.y, -screenBounds.y + 0.5f, screenBounds.y - 2);

        transform.position = position;
    }

    public override void UpdateState() { }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
        UpdateHealthBar();

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100);
        UpdateHealthBar();
    }

    public void BoostSpeed(float multiplier, float duration)
    {
        if (isSpeedBoostActive) return;

        isSpeedBoostActive = true;
        speed *= multiplier;
        StartCoroutine(ResetSpeedAfterDelay(duration));
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetSpeed();
    }

    private void ResetSpeed()
    {
        speed = initialSpeed;
        isSpeedBoostActive = false;
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = health / 100f;
        }
    }
}
