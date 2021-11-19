using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCutter : MonoBehaviour
{
    [Range(1f, 3f)] [SerializeField] private float waitTime;
    void Start()
    {
        StartCoroutine(WaitAndDestroyCutter());
    }

    private IEnumerator WaitAndDestroyCutter()
    {
        yield return new WaitForSeconds(waitTime);
        
        Destroy(gameObject);
    }
}
