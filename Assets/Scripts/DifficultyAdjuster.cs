using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyAdjuster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] DifficultyLevel[] _levels;


    DifficultyLevel _currentLevel;
    IngredientInventory _inventory;
    Spawner _spawner;


    // Start is called before the first frame update
    void Awake()
    {
        _inventory = FindObjectOfType<IngredientInventory>();
        _inventory.AddFinishedOrderListener(OnOrderFinished);

        _spawner = FindObjectOfType<Spawner>();
        

    }

    void OnOrderFinished(int finishedOrders)
    {
        
        DifficultyLevel level = null;
        for (int i = 0; i < _levels.Length; i++)
        {
            if (_levels[i].finishedOrders <= finishedOrders)
            {
                level = _levels[i];
            }
            else
            {
                break;
            }
        }

        SetDifficulty(level);

    }

    void SetDifficulty(DifficultyLevel level)
    {
        if (level == null) return;

        _currentLevel = level;

        _currentLevel.events.Invoke();

        _inventory.SetSettings(_currentLevel.inventorySettings);
        _spawner.SetListWeights(_currentLevel.spawnSettings);

        Debug.Log("Starting Level: "+ _currentLevel.name);

    }

    [System.Serializable]
    class DifficultyLevel
    {
        [Tooltip("Just an ID. It doesnt do anything")]
        public string name;

        [Header("Settings")]
        [Tooltip("Number of orders needed for this level")]
        public int finishedOrders;
        public float speedFactor;

        [Header("Inventory Settings")]
        public InventorySettings inventorySettings;

        [Header("Spawn Settings")]
        public SpawnSettings spawnSettings;

        public UnityEvent events;
    }

    [ContextMenu("Sort Levels")]
    void SortLevels()
    {
        Array.Sort(_levels, (a, b) => a.finishedOrders.CompareTo(b.finishedOrders));
    }
}
