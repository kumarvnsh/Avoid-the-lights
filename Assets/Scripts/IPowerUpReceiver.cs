/// <summary>
/// IPowerUpReceiver interface for any object
/// that can receive power-ups.
/// </summary>
public interface IPowerUpReceiver
{
    /// <summary>
    /// Apply a BasePowerUp to this object.
/// </summary>
    void ApplyPowerUp(BasePowerUp powerUp);
}
