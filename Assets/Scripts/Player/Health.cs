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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
