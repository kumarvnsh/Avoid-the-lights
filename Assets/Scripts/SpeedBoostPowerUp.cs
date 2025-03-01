using UnityEngine;
using System.Collections;

/// <summary>
/// A speed boost power-up that temporarily increases
/// the player's speed by a multiplier.
/// </summary>
public class SpeedBoostPowerUp : BasePowerUp
{
    [SerializeField] private float boostMultiplier = 1.5f;
    [SerializeField] private float duration = 5f;

    /// <summary>
    /// Apply the speed boost effect to the player, then revert after a delay.
    /// </summary>
    public override void ApplyEffect(Player player)
    {
        // Start a coroutine on the player's GameObject
        player.StartCoroutine(ApplySpeedBoost(player));
        Destroy(gameObject);
    }

    private IEnumerator ApplySpeedBoost(Player player)
    {
        float originalSpeed = player.Speed;
        player.Speed = originalSpeed * boostMultiplier;

        yield return new WaitForSeconds(duration);

        // Revert speed to original value
        player.Speed = originalSpeed;
    }
}
