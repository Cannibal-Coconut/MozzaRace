using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBounds : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Transform _upBound;

    [SerializeField]
    Transform _lowBound;

    [Header("Settings")]
    [SerializeField]
    [Range(1, 40)] float _signalOffset;

    [SerializeField]
    [Range(0.1f, 2)]
    float _gizmosRadius;

    public float signalX
    {
        get
        {
            return _upBound.position.x - _signalOffset;
        }
    }


    public Vector3 GetRandomPosition()
    {
        Vector3 position = _upBound.position;

        position.y = Random.Range(_lowBound.position.y, _upBound.position.y);

        return position;
    }



    private void OnDrawGizmos()
    {
        if (_upBound && _lowBound)
        {
            //Bounds
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_upBound.position, _lowBound.position);

            Gizmos.DrawSphere(_upBound.position, _gizmosRadius);
            Gizmos.DrawSphere(_lowBound.position, _gizmosRadius);

            //Signal
            Gizmos.color = Color.yellow;
            Vector3 offset = new Vector3(-_signalOffset, 0, 0);

            Gizmos.DrawLine(_upBound.position + offset, _lowBound.position + offset);

            Gizmos.DrawSphere(_upBound.position + offset, _gizmosRadius);
            Gizmos.DrawSphere(_lowBound.position + offset, _gizmosRadius);

        }
    }

}
