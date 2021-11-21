using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CutPizzaMission : Mission
{

    [SerializeField] [Range(0, 15)] int _targetNumber;

    int _cutCounter = 0;


    public override bool CheckMission()
    {
        return _cutCounter >= _targetNumber;
    }


    public override void StartGame()
    {

    }

    public override void EndGame()
    {

    }

    public override void Initialize()
    {
        PizzaLaunch launch = GameObject.FindObjectOfType<PizzaLaunch>();

        launch.onHitRollCallback += AddCut;
    }

    public void AddCut()
    {
        _cutCounter++;
    }

    public override string GetPercentage()
    {
        return ((int)(100 * (float)_cutCounter / (float)_targetNumber)).ToString() + "%";
    }
}
