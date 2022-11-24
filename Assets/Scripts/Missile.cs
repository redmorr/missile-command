using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeReference] private float DistanceThereshold;
    [SerializeReference] private Explosion Explosion;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float speed;

    public void Setup(Vector2 endPoint, float speed)
    {
        this.endPoint = endPoint;
        this.speed = speed;
    }

    private void Update()
    {
        Move();

    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, endPoint) < DistanceThereshold)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        transform.position += (new Vector3(endPoint.x, endPoint.y) - transform.position).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
