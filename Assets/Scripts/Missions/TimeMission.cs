using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMission : Mission
{
    MissionWatcher _watcher;

    float _elapsedTime;
    float _targetTime = 100;

    public TimeMission(MissionWatcher watcher, float targetTime)
    {
        _watcher = watcher;
        _targetTime = targetTime;
    }

    public override bool CheckMission()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _targetTime)
        {
            return true;
        }

        return false;
    }

    public void SetTargetTime(float time)
    {
        _targetTime = time;
    }

    public override void EndGame()
    {

    }

    public override void StartGame()
    {

    }
}
