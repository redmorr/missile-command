using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public UnityAction<Spawner> OnBeingDestroyed;

    public ICommandSpawns Commander { get; set; }
    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
    }

    public void Spawn()
    {
        launcher.Launch(transform.position, transform.position + Vector3.right * 50f);
    }

    private void OnDisable()
    {
        OnBeingDestroyed?.Invoke(this);
    }
}
