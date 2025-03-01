using UnityEngine;

/// <summary>
/// Controls an enemy light that follows the player after a delay.
/// </summary>
public class LightSource : MonoBehaviour
{
    [Header("Light Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float delayBeforeFollow = 3f;

    private Transform player;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        // Randomize the speed for variety
        speed = Random.Range(2f, 3.5f);

        // Find the player object by tag
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Start following after a specified delay
        Invoke(nameof(StartFollowingPlayer), delayBeforeFollow);
    }

    private void Update()
    {
        if (player != null && isFollowingPlayer)
        {
            // Move directly toward player's position
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }

    private void StartFollowingPlayer()
    {
        isFollowingPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it collided with the player
        if (other.CompareTag("Player"))
        {
            // If so, apply damage to the player
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(5);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // If the light is still colliding with the player, keep applying damage
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
        }
    }
}
