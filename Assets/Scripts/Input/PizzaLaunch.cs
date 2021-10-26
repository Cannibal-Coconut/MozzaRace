using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaLaunch : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerAttack playerAttack;

    private Rigidbody2D _rigidbody2D;

    private State _state;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _state = State.Recalling;
    }

    enum State
    {
        WithPlayer,
        Thrown,
        Recalling
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Recalling:
                Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
                _rigidbody2D.velocity = dirToPlayer * playerAttack.RecallSpeed;

                if (Vector3.Distance(transform.position, playerTransform.position) < playerAttack.grabPizzaRadius)
                {
                    _state = State.WithPlayer;
                    _rigidbody2D.velocity = Vector2.zero;
                    _rigidbody2D.isKinematic = true;
                }
                    
                break;
        }
    }

    private void LateUpdate()
    {
        switch (_state)
        {
            case State.WithPlayer:
                transform.position = playerTransform.position;
                break;
        }
    }

    public void ThrowPizza(Vector3 throwDirection)
    {
        _rigidbody2D.isKinematic = false;
        transform.position = playerTransform.position;
        var force = playerAttack.Power;
        _rigidbody2D.AddForce(throwDirection * force, ForceMode2D.Impulse);
        
        
        _state = State.Thrown;
        
    }

    public void RecallPizza()
    {
        _state = State.Recalling;
    }

    public bool IsWithPlayer()
    {
        return _state == State.WithPlayer;
    }
}