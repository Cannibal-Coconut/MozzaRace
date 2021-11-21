using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderMission : Mission
{
    [Header("Settings")]
    [SerializeField] List<ItemType> _targetOrder;

    List<ItemType> _remainingItems;

    bool _done = false;

    public override bool CheckMission()
    {
        return _done;
    }

    public override void EndGame()
    {
        _done = false;
        if (_done == false)
        {
            Initialize();
        }
    }

    public override void Initialize()
    {
        _done = false;
        var inventory = GameObject.FindObjectOfType<IngredientInventory>();
        _remainingItems = new List<ItemType>(_targetOrder);

        inventory.AddOnItemTakenListener(ItemPicked);
    }

    public override void StartGame()
    {

    }

    void ItemPicked(Item item)
    {
        if (_remainingItems.Contains(item.itemType))
        {
            _remainingItems.Remove(item.itemType);

            if (_remainingItems.Count == 0)
            {
                _done = true;
            }

        }
    }

    struct SpecialOrder
    {
        public string name { get; private set; }
        public ItemType[] ingredients { get; private set; }

        public SpecialOrder(string name, ItemType[] ingredients)
        {
            this.name = name;

            this.ingredients = ingredients;

        }

    }
}
