using UnityEngine;

public enum PowerUpType { Heal, SpeedBoost }

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType effect;
    [SerializeField] private int value = 20;
    [SerializeField] private float duration = 5f;

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
            case PowerUpType.Heal:
                player.Heal(value);
                break;
            case PowerUpType.SpeedBoost:
                player.BoostSpeed(value, duration);
                break;
        }
    }
}
