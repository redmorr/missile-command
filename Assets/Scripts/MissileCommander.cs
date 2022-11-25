using UnityEngine;
using UnityEngine.InputSystem;

public class MissileCommander : MonoBehaviour
{
    private ILaunchMissile[] launchers;

    public Vector3 MousePosition { get; private set; }

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Fire.performed += FireMissile;
        launchers = GetComponentsInChildren<ILaunchMissile>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>());
    }

    public void FireMissile(InputAction.CallbackContext context)
    {
        if (GetClosestMissileShooter(out ILaunchMissile launcher))
            launcher.Launch(MousePosition);
    }

    private bool GetClosestMissileShooter(out ILaunchMissile missileShooter)
    {
        missileShooter = null;
        float minDistnace = Mathf.Infinity;

        foreach (ILaunchMissile launcher in launchers)
        {
            float distance = Vector2.Distance(MousePosition, launcher.Position);
            if (launcher.CanFire && distance < minDistnace)
            {
                minDistnace = distance;
                missileShooter = launcher;
            }
        }

        return missileShooter != null;
    }
}
