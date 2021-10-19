using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{

    public int MaxHealthPoints;
    public int healthPoints { get; private set; }


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

    }




}
