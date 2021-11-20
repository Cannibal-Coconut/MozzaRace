using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMission : Mission
{
    float _elapsedTime;
    int _targetPoints = 100;

    bool _done;

    IngredientInventory _inventory;

    public PointMission(int targetPoints)
    {
        _targetPoints = targetPoints;

       _inventory = GameObject.FindObjectOfType<IngredientInventory>();

        _inventory.AddFinishedOrderListener(OnOrderFinishedCallback);
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

    void OnOrderFinishedCallback(int doneOrders)
    {
        /* if (_profileInventory.points >= _targetPoints)
        {
            _done = true;
        }*/
    }
}
