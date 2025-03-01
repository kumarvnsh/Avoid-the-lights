using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A separate UI handler for the Player.
/// Responsible for health bar updates and other 
/// player-specific UI elements.
/// </summary>
public class PlayerUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image healthBarImage;

    /// <summary>
    /// Updates the health bar fill based on the player's health.
    /// </summary>
    /// <param name="health">Current health of the player</param>
    public void UpdateHealthBar(int health)
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = health / 100f;
        }
    }
}
