using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{

    public int MaxHealthPoints;
    public int healthPoints { get; private set; }

    private Action _onDead;
    private Action _onLive;

    private bool _isAlive;

    DeathScreen _deadScreen;

    private void Awake()
    {
        _deadScreen = FindObjectOfType<DeathScreen>();


    }

    public bool GetAlive(){

        return _isAlive;

    }
    public void Start()
    {
       Live();
    }

    public void Live()
    {   
        _isAlive = true;
        healthPoints = MaxHealthPoints;

        if (_onLive != null)
        {
            _onLive.Invoke();

        }
    }

     void Dead()
    {
        if (_onDead != null)
        {   
            _isAlive = false;
            _onDead.Invoke();
        }

    }

    public void HurtPlayer(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            healthPoints = 0;

            Dead();
        }
    }

   

    public void AddLiveListener(Action action)
    {
        _onLive += action;
    }

    public void AddDeadListener(Action action)
    {
        _onDead += action;
    }

}
