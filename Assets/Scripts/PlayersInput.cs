using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersInput : MonoBehaviour
{
    private InputActions inputActions;

    private ICommander commander;

    private void Awake()
    {
        commander = GetComponent<ICommander>();
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
        commander.OrderAttack(Camera.main.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>()));
    }
}
