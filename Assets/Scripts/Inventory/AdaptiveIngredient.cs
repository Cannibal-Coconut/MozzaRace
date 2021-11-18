using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveIngredient : MonoBehaviour
{

    private void Start()
    {
        var inventory = FindObjectOfType<IngredientInventory>();

        var item = Instantiate(inventory.GetItemPrototype());

        item.transform.position = transform.position;
        item.transform.parent = transform;


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
