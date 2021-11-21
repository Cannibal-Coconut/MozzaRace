using System;
using UnityEngine;

[Serializable]
public class TimeMission : Mission
{
    [Header("Settings")]
    [SerializeField] float _targetTime = 100;
    
    float _elapsedTime;

    public override bool CheckMission()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _targetTime)
        {
            return true;
        }

        return false;
    }

    public override void EndGame()
    {

    }

    public override void StartGame()
    {
        _elapsedTime = 0;
    }

    public override void Initialize()
    {
        _elapsedTime = 0;
    }
}
