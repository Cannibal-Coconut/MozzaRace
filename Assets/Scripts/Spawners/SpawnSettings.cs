using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnSettings
{
    [Tooltip("Sum to easy obstacle tokens")]
    [Range(0, 10)] public int easyObstacleWeight;
    [Tooltip("Sum to easy obstacle tokens")]
    [Range(0, 10)] public int mediumObstacleWeight;
    [Tooltip("Sum to easy obstacle tokens")]
    [Range(0, 10)] public int hardObstacleWeight;
}