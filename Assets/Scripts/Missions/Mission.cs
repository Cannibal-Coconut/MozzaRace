using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Mission
{
    [Header("Mission Settings")]
    [SerializeField] public string missionNameEnglish;
    [SerializeField] public string missionNameSpanish;
    [SerializeField] public string missionDescriptionEnglish;
    [SerializeField] public string missionDescriptionSpanish;
    [SerializeField] public int points;
    [SerializeField] public int skipPrice;
    [SerializeField] public Sprite icon;

    public abstract void Initialize();
    public abstract void StartGame();
    public abstract void EndGame();
    public abstract bool CheckMission();
}
