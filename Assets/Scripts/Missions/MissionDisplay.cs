using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDisplay : MonoBehaviour
{
    [SerializeField] MissionHolder[] _holders;

    MissionWatcher _watcher;


    // Start is called before the first frame update
    void Start()
    {
        _watcher = FindObjectOfType<MissionWatcher>();

        _watcher.onMissionsChanged += DisplayMissions;

        _watcher.onMissionsChanged.Invoke();
    }

    public void DisplayMissions()
    {
        for (int i = 0; i < _holders.Length; i++)
        {
            if (_watcher.selectedMissions.Count > i)
            {
                _holders[i].SetMission(_watcher.selectedMissions[i]);
            }
        }
    }

}
