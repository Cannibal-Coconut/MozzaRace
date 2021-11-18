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
    [SerializeField] Sprite _acnhovySprite;
    [SerializeField] Sprite _baconSprite;
    [SerializeField] Sprite _cheeseSprite;
    [SerializeField] Sprite _eggSprite;
    [SerializeField] Sprite _hamSprite;
    [SerializeField] Sprite _mushroomSprite;
    [SerializeField] Sprite _oliveSprite;
    [SerializeField] Sprite _onionSprite;
    [SerializeField] Sprite _pepperoniSprite;
    [SerializeField] Sprite _pineappleSprite;
    [SerializeField] Sprite _shrimpSprite;
    [SerializeField] Sprite _tomatoSprite;
    [SerializeField] Sprite _tunaSprite;

    MealOrder _mealOrder;

    IngredientInventory _inventory;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _inventory = FindObjectOfType<IngredientInventory>();
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

    private Sprite GetSpriteForIngredient(ItemType ingredient)
    {
        return ingredient switch
        {
            ItemType.Anchovy => _acnhovySprite,
            //break;
            ItemType.Bacon => _baconSprite,
            //break;
            ItemType.Cheese => _cheeseSprite,
            //break;
            ItemType.Egg => _eggSprite,
            //break;
            ItemType.Ham => _hamSprite,
            //break;
            ItemType.Mushroom => _mushroomSprite,
            //break;
            ItemType.Olive => _oliveSprite,
            //break;
            ItemType.Onion => _onionSprite,
            //break;
            ItemType.Pepperoni => _pepperoniSprite,
            //break;
            ItemType.Pineapple => _pineappleSprite,
            //break;
            ItemType.Shrimp => _shrimpSprite,
            //break;  
            ItemType.Tomato => _tomatoSprite,
            //break;
            ItemType.Tuna => _tunaSprite,
            _ => null
        };
    }

}
