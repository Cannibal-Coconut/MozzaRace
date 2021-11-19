using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private ColliderLine _colliderLine;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _colliderLine.CutterCutPoint(ref other);
        Destroy(gameObject);
    }

    public void setColliderLine(ColliderLine colliderLine)
    {
        _colliderLine = colliderLine;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.2f * 3);
    }
}
