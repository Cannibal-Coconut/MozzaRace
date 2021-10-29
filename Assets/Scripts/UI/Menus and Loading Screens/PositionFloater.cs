using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFloater : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float freq;

    private Vector3 _thisPosition;
    private Vector3 _temporalPosition;

    private void Start() {
        _thisPosition = this.transform.position;
    }

    private void Update() {
        _temporalPosition = _thisPosition;
        _temporalPosition.y += Mathf.Sin(Time.fixedUnscaledTime * Mathf.PI * freq) * amplitude;
        transform.position = new Vector3(transform.position.x, -_temporalPosition.y, transform.position.z);

    }

}
