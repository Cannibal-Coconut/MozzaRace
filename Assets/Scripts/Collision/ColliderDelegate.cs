using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Compoment is meant to work as a tool for other scripts. It provides access to Trigger events of this object from the outside through Actions.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ColliderDelegate : MonoBehaviour
{
    ///NOTE: Check if objects have both colliders and at least one of them has a Rigidbody Component!



    /// <summary>
    /// This action will be invoked when this object enters a trigger
    /// </summary>
    public Action<Collider2D> onEnterTriggerAction;

    /// <summary>
    /// This action will be invoked when this object exits a trigger
    /// </summary>
    public Action<Collider2D> onExitTriggerAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onEnterTriggerAction != null)
        {
            onEnterTriggerAction.Invoke(collision);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onExitTriggerAction != null) {
            onExitTriggerAction.Invoke(collision);
        }
    }



}
