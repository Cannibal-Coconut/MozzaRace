using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionWatcher : MonoBehaviour, ILiveListener
{

    Mission[] _missions;
    Mission _selectedMission;

    bool _checkMissions;

    private void Awake()
    {
        //_selectedMission = new TimeMission(this, 15f);
        //_selectedMission = new OrderMission();
        _selectedMission = new PointMission(200);
        //_selectedMission = new AcumulativePointMission(10000);

        SetListeners();

        CheckMissions(false);
    }

    private void Update()
    {
        if (!_checkMissions) return;

        if (_selectedMission.CheckMission())
        {
            Debug.Log("Eureka!");
        }
    }

    public void CheckMissions(bool b)
    {
        _checkMissions = b;
    }

    public void OnLive()
    {
        CheckMissions(true);
    }

    public void OnDead()
    {
        CheckMissions(false);
    }

    public void SetListeners()
    {
        var player = FindObjectOfType<Health>();
        if (player)
        {
            player.AddLiveListener(OnLive);
            player.AddDeadListener(OnDead);
        }

    }
}
