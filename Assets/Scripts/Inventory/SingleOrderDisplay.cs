using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleOrderDisplay : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Display of Ingredients from right to left")]
    [SerializeField] Image[] _ingredientHolders;
    [SerializeField] Image _selectionMarker;

    MealOrder _mealOrder;

    Inventory _inventory;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    public void SetIngredientsSprites(MealOrder mealOrder, Sprite[] sprites, bool displayAsSelected)
    {
        if (sprites.Length <= _ingredientHolders.Length)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                _ingredientHolders[i].sprite = sprites[i];
                _ingredientHolders[i].enabled = true;
            }

            _mealOrder = mealOrder;

            if (displayAsSelected)
            {
                _selectionMarker.enabled = true;
            }
            else
            {
                _selectionMarker.enabled = false;
            }
        }

    }

    public void HideSprites()
    {
        for (int i = 0; i < _ingredientHolders.Length; i++)
        {
            _ingredientHolders[i].enabled = false;
        }

    }

    public void SelectForOrder()
    {
        _inventory.SelectOrder(_mealOrder);
    }

}
