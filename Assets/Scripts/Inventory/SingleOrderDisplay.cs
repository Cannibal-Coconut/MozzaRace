using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Representaion on UI of a MealOrder
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class SingleOrderDisplay : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Display of Ingredients from right to left")]
    [SerializeField] Image[] _ingredientHolders;
    [SerializeField] TextMeshProUGUI _pointsMesh;

    [SerializeField] Image _selectionMarker;

    [Header("Sprites")]
    [SerializeField] Sprite _baconSprite;
    [SerializeField] Sprite _tomatoSprite;
    [SerializeField] Sprite _pineappleSprite;

    MealOrder _mealOrder;

    Inventory _inventory;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    /// <summary>
    /// Set MealOrder and display acordingly.
    /// </summary>
    /// <param name="mealOrder"></param>
    /// <param name="displayAsSelected"></param>
    public void SetInMealOrder(MealOrder mealOrder, bool displayAsSelected)
    {
        Show();

        for (int i = 0; i < _ingredientHolders.Length; i++)
        {
            _ingredientHolders[i].enabled = false;
        }

        for (int i = 0; i < mealOrder.ingredients.Count; i++)
        {
            _ingredientHolders[i].enabled = true;
            _ingredientHolders[i].sprite = GetSpriteForIngredient(mealOrder.ingredients[i]);
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

        UpdatePoints();
    }

    /// <summary>
    /// Update just the points of this order
    /// </summary>
    public void UpdatePoints()
    {
        if (_mealOrder != null)
            _pointsMesh.text = _mealOrder.points.ToString();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
    }

    /// <summary>
    /// Hide and turn assigned MealOrder null
    /// </summary>
    public void Hide()
    {
        _canvasGroup.alpha = 0;

        _mealOrder = null;
    }

    /// <summary>
    /// Mark order as selected. Note: buttons on canvas might use it.
    /// </summary>
    public void SelectForOrder()
    {
        _inventory.SelectOrder(_mealOrder);
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
