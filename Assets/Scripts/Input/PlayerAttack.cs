using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PizzaLaunch _pizzaLaunch; // Reference to the launch of the pizza script

    [SerializeField]
    private PlayerManager _playerManager; // Reference to PlayerManager in order to get the position of the input

    [Range(1, 50)] [SerializeField] private float power;
    [Range(1, 50)] [SerializeField] private float recallSpeed; // Recall speed of the pizza
    [Range(0, 3)] [SerializeField] private float recallWaitTime; // Recall speed of the pizza

    [Range(0, 3)] public float grabPizzaRadius; // Radius of the grab

    public float Power => power;
    public float RecallSpeed => recallSpeed;

    private LaunchTrajectory
        _launchTrajectory; // Reference to the script LaunchTrajectory to draw the direction of the launch

    private Vector3 _startPoint, _endPoint, _currentPoint;

    private bool _isAttackStarted; // Check if the attack has started

    private void Start()
    {
        _launchTrajectory = GetComponent<LaunchTrajectory>();
    }

    private void Update()
    {
        GetCurrentPoint();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabPizzaRadius);
    }

    public void OnStartAttackInput(Vector2 startPoint)
    {
        _startPoint = startPoint;
        _isAttackStarted = true;
    }

    public void OnEndAttackInput(Vector2 endPoint)
    {
        _endPoint = endPoint;
        _isAttackStarted = false;

        _launchTrajectory.EraseLine();

        if (!_pizzaLaunch.IsWithPlayer()) return;
        _pizzaLaunch.ThrowPizza(Vector3.Normalize(_endPoint - _startPoint));
        StartCoroutine(RecallTime(recallWaitTime));
    }

    private void GetCurrentPoint()
    {
        if (!_isAttackStarted) return;

        _currentPoint = _playerManager.AttackPosition();

        _launchTrajectory.DrawTrajectory(_startPoint, _currentPoint);
    }


    private IEnumerator RecallTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _pizzaLaunch.RecallPizza();
    }
}