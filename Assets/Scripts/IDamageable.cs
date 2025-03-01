/// <summary>
/// IDamageable interface for any object that can take damage.
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Apply damage to the object.
/// Negative damage effectively heals.
    /// </summary>
    void TakeDamage(int damage);
}
