using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pick up and Check ingredients. Care for PickingUpColliders in editor inspector! It doesnt include this object.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Theses colliders will catch up ingredients")]
    [SerializeField] Collider2D[] _pickingUpColliders;

    public List<MealOrder> orders;
    int _selectedOrder;

    AudioSource _audioSource;

    OrderDisplay _orderDisplay;

    private void Awake()
    {
        orders = new List<MealOrder>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        PreparepickingUpColliders();

        //QUICK AND DIRTY

        AddRandomOrder();
        //QUICK AND DIRTY
    }

    void PreparepickingUpColliders()
    {
        if (_pickingUpColliders != null)
        {
            for (int i = 0; i < _pickingUpColliders.Length; i++)
            {
                var colliderDelegate = _pickingUpColliders[i].gameObject.AddComponent<ColliderDelegate>();

                colliderDelegate.onEnterTriggerAction = CheckTrigger;

            }
        }

    }

    /// <summary>
    /// Check if such Collider's object is an ingredient
    /// </summary>
    /// <param name="collider"></param>
    void CheckTrigger(Collider2D collider)
    {
        Item item = collider.GetComponent<Item>();

        //Collided object is an item
        if (item)
        {
            CheckIngredient(item);
        }

    }

    void CheckIngredient(Item item)
    {
        //Is it the item we want?
        if (orders[_selectedOrder].ingredients.Contains(item.itemType))
        {
            orders[_selectedOrder].ingredients.Remove(item.itemType);

            if (item.audioClip)
            {
                _audioSource.clip = item.audioClip;
                _audioSource.Play();
            }

            //Check if it is done
            if (orders[_selectedOrder].ingredients.Count == 0)
            {
                RemoveOrder(_selectedOrder);
            }
        }
        else
        {
            if (orders[_selectedOrder].ChangePoints(-10))
            {
                RemoveOrder(_selectedOrder);
            }
        }

        item.Pick();

        _orderDisplay.DisplayDesiredIngredients(orders);
    }


    void RemoveOrder(int i)
    {
        orders.RemoveAt(i);
        _selectedOrder = 0;

        _orderDisplay.DisplayDesiredIngredients(orders);

        if (orders.Count == 0)
        {
            AddRandomOrder();
        }
    }

    void AddOrder(MealOrder order)
    {
        orders.Add(order);

        _orderDisplay.DisplayDesiredIngredients(orders);
    }

    void AddRandomOrder()
    {
        List<ItemType> items = new List<ItemType>() {
            ItemType.Bacon, ItemType.Tomato, ItemType.Pineapple
       };

        _orderDisplay = FindObjectOfType<OrderDisplay>();

        AddOrder(new MealOrder(100, items));
    }
}

