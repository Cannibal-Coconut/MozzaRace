using System;
using UnityEngine;

[Serializable]
public class PointMission : Mission
{
    [Header("Settings")]
    [SerializeField] int _targetPoints = 100;

    bool _done;

    ProfileInventory _inventory;

    public override bool CheckMission()
    {
        return _done;
    }


    public override void EndGame()
    {

    }

    public override string GetPercentage()
    {
        return "";
    }

    public override void Initialize()
    {
        _inventory = GameObject.FindObjectOfType<ProfileInventory>();

        _inventory.AddOnEconomyChangeListener(OnPointsChangeCallback);

    }

    public override void StartGame()
    {

    }

    void OnPointsChangeCallback()
    {
        if (_inventory.matchPoints >= _targetPoints)
        {
            _done = true;
        }
    }
}
