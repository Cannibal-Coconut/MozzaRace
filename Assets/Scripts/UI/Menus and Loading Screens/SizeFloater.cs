using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeFloater : MonoBehaviour
{

    private Transform _thisTransform;
    private float _f;
    [SerializeField] private float _fMax;
    [SerializeField] private float _fMin;
    [SerializeField] private float _timeScaleFactor ;
    private void Start() {
        _thisTransform = this.transform;

        if (_timeScaleFactor == 0) _timeScaleFactor = 2f;
        if (_fMax == 0) _fMax = 1.3f;
        if (_fMin == 0) _fMin = 0.9f;

    } 

    private void Update() {
        _f = Mathf.PingPong(Time.unscaledTime/_timeScaleFactor, _fMax - _fMin) + _fMin;
        _thisTransform.localScale = new Vector3(_f,_f,_f);
    }

    
}
