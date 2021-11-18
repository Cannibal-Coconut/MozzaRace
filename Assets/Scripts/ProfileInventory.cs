using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInventory : MonoBehaviour
{
    public int premiumCoins { get; private set; }
    public int skinPoints { get; private set; }

    static ProfileInventory _instance;

    public List<Skin> skins;

    Action _onEconomyChange;

    private void Awake()
    {
        //Make Sure there is only one of these.
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            skins = new List<Skin>();

            var whiteSkin = new Skin(Color.white, 0);
            whiteSkin.purchased = true;

            skins.Add(whiteSkin);

            _instance = this;
            //DontDestroyOnLoad(this);

            LoadPremiumCoins();
            LoadSkinPoints();

        }
    }

    void LoadPremiumCoins()
    {
        premiumCoins = 100;


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    void LoadSkinPoints()
    {
        skinPoints = 12350;


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void AddOnEconomyChangeListener(Action action)
    {
        _onEconomyChange += action;
    }

    public void AddPremiumCoins(int value)
    {
        premiumCoins += Mathf.Abs(value);


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void RemovePremiumCoins(int value)
    {
        premiumCoins -= Mathf.Abs(value);

        if (premiumCoins < 0)
        {
            premiumCoins = 0;

        }

        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void AddSkinPoints(int value)
    {
        skinPoints += Mathf.Abs(value);


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }

    public void RemoveSkinPoints(int value)
    {
        skinPoints -= Mathf.Abs(value);

        if (skinPoints < 0)
        {
            skinPoints = 0;

        }


        if (_onEconomyChange != null)
        {
            _onEconomyChange.Invoke();
        }
    }


}
