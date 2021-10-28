using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeFloater : MonoBehaviour
{

    private Transform _thisTransform;
    private float _f;
    [SerializeField] private float _fMax;
    [SerializeField] private float _fMin;
    private void Start() {
        _thisTransform = this.transform;
        _fMax = 1.3f;
        _fMin = 0.8f;

    } 

    private void Update() {
        _f = Mathf.PingPong(Time.time/2, _fMax - _fMin) + _fMin;
        _thisTransform.localScale = new Vector3(_f,_f,_f);
    }

    
}
