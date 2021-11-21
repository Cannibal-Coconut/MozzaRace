using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[Serializable]
public class JumpInputEvent : UnityEvent<float>
{
}

[Serializable]
public class StartAttackInputEvent : UnityEvent<Vector2>
{
}

[Serializable]
public class EndAttackInputEvent : UnityEvent<Vector2>
{
}

[Serializable]
public class ChangeOrderInputEvent : UnityEvent<float>
{
}


/**
 * Set the configurations to be able to interact with the player and disable the UI input
 */
[DefaultExecutionOrder(-1)]
public class PlayerManager : MonoBehaviour
{
    private Controls _controls;

    [SerializeField] private BoxCollider2D jumpButtonCollider;

    [SerializeField] private GameObject playerGameObject;
    
    [SerializeField] private JumpInputEvent jumpInputEvent;
    [SerializeField] private StartAttackInputEvent startAttackInputEvent;
    [SerializeField] private EndAttackInputEvent endAttackInputEvent;
    [SerializeField] private ChangeOrderInputEvent changeOrderInputEvent;

    private Camera _camera;

    

    private void Awake()
    {
        _camera = Camera.main;
        _controls = new Controls();
    }

    private void Start()
    {
        _controls.Player.Jump.performed += OnJumpPerformed;
        _controls.Player.Jump.canceled += OnJumpPerformed;

        _controls.Player.AttackContact.started += OnAttackStarted;
        _controls.Player.AttackContact.canceled += OnAttackEnded;

        _controls.Player.ChangeOrder.performed += OnChangeOrderPerformed;
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
        if (jumpInputEvent == null) return;
        var jumped = context.ReadValue<float>();
        jumpInputEvent.Invoke(jumped);
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (startAttackInputEvent == null) return;
        
        if (EventSystem.current.IsPointerOverGameObject())return;
            //if (!EventSystem.current.currentSelectedGameObject.Equals(playerGameObject)) 


        if (jumpButtonCollider.bounds.Contains(_controls.Player.AttackPosition.ReadValue<Vector2>())) return;

        var startPointWorldCoords = AttackPosition();

        startAttackInputEvent.Invoke(startPointWorldCoords);
    }

    private void OnAttackEnded(InputAction.CallbackContext context)
    {
        if (endAttackInputEvent == null) return;

        var endPointWorldCoords = AttackPosition();

        endAttackInputEvent.Invoke(endPointWorldCoords);
    }

    public Vector2 AttackPosition()
    {
        return Utils.ScreenToWorld(_camera, _controls.Player.AttackPosition.ReadValue<Vector2>());
    }

    private void OnChangeOrderPerformed(InputAction.CallbackContext context)
    {
        if (changeOrderInputEvent == null) return;
        changeOrderInputEvent.Invoke(context.ReadValue<float>());
    }
}