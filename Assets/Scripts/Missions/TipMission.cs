using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TipMission : Mission
{
    [Header("Settings")]
    [SerializeField] [Range(1, 10)] int _targetCounter;

    int _counter;

    public override bool CheckMission()
    {
        return _counter >= _targetCounter;
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
        _targetCounter = 0;

        IngredientInventory _inventory = GameObject.FindObjectOfType<IngredientInventory>();
        _inventory.onTakenTipAction += AddCounter;

    }

    public override void StartGame()
    {
        _counter = 0;
    }


    void AddCounter()
    {
        _counter++;
    }

}
