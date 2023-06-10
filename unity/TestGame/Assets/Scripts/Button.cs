using UnityEngine;

public class Button : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool isPressed => IsPressed();

    private void FixedUpdate()
    {
        sprite.color = isPressed ? Color.red : Color.white;
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public bool IsPressed()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        return collider.Length > 2;
    }
}
