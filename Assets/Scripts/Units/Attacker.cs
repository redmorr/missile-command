using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    public UnityAction<Attacker> OnBeingDestroyed;
    
    public ICommandAttacks Commander { get; set; }
    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
    }

    public void Launch(Vector3 target)
    {
        launcher.Launch(transform.position, target);
    }

    private void OnDisable()
    {
        OnBeingDestroyed?.Invoke(this);
    }
}
