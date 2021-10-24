using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInventory : MonoBehaviour
{
    public int premiumCoins { get; private set; }

    static ProfileInventory _instance;

    private void Awake()
    {
        //Make Sure there is only one of these.
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;

            LoadPremiumCoins();
        }
    }

    void LoadPremiumCoins()
    {
        premiumCoins = 0;
    }

}
