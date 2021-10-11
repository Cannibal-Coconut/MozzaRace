using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class JumpInputEvent : UnityEvent<float> {}

[Serializable]
public class AttackInputEvent : UnityEvent<float> {}

/**
 * Set the configurations to be able to interact with the player and disable the UI input
 */
public class PlayerManager : MonoBehaviour
{
    private Controls _controls;

    public JumpInputEvent jumpInputEvent;

    private void Awake()
    {
        _controls = new Controls();
    }
    
    private void Start()
    {
        _controls.Player.Jump.performed += OnJumpPerformed;
        _controls.Player.Jump.canceled += OnJumpPerformed;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.UI.Disable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }


    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        var jumped = context.ReadValue<float>();
        jumpInputEvent.Invoke(jumped);
        Debug.Log("PlayerManager: " + jumped);
    }
}
