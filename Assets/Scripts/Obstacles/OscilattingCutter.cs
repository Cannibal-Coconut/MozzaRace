using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OscilattingCutter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0, 10)] float _speed;
    [SerializeField] [Range(0, 300)] float _spinVelocity;
    [SerializeField] [Range(0, 90)] float _angle;

    bool _spinningRight = true;

    private void Awake()
    {
        transform.localEulerAngles = new Vector3(0,0, 180 + _angle);
    }

    private void FixedUpdate()
    {
        float adjustedSpinVelocity = 0;
        if (_spinningRight)
        {
            if (transform.localEulerAngles.z <= 180 - _angle)
            {
                _spinningRight = false;
            }
            else
            {
                adjustedSpinVelocity = -_spinVelocity;
            }



        }
        else
        {
            if (transform.localEulerAngles.z > 180 + _angle)
            {
                _spinningRight = true;
            }
            else
            {
                adjustedSpinVelocity = _spinVelocity;
            }

        }

        transform.eulerAngles += new Vector3(0, 0, adjustedSpinVelocity * Time.fixedDeltaTime);


    }



}
