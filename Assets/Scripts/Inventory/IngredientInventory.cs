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
    [Header("References")]
    [Tooltip("Theses colliders will catch up ingredients")]
    [SerializeField] Collider2D[] _pickingUpColliders;
    [SerializeField] ItemSet _itemSet;

    [Tooltip("Delay for assigning correct mealOrder")]
    [SerializeField] [Range(0.1f, 1)] float _assigningDelay = 0.5f;

    InventorySettings _settings;
    ProfileInventory _profileInventory;

    public InventorySettings defaulSettings;

    /// <summary>
    ///Orders in use.
    /// CARE: Dont modify orders directly. Instead, use RemoveOrder(), AddOrder... so it is updated on UI.
    /// </summary>
    public List<MealOrder> orders;

    List<DelayedIngredient> _delayedIngredients;
    //Index for selected MealOrder from orders list.
    int _selectedOrder;

    AudioSource _audioSource;
    OrderDisplay _orderDisplay;
    PointsDisplay _pointsDisplay;

    Coroutine _countDownCoroutine;
    Coroutine _orderGiverCoroutine;

    Action<Item> _onTakenItemAction;
    public Action onTakenTipAction;

    public Item GetItemPrototype()
    {
        List<ItemType> ingredients = new List<ItemType>();


        foreach (var order in orders)
        {
            foreach (var ingredient in order.ingredients)
            {
                for (int i = 0; i < 1 + _settings.requestedItemSpawnBuff; i++)
                {
                    ingredients.Add(ingredient);
                }

            }
        }

        foreach (ItemType item in Enum.GetValues(typeof(ItemType)))
        {
            ingredients.Add(item);
        }

        var type = ingredients[Random.Range(0, ingredients.Count)];
        return _itemSet.GetItem(type);

    }

    Action<int> _finishedOrdersIncreasedCallback;
    public int finishedOrders = -1;

    private void Awake()
    {
        orders = new List<MealOrder>();
        _delayedIngredients = new List<DelayedIngredient>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        _orderDisplay = FindObjectOfType<OrderDisplay>();
        _pointsDisplay = FindObjectOfType<PointsDisplay>();

        _profileInventory = FindObjectOfType<ProfileInventory>();

        PreparepickingUpColliders();

        SetListeners();

        _settings = defaulSettings;

    }

    public void AddOnItemTakenListener(Action<Item> action)
    {
        _onTakenItemAction += action;
    }

    private void Start()
    {
        //QUICK AND DIRTY
        AddRandomOrder();
        //QUICK AND DIRTY

        AddFinishedOrder();
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

    public void SelectOrderScrollWheel(float dirScroll)
    {
        if (dirScroll > 0)
        {
            if ((_selectedOrder + 1) > orders.Count - 1)
            {
                _selectedOrder = 0;
                ChangeSelectedOrder(_selectedOrder);
            }
            else
            {
                _selectedOrder++;
                ChangeSelectedOrder(_selectedOrder);
            }
        }
        else if (dirScroll < 0)
        {
            if ((_selectedOrder - 1) < 0)
            {
                _selectedOrder = orders.Count - 1;
                ChangeSelectedOrder(_selectedOrder);
            }
            else
            {
                _selectedOrder--;
                ChangeSelectedOrder(_selectedOrder);
            }
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
        _profileInventory.AddMatchPoints(value);

    }

    //QUICK AND DIRTY
    IEnumerator Countdown()
    {
        while (true)
        {
            foreach (var order in orders)
            {
                order.ChangePoints(-_settings.overtimeLoose);
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

            yield return new WaitForSeconds(_settings.looseDelay);
        }

    }

    IEnumerator OrderGiver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (orders.Count < _settings.maxOrders)
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
        TipJar tipjar = collider.GetComponent<TipJar>();
        Coin coin = collider.GetComponent<Coin>();


        //Collided object is an item
        if (item)
        {
            CheckIngredient(item);

            if (_onTakenItemAction != null)
            {
                _onTakenItemAction.Invoke(item);
            }
        }
        else if (tipjar)
        {

            tipjar.StartTipTimeSpawn();

            if (onTakenTipAction != null)
            {
                onTakenTipAction.Invoke();
            }

        }
        else if (coin)
        {

            coin.AddPointsPerCoin();
        }
    }

    public void SetSettings(InventorySettings newSettings)
    {
        _settings = newSettings;
    }

    void CheckIngredient(Item item)
    {
        if (_selectedOrder >= orders.Count || _selectedOrder < 0) return;

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

            AddFinishedOrder();
        }

        _orderDisplay.DisplayOrders(orders, _selectedOrder);

    }

    void AddFinishedOrder()
    {
        finishedOrders++;
        if (_finishedOrdersIncreasedCallback != null)
        {
            _finishedOrdersIncreasedCallback.Invoke(finishedOrders);
        }

    }

    public void AddFinishedOrderListener(Action<int> action)
    {
        _finishedOrdersIncreasedCallback += action;
    }

    void WrongIngredientPicked(Item item)
    {
        _delayedIngredients.Add(new DelayedIngredient(item, this, _assigningDelay));
    }

    protected void ConfirmWrongIngredient(DelayedIngredient delayedIngredient)
    {
        if (orders[_selectedOrder].ChangePoints(-_settings.wrongIngredientLoose))
        {
            RemoveOrder(_selectedOrder);
        }

        _delayedIngredients.Remove(delayedIngredient);

        _orderDisplay.DisplayOrders(orders, _selectedOrder);

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

        CheckDelayedIngredients();

    }

    void CheckDelayedIngredients()
    {
        for (int i = 0; i < _delayedIngredients.Count; i++)
        {
            if (orders[_selectedOrder].ingredients.Contains(_delayedIngredients[i].item.itemType))
            {
                CorrectIngredientPicked(_delayedIngredients[i].item);
                _delayedIngredients[i].Pick();

                _delayedIngredients.RemoveAt(i);

                i--;
            }

        }

    }

    void AddRandomOrder()
    {
        List<ItemType> allItems = new List<ItemType>();

        foreach (ItemType item in System.Enum.GetValues(typeof(ItemType)))
        {
            allItems.Add(item);
        }

        List<ItemType> items = new List<ItemType>();

        for (int i = 0; i < 3; i++)
        {
            ItemType itemToAdd = allItems[Random.Range(0, allItems.Count)];
            items.Add(itemToAdd);
            allItems.Remove(itemToAdd);
        }

        AddOrder(new MealOrder(_settings.baseOrderPoints, items));
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

    public void ResetInventory()
    {
        for (int i = 0; i < orders.Count; i++) RemoveOrder(i);
        for (int i = 0; i < orders.Count; i++) RemoveOrder(i);

        AddRandomOrder();
    }


    protected class DelayedIngredient
    {
        public Item item { get; private set; }
        IngredientInventory _inventory;
        float _time;

        Coroutine _coroutine;


        public DelayedIngredient(Item item, IngredientInventory inventory, float time)
        {
            this.item = item;
            _inventory = inventory;
            _time = time;

            _coroutine = inventory.StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {

            yield return new WaitForSeconds(_time);

            _inventory.ConfirmWrongIngredient(this);

        }

        public void Pick()
        {
            _inventory.StopCoroutine(_coroutine);
        }
    }

}

