using UnityEngine;

public abstract class GameEntity : MonoBehaviour, IUpdatable
{
    public abstract void UpdateState();
}
