using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCount : MonoBehaviour
{
    private HashSet<Collider2D> _colliderSet = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        _colliderSet.Add(other);
    }

    public int Collisions()
    {
        return _colliderSet.Count;
    }
}
