using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColliderLine : MonoBehaviour
{
    public enum CutState
    {
        NotMade,
        Correct
    }

    private Collider2D _cutterCollider;

    public CutState cutState;

    private PizzaCutCheck _pizzaCutCheck;

    private void Awake()
    {
        _cutterCollider = null;
        cutState = CutState.NotMade;
        foreach (Transform checkpointTransform in transform)
        {
            var checkPoint = checkpointTransform.GetComponent<CheckPoint>();
            checkPoint.setColliderLine(this);
        }
    }

    public void SetPizza(PizzaCutCheck pizzaCutCheck)
    {
        _pizzaCutCheck = pizzaCutCheck;
    }

    public void CutterCutPoint(ref Collider2D cutterCollider)
    {
        if (_cutterCollider == null)
        {
            _cutterCollider = cutterCollider;
            StartCoroutine(WaitForSecondCollision());
        }

        else if (_cutterCollider.Equals(cutterCollider))
            cutState = CutState.Correct;
        else
            _pizzaCutCheck.PizzaFailed();
    }

    private IEnumerator WaitForSecondCollision()
    {
        yield return new WaitForSeconds(0.6f);

        if (cutState != CutState.Correct)
            _pizzaCutCheck.PizzaFailed();
    }
    
}