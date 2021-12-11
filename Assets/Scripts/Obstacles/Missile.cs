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
    [SerializeField] [Range(0, 0.5f)] float _alignThreshold = 0.2f;
    [SerializeField] [Range(0, 5)] float _aligningTime = 3;
    [SerializeField] [Range(0, 5)] float _timeAfterAlign = 1;

    [SerializeField] [Range(0, 10)] float _alignSpeed = 1;
    [SerializeField] [Range(1, 25)] float _fireSpeed = 10;

    [Space(5)] 
    [SerializeField] [Range(1, 3)] float _scaleFactor = 1.5f;
    [SerializeField] [Range(0, 5)] float _signalTime = 1;


    Health _target;

    float _aligningElapsedTime = 0;

    bool _isAligning;
    bool _go;

    SoundSettingManager sound;

    private void Awake()
    {
        var removable = GetComponent<Removable>();
        removable.AddRemoveListener(Remove);

        _target = FindObjectOfType<Health>();
        sound = FindObjectOfType<SoundSettingManager>();
}

    private void Start()
    {
        SetOnBounds();

        _isAligning = true;

        transform.parent = null;    

        sound.PlayKnifeAlert();
    }

    void Remove()
    {
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


            if (Mathf.Abs(transform.position.y - _target.transform.position.y) > _alignThreshold)
            {
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

        }
        else
        {
            _isAligning = false;

            StartCoroutine(FireCountdown());
        }
    }

    IEnumerator FireCountdown()
    {

        Vector3 startingScale = _warningSignal.transform.localScale;
        Vector3 targetScale = _warningSignal.transform.localScale * _scaleFactor;

        float elapsedTime = 0;

        while (elapsedTime < _signalTime)
        {
            _warningSignal.transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / _signalTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        _warningSignal.enabled = false;
        sound.PlayKnifeThrow();
        _go = true;

    }

    
}
