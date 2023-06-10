using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float interactionZone;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private GameObject closestItem => FindClosestItem();
    private GameObject pickedItem = null;
    private bool isGrounded = false;

    private void FixedUpdate()
    {
        IsGrounded();
        sprite.color = pickedItem != null ? Color.blue : Color.white;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump") )
            Jump();
        if (Input.GetButton("Fire3"))
            PickClosestItem();
        if (Input.GetButtonUp("Fire3"))
            DropItem();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void IsGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        isGrounded = collider.Length > 1;
    }

    private void PickClosestItem()
    {
        if (closestItem != null)
        {
            pickedItem = closestItem;
            closestItem.transform.position = new Vector3(100f, 100f);
        }
    }

    private void DropItem()
    {
        if (pickedItem != null)
        {
            pickedItem.transform.position = transform.position + transform.right * interactionZone;
            pickedItem = closestItem;
        }
    }

    private GameObject FindClosestItem()
    {
        var items = GameObject.FindGameObjectsWithTag("Item");
        GameObject closest = null;
        float distance = interactionZone;
        Vector3 position = transform.position;
        foreach (GameObject item in items)
        {
            Vector3 diff = item.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= distance)
            {
                closest = item;
                distance = curDistance;
            }
        }
        return closest;
    }
}
