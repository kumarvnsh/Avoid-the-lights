using UnityEngine;

/// <summary>
/// A healing power-up that increases the player's health.
/// </summary>
public class HealPowerUp : BasePowerUp
{
    [SerializeField] private int healAmount = 20;

    /// <summary>
    /// Heal the player by applying negative damage.
    /// </summary>
    public override void ApplyEffect(Player player)
    {
        // Negative damage effectively heals
        player.TakeDamage(-healAmount);
        Destroy(gameObject);
    }
}
