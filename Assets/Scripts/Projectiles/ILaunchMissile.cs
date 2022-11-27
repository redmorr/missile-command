using UnityEngine;
using UnityEngine.Events;

public abstract class ILaunchMissile : MonoBehaviour
{
    public UnityAction<ILaunchMissile> OnBeingDestroyed;

    public EnemyCommander EnemyCommander { get; set; }

    public abstract void Launch(Vector3 target);

    public bool CanFire { get => true; }
    
    public Vector3 Position { get => transform.position; }

    private void OnDisable()
    {
        OnBeingDestroyed?.Invoke(this);
    }
}
