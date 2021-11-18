using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Roller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0, 10)] float _speed;
    [SerializeField] [Range(0, 20)] float _flippedSpeed;
    [SerializeField] [Range(0, 5)] float _bounceHeight;

    Rigidbody2D _rigidbody;
    bool _flipped;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //h*m*g = 1/2*m*(v^2) => v = (2*g*h)^0.5; _bounceHeight is h, just calculate speed for such height
        float verticalSpeed = Mathf.Pow(Mathf.Abs(2 * Physics2D.gravity.y * _bounceHeight), 0.5f);

        var desiredVelocity = _rigidbody.velocity;
        desiredVelocity.y = verticalSpeed;
        _rigidbody.velocity = desiredVelocity;

        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (_flipped)
        {
            _rigidbody.velocity = new Vector2(_flippedSpeed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PizzaLaunch>())
        {
            _flipped = true;
        }
    }

}
