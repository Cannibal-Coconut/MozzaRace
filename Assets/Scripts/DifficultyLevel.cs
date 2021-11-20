using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DifficultyLevel
{
    [Tooltip("Just an ID. It doesnt do anything")]
    public string name;

    [Header("Settings")]
    [Tooltip("Number of orders needed for this level")]
    public int finishedOrders;
    public float speedFactor;
    public int reviveCost;

    [Header("Inventory Settings")]
    public InventorySettings inventorySettings;

    [Header("Spawn Settings")]
    public SpawnSettings spawnSettings;

    public UnityEvent events;
}