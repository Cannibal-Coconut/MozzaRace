using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveListener
{
    public void OnLive();

    public void OnDead();

    /// <summary>
    /// Find Health and Add listener. Call this function on Awake.
    /// </summary>
    public void SetListeners();

}
