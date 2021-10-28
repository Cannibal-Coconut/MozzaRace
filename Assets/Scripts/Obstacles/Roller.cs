using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Roller : MonoBehaviour
{

    [SerializeField] [Range(0, 10)] float _speed;
    [SerializeField] [Range(0, 5)] float _bounceHeight;
    Rigidbody2D _rigidbody;

    int _sign = -1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        float verticalSpeed = Mathf.Sqrt(Mathf.Abs(2 * Physics2D.gravity.y * _bounceHeight));

        var desiredVelocity = _rigidbody.velocity;
        desiredVelocity.y = verticalSpeed;
        _rigidbody.velocity = desiredVelocity;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector2(_speed * _sign, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).collider.GetComponent<PlayerAttack>())
        {
            _sign *= -1;
        }
    }

}
