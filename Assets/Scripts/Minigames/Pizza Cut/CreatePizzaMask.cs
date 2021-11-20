using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePizzaMask : MonoBehaviour
{
    [SerializeField] private GameObject mask;
    private void OnTriggerExit2D(Collider2D other)
    {
        var cutterMovementDirection = other.attachedRigidbody.velocity;
        var cutterMovementPosition = other.transform.position + new Vector3(other.offset.x, other.offset.y, 0);
        InstantiateMask(cutterMovementDirection, cutterMovementPosition);
    }

    private void InstantiateMask(Vector3 cutDirection, Vector3 position)
    {
        Instantiate(mask, position, Quaternion.LookRotation(Vector3.forward, cutDirection), transform);
    }
}
