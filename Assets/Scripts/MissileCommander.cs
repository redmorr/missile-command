using UnityEngine;
using UnityEngine.InputSystem;

public class MissileCommander : MonoBehaviour
{
    [SerializeField] private MissileShooter[] missileShooters;

    public Vector3 MousePosition { get; private set; }

    private InputActions inputActions;
    private MissileShooter missileShooter;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Fire.performed += FireMissile;

        foreach (MissileShooter ms in missileShooters)
        {
            if (ms)
                ms.Setup(this);
        }
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
        if (GetClosestNonEmptyMissileShooter(out missileShooter))
            missileShooter.FireMissile();
    }

    private bool GetClosestNonEmptyMissileShooter(out MissileShooter missileShooter)
    {
        missileShooter = null;
        float minDistnace = Mathf.Infinity;

        foreach (MissileShooter ms in missileShooters)
        {
            float distance = Vector2.Distance(MousePosition, ms.transform.position);
            if (ms.Ammo > 0 && distance < minDistnace)
            {
                minDistnace = distance;
                missileShooter = ms;
            }
        }

        return missileShooter != null;
    }
}
