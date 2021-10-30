using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSet", menuName = "ScriptableObjects/ItemSet", order = 1)]
public class ItemSet : ScriptableObject
{
    public Item bacon;
    public Item ham;
    public Item mozzarella;
    public Item mushroom;
    public Item onion;
    public Item pepper;
    public Item pineapple;
    public Item tomato;

    public Item GetItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Bacon:
                return bacon;

            case ItemType.Ham:
                return ham;

            case ItemType.Mozzarella:
                return mozzarella;

            case ItemType.Mushroom:
                return mushroom;

            case ItemType.Onion:
                return onion;

            case ItemType.Pepper:
                return pepper;

            case ItemType.Pineapple:
                return pineapple;

            case ItemType.Tomato:
                return tomato;
            default:
                throw new System.Exception("Requested Item is not implemented in ItemSet class GetItem()!");
        }

    }

}
