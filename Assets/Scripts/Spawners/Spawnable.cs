using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    bool _go = false;
    float _speed = 0;

    private void FixedUpdate()
    {
        if (_go)
        {
            transform.position += new Vector3(_speed, 0, 0) * Time.fixedDeltaTime;
        }
    }
    //QUICK AND DIRTY

    public void Go(float speed)
    {
        _go = true;

        _speed = speed;
    }


}
