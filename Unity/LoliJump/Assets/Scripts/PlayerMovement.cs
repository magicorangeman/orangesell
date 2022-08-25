using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Settings")]

    [SerializeField] private float _moveSpeed;

    [Header("Components")]

    [SerializeField] public Rigidbody2D _rigidbody;
    [SerializeField] private bool _lookRight;

    private float topScore = 0.0f;
    public Text _scoreText;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if(_rigidbody.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }

        _scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(Input.acceleration.x * _moveSpeed, _rigidbody.velocity.y);

        CheckFlip();
    }

    private void CheckFlip()
    {
        if (Input.acceleration.x > 0 && !_lookRight)
        {
            Flip();
        }
        else if(Input.acceleration.x < 0 && _lookRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        _lookRight = !_lookRight;
    }
}
