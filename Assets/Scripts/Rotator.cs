using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform WeaponPivot;

    private readonly float maxShootAngle = 90f;
    private float shootAngle;
    private Launcher launcher;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
    }

    private void OnEnable()
    {
        launcher.OnLaunch += Rotate;
    }

    private void Rotate(Vector3 target)
    {
        Vector2 directionToMouse = target - transform.position;
        shootAngle = Vector2.SignedAngle(Vector2.up, directionToMouse);
        WeaponPivot.eulerAngles = new Vector3(0, 0, Mathf.Clamp(shootAngle, -maxShootAngle, maxShootAngle));
    }

    private void OnDisable()
    {
        launcher.OnLaunch -= Rotate;
    }
}
