using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileShooter : MonoBehaviour
{
    [SerializeField] private Transform WeaponPivot;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Missile MissilePrefab;
    [SerializeField] private float MissileSpeed = 5f;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Fire.performed += FireMissile;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>());

        Vector2 directionToMouse = mousePosition - transform.position;

        float angle = Vector2.SignedAngle(Vector2.up, directionToMouse);
        WeaponPivot.eulerAngles = new Vector3(0, 0, angle);
    }

    private void FireMissile(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>());
        Missile missile = Instantiate(MissilePrefab, SpawnPoint.position, SpawnPoint.rotation);
        missile.Setup(SpawnPoint.position, mousePosition, MissileSpeed);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
