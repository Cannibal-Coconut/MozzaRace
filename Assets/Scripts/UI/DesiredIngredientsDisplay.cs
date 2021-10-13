using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Since it is not clear yet, I'll just let this be. For now, it only displays stuff
/// </summary>
public class DesiredIngredientsDisplay : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("Display of Ingredients from right to left")]
    [SerializeField] SpriteRenderer[] _ingredientHolders;

    [Header("References")]
    [SerializeField] Sprite _baconSprite;
    [SerializeField] Sprite _tomatoSprite;
    [SerializeField] Sprite _pineappleSprite;

    public void DisplayDesiredIngredients(ItemType[] ingredients)
    {
        foreach (var holder in _ingredientHolders)
        {
            holder.enabled = false;
        }

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (i < _ingredientHolders.Length)
            {
                _ingredientHolders[i].enabled = true;

                _ingredientHolders[i].sprite = GetSpriteForIngredient(ingredients[i]);
            }
            else
            {
                Debug.LogError("Not Enough Holders!");
            }

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
