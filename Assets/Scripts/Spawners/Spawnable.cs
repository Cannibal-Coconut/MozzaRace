using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Removable))]
public class Spawnable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0, 10f)] float _timeForNextSpawn;

    public float timeForNextSpawn
    {
        get
        {
            return _timeForNextSpawn;
        }
    }

    public bool dontMove;


    bool _go = false;

    float _speed = 0;

    private void FixedUpdate()
    {
        if (_go && !dontMove)
        {
            transform.position += new Vector3(_speed * Time.fixedDeltaTime, 0, 0);
        }
    }
    //QUICK AND DIRTY

    public void Go(float speed)
    {
        _go = true;

        _speed = speed;
    }


}
