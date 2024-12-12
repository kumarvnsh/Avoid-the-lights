using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private float speed; // Movement speed of the light source
    [SerializeField] private float avoidanceRadius = 1.5f; // Radius for avoiding overlaps
    private Transform player;
    private bool isFollowingPlayer = false;
    private Transform cacheTransform;
    private void Start()
    {
        // Set random speed between 2 and 4
        speed = Random.Range(2f, 3.5f);

        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cacheTransform = transform;

        // Start following the player after a delay
        Invoke(nameof(StartFollowingPlayer), 3f);
    }

    private void Update()
    {
        if (player != null && isFollowingPlayer)
        {
            MoveTowardPlayerWithAvoidance();
        }
    }

    private void MoveTowardPlayerWithAvoidance()
    {
        Vector3 directionToPlayer = (player.position - cacheTransform.position).normalized;
        Vector3 avoidanceDirection = Vector3.zero;
       

        // Find nearby lights to avoid clustering
        Collider2D[] nearbyLights = Physics2D.OverlapCircleAll(cacheTransform.position, avoidanceRadius);
        foreach (var collider in nearbyLights)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("LightSource"))
            {
                Vector3 directionAwayFromLight = (cacheTransform.position - collider.transform.position).normalized;
                avoidanceDirection += directionAwayFromLight;
            }
        }

        // Combine direction to player with avoidance direction
        Vector3 finalDirection = (directionToPlayer + avoidanceDirection).normalized;

        // Move the light source
        cacheTransform.position += finalDirection * speed * Time.deltaTime;
    }

    private void StartFollowingPlayer()
    {
        isFollowingPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player on contact
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(5); // Adjust damage as needed
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the avoidance radius for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(cacheTransform.position, avoidanceRadius);
    }
}
