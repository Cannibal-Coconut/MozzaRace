using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[Serializable]
public class StartCutInputEvent : UnityEvent<Vector2>
{
}

[Serializable]
public class EndCutInputEvent : UnityEvent<Vector2>
{
}

[DefaultExecutionOrder(-1)]
public class PizzaCutManager : MonoBehaviour
{
    private Controls _controls;
    
    [SerializeField] private StartCutInputEvent startCutInputEvent;
    [SerializeField] private EndCutInputEvent endCutInputEvent;
    
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
        _controls = new Controls();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _controls.Player.AttackContact.started += OnCutStarted;
        _controls.Player.AttackContact.canceled += OnCutEnded;
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

    private void OnCutStarted(InputAction.CallbackContext context)
    {
        if (startCutInputEvent == null) return;

        var startPointWorldCoords = AttackPosition();

        startCutInputEvent.Invoke(startPointWorldCoords);
    }

    private void OnCutEnded(InputAction.CallbackContext context)
    {
        if (endCutInputEvent == null) return;

        var endPointWorldCoords = AttackPosition();

        endCutInputEvent.Invoke(endPointWorldCoords);
    }
    
    public Vector2 AttackPosition()
    {
        return Utils.ScreenToWorld(_camera, _controls.Player.AttackPosition.ReadValue<Vector2>());
    }
}
