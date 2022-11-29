using UnityEngine;

public class GunBarrelRotator : MonoBehaviour
{
    private readonly float maxShootAngle = 90f;

    [SerializeField] private Transform pivot;

    public void RotateBarrel(Vector3 target)
    {
        Vector2 directionToMouse = target - transform.position;
        float shootAngle = Vector2.SignedAngle(Vector2.up, directionToMouse);
        pivot.eulerAngles = new Vector3(0, 0, Mathf.Clamp(shootAngle, -maxShootAngle, maxShootAngle));
    }
}
