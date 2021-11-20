using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipTime : MonoBehaviour
{
    
    private const int TIP_TIME_DURATION = 10;
    private const int TIP_TIME_POINT_THRESHHOLD = 500;
    private ProfileInventory _inventoryPoints;
    private int _currentPoints; 
     private void Start() {

        _inventoryPoints = FindObjectOfType<ProfileInventory>();
        

    }

    private void Update() {


        _currentPoints = _inventoryPoints.points; 
        if(_currentPoints % 500 == 0){
    
            TriggerTipTime();
        
        }


    }

    private void TriggerTipTime(){

        Debug.Log("Tip Time!");

    }
    

    public int GetTipTime(){

        return TIP_TIME_DURATION;

    }


}
