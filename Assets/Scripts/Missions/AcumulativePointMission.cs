using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcumulativePointMission : Mission
{

    int _acumulatedPoints = 0;
    int _targetPoints = 100;

    bool _done;

    public AcumulativePointMission(int targetPoints)
    {
        _targetPoints = targetPoints;

        var inventory = GameObject.FindObjectOfType<IngredientInventory>();

        inventory.AddFinishedOrderListener(OnOrderFinishedCallback);
    }

    public override bool CheckMission()
    {
        return _done;
    }


    public override void EndGame()
    {

    }

    public override void StartGame()
    {

    }

    void OnOrderFinishedCallback(int points)
    {
        _acumulatedPoints += points;
        if (points <= _acumulatedPoints)
        {
            _done = true;
        }
    }
}
