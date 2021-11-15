using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMission : Mission
{

    ItemType[] _targetOrder;
    int _position;
    bool _done = false;

    public OrderMission()
    {
        var inventory = GameObject.FindObjectOfType<IngredientInventory>();

        inventory.AddOnItemTakenListener(ItemPicked);

        SetTargetOrder();
    }

    public override bool CheckMission()
    {
        return _done;
    }

    public override void EndGame()
    {
        _position = 0;
        _done = false;
    }

    public override void StartGame()
    {
        _position = 0;
        _done = false;
    }

    void ItemPicked(Item item)
    {
        if (item.itemType == _targetOrder[_position])
        {
            _position++;


            if (_position >= _targetOrder.Length)
            {
                _done = true;
            }

        }
        else
        {
            _position = 0;
        }
    }

    void SetTargetOrder()
    {
        List<SpecialOrder> possibleOrders = new List<SpecialOrder>();

        //Add Here possible orders
        possibleOrders.Add(new SpecialOrder("Eggspecial", new ItemType[] { ItemType.Egg, ItemType.Egg, ItemType.Egg }));

        var specialOrder = possibleOrders[Random.Range(0, possibleOrders.Count)];
        _targetOrder = specialOrder.ingredients;
        Debug.Log(specialOrder.name + " order selected!");
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
