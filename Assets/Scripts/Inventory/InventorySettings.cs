using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventorySettings
{
    [Header("Settings")]
    [Tooltip("Max count of orders at the same time")]
    [SerializeField] [Range(1, 3)] public int maxOrders;
    [Tooltip("Delay for points going down")]
    [SerializeField] [Range(10, 500)] public int baseOrderPoints;

    [Space(5)]
    [Tooltip("Loose of points for wrong ingredient")]
    [SerializeField] [Range(0, 100)] public int wrongIngredientLoose;
    [Tooltip("Loose of points overtime")]
    [SerializeField] [Range(0, 100)] public int overtimeLoose;    
    [Tooltip("Delay for points going down")]
    [SerializeField] [Range(0.1f, 10)] public float looseDelay;


    [Space(5)]
    [Tooltip("Sum to requested items tokens")]
    [Range(1, 10)] public int requestedItemSpawnBuff;

}

