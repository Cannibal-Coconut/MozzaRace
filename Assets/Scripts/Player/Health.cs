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

    public Action onDead;
    public Action onLive;


    DeadScreen _deadScreen;

    private void Awake()
    {
        _deadScreen = FindObjectOfType<DeadScreen>();


    }

    public void Start()
    {
        Live();
    }

    public void Live()
    {
        healthPoints = MaxHealthPoints;

        if (onLive != null)
        {
            onLive.Invoke();

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

    void Dead()
    {
        if (onDead != null)
        {
            onDead.Invoke();
        }

    }

}
