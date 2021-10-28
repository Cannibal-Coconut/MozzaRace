using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFloater : MonoBehaviour
{
    private Transform _thisTransform;
    private float _f;
    [SerializeField] private float _fMax;
    [SerializeField] private float _fMin;
    private void Start() {
        _thisTransform = this.transform;

    } 

    private void Update() {
        _f = Mathf.PingPong(Time.time*5, _fMax - _fMin) + _fMin;
        _thisTransform.localPosition = new Vector3(_thisTransform.localPosition.x,_thisTransform.localPosition.y + _f,_thisTransform.localPosition.z);
    }

}
