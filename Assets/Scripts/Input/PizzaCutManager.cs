using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[Serializable]
public class StartCutInputEvent : UnityEvent<Vector2, bool>
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
    private bool _isFirstCut;

    private void Awake()
    {
        _camera = Camera.main;
        _controls = new Controls();
    }

    // Start is called before the first frame update
    void Start()
    {
        _isFirstCut = true;
        _controls.Player.AttackContact.started += OnCutStarted;
        _controls.Player.AttackContact.canceled += OnCutEnded;
    }

    private void OnEnable()
    {
        _isFirstCut = true;
        _controls.Player.Enable();
        _controls.UI.Disable();
    }

    private void OnDisable()
    {
        _isFirstCut = false;
        _controls.Player.Disable();
    }

    private void OnCutStarted(InputAction.CallbackContext context)
    {
        if (startCutInputEvent == null) return;

        var startPointWorldCoords = AttackPosition();


        if (_isFirstCut)
        {
            _isFirstCut = false;
            startCutInputEvent.Invoke(startPointWorldCoords, true);
        }
        else
            startCutInputEvent.Invoke(startPointWorldCoords, false);
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