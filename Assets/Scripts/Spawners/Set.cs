using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Removable _removable;



    // Start is called before the first frame update
    void Start()
    {
        _removable.AddRemoveListener(Remove);
    }

    void Remove()
    {
        foreach (Transform child in transform)
        {
            if (child != transform)
                Destroy(child.gameObject);

        }

        Destroy(gameObject);
    }

}
