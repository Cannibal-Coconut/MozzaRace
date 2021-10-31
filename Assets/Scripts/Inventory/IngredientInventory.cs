using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Pick up and Check ingredients. Care for PickingUpColliders in editor inspector! It doesnt include this object.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class IngredientInventory : MonoBehaviour, ILiveListener
{
    public const int MaxOrders = 3;

    [Header("References")]
    [Tooltip("Theses colliders will catch up ingredients")]
    [SerializeField] Collider2D[] _pickingUpColliders;
    [SerializeField] ItemSet _itemSet;

    [Header("Settings")]
    [Tooltip("Loose of points overtime")]
    [SerializeField] [Range(0, 100)] int _loose = 5;
    [Tooltip("Delay for points going down")]
    [SerializeField] [Range(0.1f, 5)] float _looseDelay = 4;

    //Orders in use.
    //CARE: Dont modify orders directly. Instead, use RemoveOrder(), AddOrder... so it is updated on UI.
    public List<MealOrder> orders;
    //Index for selected MealOrder from orders list.
    int _selectedOrder;

    public int points { get; private set; }

    AudioSource _audioSource;
    OrderDisplay _orderDisplay;
    PointsDisplay _pointsDisplay;

    Coroutine _countDownCoroutine;
    Coroutine _orderGiverCoroutine;

    public Item GetItemPrototype()
    {
        List<ItemType> ingredients = new List<ItemType>();

        foreach (var order in orders)
        {
            foreach (var ingredient in order.ingredients)
            {
                ingredients.Add(ingredient);
            }
        }

        foreach (ItemType item in Enum.GetValues(typeof(ItemType)))
        {
            ingredients.Add(item);
        }

        var type = ingredients[Random.Range(0, ingredients.Count)];
        return _itemSet.GetItem(type);

    }




    private void Awake()
    {
        orders = new List<MealOrder>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        _orderDisplay = FindObjectOfType<OrderDisplay>();
        _pointsDisplay = FindObjectOfType<PointsDisplay>();

        PreparepickingUpColliders();

        SetListeners();
    }

    private void Start()
    {
        //QUICK AND DIRTY
        AddRandomOrder();
        //QUICK AND DIRTY
    }

    void StartOrderClock()
    {
        StopOrderClock();

        _countDownCoroutine = StartCoroutine(Countdown());
        _orderGiverCoroutine = StartCoroutine(OrderGiver());
    }

    void StopOrderClock()
    {
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
        }


        if (_orderGiverCoroutine != null)
        {
            StopCoroutine(_orderGiverCoroutine);
        }
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
        List<ItemType> allItems = new List<ItemType>();

        foreach (ItemType item in System.Enum.GetValues(typeof(ItemType)))
        {
          allItems.Add(item);
        }

        List<ItemType> items = new List<ItemType>();

        for(int i = 0; i < 3; i++) {
            ItemType itemToAdd = allItems[Random.Range(0,allItems.Count)];
            items.Add(itemToAdd);
            allItems.Remove(itemToAdd);
        }

        AddOrder(new MealOrder(100, items));
    }

    void AddOrder(MealOrder order)
    {
        orders.Add(order);

        _orderDisplay.DisplayOrders(orders, _selectedOrder);
    }

    public void OnLive()
    {
        StartOrderClock();
    }

    public void OnDead()
    {
        StopOrderClock();
    }

    public void SetListeners()
    {
        var player = FindObjectOfType<Health>();
        if (player)
        {
            player.AddLiveListener(OnLive);
            player.AddDeadListener(OnDead);
        }

    }

    public void ResetInventory(){

     for (int i = 0; i < orders.Count; i++)RemoveOrder(i);
     for (int i = 0; i < orders.Count; i++)RemoveOrder(i);
    points = 0;
    ChangePoints(points);
    AddRandomOrder();
    }

}

