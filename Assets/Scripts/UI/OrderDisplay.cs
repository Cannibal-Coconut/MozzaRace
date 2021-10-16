using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Since it is not clear yet, I'll just let this be. For now, it only displays stuff
/// </summary>
public class OrderDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] SingleOrderDisplay[] _displayers;

    [Header("References")]
    [SerializeField] Sprite _baconSprite;
    [SerializeField] Sprite _tomatoSprite;
    [SerializeField] Sprite _pineappleSprite;

    public void DisplayDesiredIngredients(List<MealOrder> orders, int selectedOrder)
    {
        if (orders.Count > _displayers.Length)
        {
            Debug.LogWarning("Not enough Displayers!");

            return;
        }

        foreach (var display in _displayers)
        {
            display.HideSprites();
        }

        for (int i = 0; i < orders.Count; i++)
        {
            Sprite[] sprites = new Sprite[orders[i].ingredients.Count];
            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j] = GetSpriteForIngredient(orders[i].ingredients[j]);
            }

            bool markAsSelected = false;

            if (i == selectedOrder)
                markAsSelected = true;

            _displayers[i].SetIngredientsSprites(orders[i], sprites, markAsSelected);
        }


    }

    Sprite GetSpriteForIngredient(ItemType ingredient)
    {

        switch (ingredient)
        {
            case ItemType.Bacon:
                return _baconSprite;
            //break;

            case ItemType.Tomato:
                return _tomatoSprite;
            //break;

            case ItemType.Pineapple:
                return _pineappleSprite;
            //break;

            default:
                return null;
                //break;
        }
    }

}
