using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private float _jumpForce;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //_rigitbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        if (collision.relativeVelocity.y < 0)
        {
            PlayerMovement.instance._rigidbody.velocity = new Vector2(PlayerMovement.instance._rigidbody.velocity.x, _jumpForce);
        }
    }
}
