using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mission
{
    public abstract void StartGame();
    public abstract void EndGame();
    public abstract bool CheckMission();
}
