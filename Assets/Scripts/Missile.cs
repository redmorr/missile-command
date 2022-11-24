using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Explode))]
public class Missile : MonoBehaviour
{
    [SerializeReference] private Rigidbody2D Rigidbody2D;
    [SerializeReference] private Destructible Destructible;
    [SerializeReference] private float DistanceThereshold; // TODO: Calculate from fixed time step * speed.

    private Vector2 startPoint;
    private Vector2 destinationPoint;
    private Vector2 directionToDestination;
    private float speed;

    public void Setup(Vector2 startPoint, Vector2 destinationPoint, float speed)
    {
        this.startPoint = startPoint;
        this.destinationPoint = destinationPoint;
        this.speed = speed;

        directionToDestination = (destinationPoint - startPoint).normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, destinationPoint) < DistanceThereshold)
            Destructible.Die();

        Rigidbody2D.MovePosition(new Vector2(transform.position.x, transform.position.y) + speed * Time.fixedDeltaTime * directionToDestination);
    }
}
