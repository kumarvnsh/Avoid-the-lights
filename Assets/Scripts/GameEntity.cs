using UnityEngine;

/// <summary>
/// An abstract class representing a generic game entity.
/// </summary>
public abstract class GameEntity : MonoBehaviour
{
    /// <summary>
    /// An abstract method for updating the entity's state.
/// Derived classes must implement their own logic.
    /// </summary>
    public abstract void UpdateState();
}
