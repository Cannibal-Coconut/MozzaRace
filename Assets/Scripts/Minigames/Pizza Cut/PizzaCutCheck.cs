using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCutCheck : MonoBehaviour
{
    private ChangePizza changePizza;

    private List<ColliderLine> _colliderLines = new List<ColliderLine>();

    private void Start()
    {
        changePizza = GameObject.FindWithTag("GameManager").GetComponent<ChangePizza>();
        foreach (Transform colliderLineTransform in transform)
        {
            var colliderLine = colliderLineTransform.GetComponent<ColliderLine>();
            colliderLine.SetPizza(this);
            _colliderLines.Add(colliderLine);
        }
    }

    private void Update()
    {
        CheckCut();
    }

    private void CheckCut()
    {
        var checkCorrectList = new List<bool>();
        foreach (var colliderLine in _colliderLines)
        {
            switch (colliderLine.cutState)
            {
                case ColliderLine.CutState.NotMade:
                    checkCorrectList.Add(false);
                    break;
                case ColliderLine.CutState.Correct:
                    checkCorrectList.Add(true);
                    break;
            }
        }

        if (checkCorrectList.Contains(false)) return;

        PizzaCorrect();
    }

    public void PizzaFailed()
    {
        changePizza.ChangeVisiblePizza(this, false);
    }

    private void PizzaCorrect()
    {
        changePizza.ChangeVisiblePizza(this, true);
    }
}