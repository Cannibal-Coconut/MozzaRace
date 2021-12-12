using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LaunchTrajectory))]
public class PizzaCutterSlide : MonoBehaviour
{
    [SerializeField]
    private PizzaCutManager pizzaCutManager; // Reference to PlayerManager in order to get the position of the input

    [SerializeField] private Rigidbody2D cutter;

    [Range(1, 50)] [SerializeField] private float power; // Power of the launch

    [Range(0, 1)] [SerializeField]
    private float minimalDistance; // Minimal distance between the start and the end points of the charge

    private LaunchTrajectory
        _launchTrajectory; // Reference to the script LaunchTrajectory to draw the direction of the launch

    private Vector3
        _startPoint,
        _endPoint,
        _currentPoint; // Points that saves the position of the finger during the charge of the attack

    private bool IsCutStarted { get; set; } // Check if the cut has started

    private bool Thrown { get; set; }
    SoundSettingManager sound;


    private void Start()
    {
        _launchTrajectory = GetComponent<LaunchTrajectory>();
        sound = FindObjectOfType<SoundSettingManager>();
    }

    private void Update()
    {
        GetCurrentPoint();
    }

    public void OnStartCutInput(Vector2 startPoint, bool isFirstCut)
    {
        if (isFirstCut) return;
        _startPoint = startPoint;
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
            sound.PlayPizaTimeCut();
        }
    }


    private void GetCurrentPoint()
    {
        if (!IsCutStarted) return;

        _currentPoint = pizzaCutManager.AttackPosition();

        _launchTrajectory.DrawTrajectory(_startPoint, _currentPoint);
    }

    private bool IsMinimalDistance()
    {
        return (_endPoint - _startPoint).magnitude > minimalDistance;
    }

    private void ThrowCutter(Vector3 throwDirection)
    {
        var force = power;
        Rigidbody2D cutterRigidbody = null;
        InstantiateCutter(ref cutterRigidbody);
        cutterRigidbody.AddForce(throwDirection * force, ForceMode2D.Impulse);
    }

    private void InstantiateCutter(ref Rigidbody2D rigidbody2D)
    {
        rigidbody2D = Instantiate(cutter, cutter.transform.position + _startPoint, Quaternion.identity, transform);
    }
}