using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HitRollMission : Mission
{

    [SerializeField] [Range(0, 15)] int _targetNumber;

    int _hitCounter = 0;


    public override bool CheckMission()
    {
        return _hitCounter >= _targetNumber;
    }


    public override void StartGame()
    {
        _hitCounter = 0;
    }

    public override void EndGame()
    {

    }

    public override void Initialize()
    {
        PizzaLaunch launch = GameObject.FindObjectOfType<PizzaLaunch>();

        launch.onHitRollCallback += AddRollHit;
    }

    public void AddRollHit()
    {
        _hitCounter++;
    }

    public override string GetPercentage()
    {
        return "";
    }
}
