using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionWatcher : MonoBehaviour, ILiveListener
{

    [SerializeField] OrderMission[] _orderMissions;
    [SerializeField] PointMission[] _pointMissions;
    [SerializeField] TimeMission[] _timeMissions;

    Mission[] _allMissions
    {
        get
        {
            int arrayLength = _orderMissions.Length + _pointMissions.Length + _timeMissions.Length;
            int acumulativeIndex = 0;
            Mission[] missions = new Mission[arrayLength];

            for (int i = 0; i < _orderMissions.Length; i++)
            {
                missions[acumulativeIndex] = _orderMissions[i];
                acumulativeIndex++;
            }

            for (int i = 0; i < _timeMissions.Length; i++)
            {
                missions[acumulativeIndex] = _timeMissions[i];
                acumulativeIndex++;
            }

            for (int i = 0; i < _pointMissions.Length; i++)
            {
                missions[acumulativeIndex] = _pointMissions[i];
                acumulativeIndex++;
            }

            return missions;
        }
    }

    public List<Mission> selectedMissions { get; private set; }

    [SerializeField] [Range(1, 3)] int _maxSelectedMissions = 1;

    bool _checkMissions;

    MissionWatcher _instance;

    public Action onMissionsChanged;

    private void Awake()
    {
        if (_instance)
        {
            _instance.FillSelectedMissions();

            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            transform.parent = null;
            
            selectedMissions = new List<Mission>();
            FillSelectedMissions();

            SetListeners();

            SetCheckMissions(false);
        }
    }

    public void FillSelectedMissions()
    {
        if (selectedMissions.Count == _maxSelectedMissions) return;

        List<Mission> possibleMissions = new List<Mission>(_allMissions);

        if (possibleMissions.Count < _maxSelectedMissions) return;
        while (selectedMissions.Count < _maxSelectedMissions)
        {
            int index = UnityEngine.Random.Range(0, possibleMissions.Count);

            if (!selectedMissions.Contains(possibleMissions[index]))
            {
                selectedMissions.Add(possibleMissions[index]);
                possibleMissions[index].Initialize();
            }
            else
            {
                possibleMissions.RemoveAt(index);
            }
        }

        if (onMissionsChanged != null)
        {
            onMissionsChanged.Invoke();
        }

    }

    private void Update()
    {
        if (!_checkMissions) return;

        UpdateMissions();

    }

    void UpdateMissions()
    {
        for (int i = 0; i < selectedMissions.Count; i++)
        {
            if (selectedMissions[i].CheckMission())
            {
                Debug.Log(selectedMissions[i].missionNameEnglish + " completed");
                selectedMissions.RemoveAt(i);


                i--;
            }
        }
    }

    public void SetCheckMissions(bool b)
    {
        _checkMissions = b;
    }

    public void SkipMission(Mission mission)
    {
        if (selectedMissions.Contains(mission))
        {
            selectedMissions.Remove(mission);

            FillSelectedMissions();
        }
    }
    public void OnLive()
    {
        SetCheckMissions(true);
        StartGame();

    }

    void StartGame()
    {
        foreach (var mission in selectedMissions)
        {
            mission.StartGame();
        }
    }

    void EndGame()
    {
        foreach (var mission in selectedMissions)
        {
            mission.EndGame();
        }
    }

    public void OnDead()
    {
        SetCheckMissions(false);

        EndGame();
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
