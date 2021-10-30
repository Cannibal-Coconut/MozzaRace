using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSet", menuName = "ScriptableObjects/ItemSet", order = 1)]
public class ItemSet : ScriptableObject
{
    public Item anchovy;
    public Item bacon;
    public Item cheese;
    public Item egg;
    public Item ham;
    public Item mushroom;
    public Item olive;
    public Item onion;
    public Item pepperoni;
    public Item pineapple;
    public Item shrimp;
    public Item tomato;
    public Item tuna;

    public Item GetItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Anchovy:
                return anchovy;

            case ItemType.Bacon:
                return bacon;

            case ItemType.Cheese:
                return cheese;

            case ItemType.Egg:
                return egg;

            case ItemType.Ham:
                return ham;


            case ItemType.Mushroom:
                return mushroom;

            case ItemType.Olive:
                return olive;

            case ItemType.Onion:
                return onion;


            case ItemType.Pineapple:
                return pineapple;

            case ItemType.Pepperoni:
                return pepperoni;

            case ItemType.Shrimp:
                return shrimp;

            case ItemType.Tomato:
                return tomato;

            case ItemType.Tuna:
                return tuna;
            default:
                throw new System.Exception("Requested Item is not implemented in ItemSet class GetItem()!");
        }

    }

}
