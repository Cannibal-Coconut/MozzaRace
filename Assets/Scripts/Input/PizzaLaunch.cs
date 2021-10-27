using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PizzaLaunch : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerAttack playerAttack;

    
    public delegate void PizzaLaunchEvent();

    public event PizzaLaunchEvent onLaunchPizza;

    public delegate void PizzaReceiveEvent();

    public event PizzaReceiveEvent onReceivePizza;


    private Rigidbody2D _rigidbody2D;

    private State _state;

    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _state = State.Recalling;
        _trailRenderer = GetComponent<TrailRenderer>();
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
                transform.rotation = quaternion.LookRotation(Vector3.back, (Vector3)_rigidbody2D.velocity);


                if (Vector3.Distance(transform.position, playerTransform.position) < playerAttack.grabPizzaRadius)
                {
                    _state = State.WithPlayer;
                    _trailRenderer.enabled = false;
                    _rigidbody2D.velocity = Vector2.zero;
                    _rigidbody2D.isKinematic = true;
                    onReceivePizza();
                }

                break;
            
            case State.Thrown:
                transform.rotation = quaternion.LookRotation(Vector3.forward, (Vector3)_rigidbody2D.velocity);
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
        transform.rotation = Quaternion.LookRotation(Vector3.forward, throwDirection);

        _trailRenderer.enabled = true;
        _state = State.Thrown;
        onLaunchPizza();
  
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