using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string effect; // "Heal" or "SpeedBoost"
    [SerializeField] private int value = 20; // Heal amount or SpeedBoost multiplier
    [SerializeField] private float duration = 5f; // Duration for SpeedBoost

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                ApplyEffect(player);
                Destroy(gameObject);
            }
        }
    }

    private void ApplyEffect(Player player)
    {
        switch (effect)
        {
            case "Heal":
                player.Heal(value);
                break;
            case "SpeedBoost":
                player.BoostSpeed(value, duration);
                break;
            default:
                Debug.LogWarning($"Effect {effect} not recognized!");
                break;
        }
    }
}
