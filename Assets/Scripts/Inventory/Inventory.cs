using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pick up and Check ingredients. Care for PickingUpColliders in editor inspector! It doesnt include this object.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Inventory : MonoBehaviour
{
    public const int MaxOrders = 3;

    [Header("References")]
    [Tooltip("Theses colliders will catch up ingredients")]
    [SerializeField] Collider2D[] _pickingUpColliders;

    [Header("Settings")]
    [Tooltip("Loose of points overtime")]
    [SerializeField] [Range(0, 100)] int _loose = 5;
    [Tooltip("Delay for points going down")]
    [SerializeField] [Range(0.1f, 5)] float _looseDelay = 2;

    //Orders in use.
    //CARE: Dont modify orders directly. Instead, use RemoveOrder(), AddOrder... so it is updated on UI.
    public List<MealOrder> orders;
    //Index for selected MealOrder from orders list.
    int _selectedOrder;

    public int points { get; private set; }

    AudioSource _audioSource;
    OrderDisplay _orderDisplay;
    PointsDisplay _pointsDisplay;

    private void Awake()
    {
        orders = new List<MealOrder>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        _orderDisplay = FindObjectOfType<OrderDisplay>();
        _pointsDisplay = FindObjectOfType<PointsDisplay>();

        PreparepickingUpColliders();
    }

    private void Start()
    {
        //QUICK AND DIRTY
        AddRandomOrder();
        //QUICK AND DIRTY

        StartCoroutine(Countdown());
        StartCoroutine(OrderGiver());
    }

    public void SelectOrder(MealOrder order)
    {
        if (orders.Contains(order))
        {
            ChangeSelectedOrder(orders.IndexOf(order));
        }
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

    void ChangePoints(int value)
    {
        points += value;

        if (_pointsDisplay)
        {
            _pointsDisplay.SetPointsInDisplay(points);
        }

    }

    //QUICK AND DIRTY
    IEnumerator Countdown()
    {
        while (true)
        {
            foreach (var order in orders)
            {
                order.ChangePoints(-_loose);
            }
            _orderDisplay.UpdatePoints();


            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].points == 0)
                {
                    RemoveOrder(i);

                    i--;
                }
            }

            yield return new WaitForSeconds(_looseDelay);
        }

    }

    IEnumerator OrderGiver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (orders.Count != MaxOrders)
            {
                AddRandomOrder();
            }
        }
    }
    //QUICK AND DIRTY

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
            CorrectIngredientPicked(item);
        }
        else
        {
            WrongIngredientPicked(item);
        }

        item.Pick();

        _orderDisplay.DisplayOrders(orders, _selectedOrder);
    }

    void CorrectIngredientPicked(Item item)
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
            ChangePoints(orders[_selectedOrder].points);

            RemoveOrder(_selectedOrder);
        }
    }

    void WrongIngredientPicked(Item item)
    {
        if (orders[_selectedOrder].ChangePoints(-10))
        {
            RemoveOrder(_selectedOrder);
        }
    }

    void RemoveOrder(int i)
    {
        orders.RemoveAt(i);
        _selectedOrder = 0;

        _orderDisplay.DisplayOrders(orders, _selectedOrder);

    }

    void ChangeSelectedOrder(int selected)
    {
        _selectedOrder = selected;

        _orderDisplay.DisplayOrders(orders, _selectedOrder);
    }

    void AddRandomOrder()
    {
        List<ItemType> items = new List<ItemType>() {
            ItemType.Bacon, ItemType.Tomato, ItemType.Pineapple
       };


        AddOrder(new MealOrder(100, items));
    }

    void AddOrder(MealOrder order)
    {
        orders.Add(order);

        _orderDisplay.DisplayOrders(orders, _selectedOrder);
    }
}

