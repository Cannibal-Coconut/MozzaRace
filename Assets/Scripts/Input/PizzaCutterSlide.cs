using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LaunchTrajectory)), RequireComponent(typeof(CircleCollider2D)),
 RequireComponent(typeof(TrailRenderer)), RequireComponent(typeof(Rigidbody2D))]
public class PizzaCutterSlide : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager; // Reference to PlayerManager in order to get the position of the input

    [SerializeField] private SpriteRenderer cutterRenderer;

    [Range(1, 50)] [SerializeField] private float power; // Power of the launch
    [Range(0, 3)] [SerializeField] private float waitTime; // Recall speed of the pizza

    [Range(0, 1)] [SerializeField]
    private float minimalDistance; // Minimal distance between the start and the end points of the charge

    private CircleCollider2D _cutterCollider;

    private LaunchTrajectory
        _launchTrajectory; // Reference to the script LaunchTrajectory to draw the direction of the launch

    private TrailRenderer _trailRenderer;

    private Rigidbody2D _rigidbody2D;

    private Vector3
        _startPoint,
        _endPoint,
        _currentPoint; // Points that saves the position of the finger during the charge of the attack

    private bool IsCutStarted { get; set; } // Check if the cut has started

    private bool Thrown { get; set; }


    private void Start()
    {
        _launchTrajectory = GetComponent<LaunchTrajectory>();
        _cutterCollider = GetComponent<CircleCollider2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetCurrentPoint();
    }

    public void OnStartCutInput(Vector2 startPoint)
    {
        if (Thrown) return;
        _startPoint = startPoint;
        _rigidbody2D.position = startPoint;
        _rigidbody2D.velocity = Vector2.zero;;
        IsCutStarted = true;
    }

    public void OnEndCutInput(Vector2 endPoint)
    {
        if (!IsCutStarted) return;
        _endPoint = endPoint;
        IsCutStarted = false;
        _launchTrajectory.EraseLine();
        if (IsMinimalDistance() && !Thrown)
        {
            ThrowCutter(Vector3.Normalize(_endPoint - _startPoint));
        }
    }


    private void GetCurrentPoint()
    {
        if (!IsCutStarted) return;

        _currentPoint = playerManager.AttackPosition();

        _launchTrajectory.DrawTrajectory(_startPoint, _currentPoint);
    }

    private bool IsMinimalDistance()
    {
        return (_endPoint - _startPoint).magnitude > minimalDistance;
    }

    private void ThrowCutter(Vector3 throwDirection)
    {
        cutterRenderer.flipX = throwDirection.x < 0;
        
        var force = power;
        _rigidbody2D.AddForce(throwDirection * force, ForceMode2D.Impulse);

        _trailRenderer.enabled = true;
        Thrown = true;
        StartCoroutine(WaitTimeForNewCut());
    }

    private IEnumerator WaitTimeForNewCut()
    {
        yield return new WaitForSeconds(waitTime);
        Thrown = false;
    }
}