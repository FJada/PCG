using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{

    public float speed = 3.0f;
    public float jumpForce = 5.0f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;


    private int direction = 0;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.velocity = new Vector2(-speed, _rigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }

        if (_rigidbody.velocity.x < 0) direction = 1;
        if (_rigidbody.velocity.x > 0) direction = 0;
        _spriteRenderer.flipX = (direction == 1);

    }
}
