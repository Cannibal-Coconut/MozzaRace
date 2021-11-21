using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipJar : MonoBehaviour
{
    TipTime _tipTime;

    private void Start() {
        _tipTime = FindObjectOfType<TipTime>();
    }

    
    public void StartTipTimeSpawn(){
        
        _tipTime.TriggerTipTime();

    }


}
