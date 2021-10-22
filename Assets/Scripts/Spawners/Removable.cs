using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removable : MonoBehaviour
{
    Action _removeAction;

    /// <summary>
    /// Listener just before this gameobject is destroyed
    /// </summary>
    /// <param name="action"></param>
    public void AddRemoveListener(Action action)
    {
        _removeAction += action;
    }

    /// <summary>
    /// Invoke Listeners and destroy.
    /// </summary>
    public void Remove()
    {
        if (_removeAction != null)
            _removeAction.Invoke();

        Destroy(gameObject);
    }


}
