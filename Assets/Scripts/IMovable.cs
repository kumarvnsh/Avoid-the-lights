using UnityEngine;

/// <summary>
/// IMovable interface for movement handling.
/// Classes that implement this interface
/// should define how they move in a 2D space.
/// </summary>
public interface IMovable
{
    /// <summary>
    /// Move the object in the specified direction.
    /// </summary>
    void Move(Vector2 direction);
}
