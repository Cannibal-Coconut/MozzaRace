using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Remover : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;

        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var removable = collision.GetComponent<Removable>();

        if (removable)
        {
            removable.Remove();
        }
    }


}
