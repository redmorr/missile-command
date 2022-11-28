using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersInput : MonoBehaviour
{
    [SerializeField] private Transform MaxTargetHeight;
    private InputActions inputActions;

    private ICommandAttacks commander;

    private void Awake()
    {
        commander = GetComponent<ICommandAttacks>();
        inputActions = new InputActions();
        inputActions.Player.Fire.performed += ChooseTarget;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void ChooseTarget(InputAction.CallbackContext _)
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>());
        target.y = Mathf.Max(target.y, MaxTargetHeight.position.y);
        commander.OrderAttack(target);
    }
}
