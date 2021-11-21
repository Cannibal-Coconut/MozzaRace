using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AcumulativePointMission : Mission
{

    [Header("Settings")]
    [SerializeField] int _targetPoints = 100;
    int _acumulatedPoints = 0;

    public AcumulativePointMission(int targetPoints)
    {
        _targetPoints = targetPoints;

        var inventory = GameObject.FindObjectOfType<ProfileInventory>();

        inventory.AddOnEconomyChangeListener(OnOrderFinishedCallback);
    }

    public override bool CheckMission()
    {
        return _acumulatedPoints >= _targetPoints;
    }


    public override void Initialize()
    {
        _acumulatedPoints = 0;
    }

    public override void StartGame()
    {

    }
    public override void EndGame()
    {

    }

    void OnOrderFinishedCallback()
    {
        _acumulatedPoints += points;
    }

    public override string GetPercentage()
    {
        return ((int)(_acumulatedPoints * 100 / _targetPoints)).ToString() + "%";
    }
}
