using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleOrderDisplay : MonoBehaviour
{

    [Tooltip("Display of Ingredients from right to left")]
    [SerializeField] Image[] _ingredientHolders;

    public void SetIngredientsSprites(Sprite[] sprites)
    {
        if (sprites.Length <= _ingredientHolders.Length)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                _ingredientHolders[i].sprite = sprites[i];
                _ingredientHolders[i].enabled = true;
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

}
