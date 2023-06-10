using UnityEngine;

public class Sunroof : MonoBehaviour
{
    [SerializeField] private Button button;

    private BoxCollider2D box;
    private float width;
    private Vector3 lockedPosition;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        width = box.size.x;
        lockedPosition = transform.position;
    }
    private void FixedUpdate()
    {
        if (button.IsPressed()) Open();
        else Close();
    }

    private void Open()
    {
        if (transform.position.x > lockedPosition.x - 2 * width)
            transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right, Time.deltaTime);
    }

    private void Close()
    {
        if (transform.position.x < lockedPosition.x)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, Time.deltaTime);
    }
}
