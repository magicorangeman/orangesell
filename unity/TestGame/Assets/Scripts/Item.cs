using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float bounce;

    private Vector2 lastVelocity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 surfaceNormal = collision.contacts[0].normal;
        rb.velocity = 0.5f * bounce * Vector2.Reflect(lastVelocity, surfaceNormal);
    }
}