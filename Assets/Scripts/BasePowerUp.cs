using UnityEngine;

/// <summary>
/// An abstract base class for any power-up. 
/// New power-ups should inherit from this class.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class BasePowerUp : MonoBehaviour
{
    /// <summary>
    /// Apply the effect of this power-up to the given player.
    /// </summary>
    public abstract void ApplyEffect(Player player);

    /// <summary>
    /// Detects trigger collisions with the player, calls ApplyEffect, and destroys the power-up.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to a player
        if (other.CompareTag("Player"))
        {
            // Try to get the IPowerUpReceiver interface from the player
            var receiver = other.GetComponent<IPowerUpReceiver>();
            if (receiver != null)
            {
                // Apply the effect via the IPowerUpReceiver
                receiver.ApplyPowerUp(this);
                Destroy(gameObject);
            }
        }
    }
}
