using System;
using UnityEngine;

[Serializable]
public class GameCounterMission : Mission
{
    [Header("Settings")]
    [SerializeField] int _targetCount = 15;

    int _counter = 0;

    public override bool CheckMission()
    {
        
        return _counter >= _targetCount;
    }

    public override void EndGame()
    {

    }

    public override void StartGame()
    {
        _counter++;
    }

    public override void Initialize()
    {
        _counter = 0;
    }

    public override string GetPercentage()
    {
        return ((int)(100 * (float)_counter / (float)_targetCount)).ToString() + "%";
    }
}
