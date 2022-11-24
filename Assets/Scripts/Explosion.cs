using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float duration = 1f;
    private Vector3 startScale = new Vector3(1f, 1f, 1f);
    private Vector2 finalScale = new Vector3(2f, 2f, 2f);

    private float time;

    private void Start()
    {
        time = 0f;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(startScale, finalScale, time / duration);
        time += Time.deltaTime;

        if (time > duration)
        {
            Destroy(gameObject);
        }
    }
}
