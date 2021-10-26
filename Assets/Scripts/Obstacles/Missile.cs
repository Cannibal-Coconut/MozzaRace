using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Obstacle))]
[RequireComponent(typeof(Removable))]
[RequireComponent(typeof(Collider2D))]
public class Missile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer _warningSignal;

    [Header("Settings")]
    [SerializeField] [Range(0, 5)] float _aligningTime = 3;
    [SerializeField] [Range(0, 5)] float _timeAfterAlign = 1;

    [SerializeField] [Range(0, 10)] float _alignSpeed = 1;
    [SerializeField] [Range(1, 10)] float _fireSpeed = 10;

    Health _target;

    float _aligningElapsedTime = 0;

    bool _isAligning;
    bool _go;

    private void Awake()
    {
        var removable = GetComponent<Removable>();
        removable.AddRemoveListener(Remove);

        _target = FindObjectOfType<Health>();

        SetOnBounds();

        _isAligning = true;
    }

    void Remove() {
        Destroy(_warningSignal);
    }

    void SetOnBounds()
    {
        var bounds = FindObjectOfType<ProjectileBounds>();

        var newPosition = bounds.GetRandomPosition();
        transform.position = newPosition;

        newPosition.x = bounds.signalX;
        _warningSignal.transform.position = newPosition;
    }

    private void FixedUpdate()
    {
        if (_isAligning)
        {
            Align();
        }
        else if (_go)
        {
            MoveMissile();
        }
    }

    void MoveMissile()
    {
        transform.position -= new Vector3(_fireSpeed * Time.fixedDeltaTime, 0, 0);
    }

    void Align()
    {
        if (_aligningElapsedTime < _aligningTime)
        {
            _aligningElapsedTime += Time.fixedDeltaTime;

            Vector3 move = new Vector3(0, _alignSpeed * Time.fixedDeltaTime, 0);
            if (transform.position.y > _target.transform.position.y)
            {
                transform.position -= move;
            }
            else
            {
                transform.position += move;
            }
        }
        else
        {
            _isAligning = false;

            StartCoroutine(FireCountdown());
        }
    }

    IEnumerator FireCountdown()
    {
        yield return new WaitForSeconds(_timeAfterAlign);
        _warningSignal.enabled = false;

        _go = true;
    }

}
