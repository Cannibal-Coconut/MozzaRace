using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    [SerializeField] private int _coinValue;
    ProfileInventory _coinInventory;

    private void Start() {
        _coinInventory = FindObjectOfType<ProfileInventory>();
    }

    public void AddPointsPerCoin(){
        
        _coinInventory.AddMatchPoints(_coinValue);  
        Destroy(this.gameObject);

    }

    public int GetCoinValue(){

        return _coinValue;

    }
    
}
