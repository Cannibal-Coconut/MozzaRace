using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealOrder
{
    public List<ItemType> ingredients;
    public int points { get; private set; }

    private int _maxPoints;
    public MealOrder(int maxPoints, List<ItemType> ingredients)
    {
        points = maxPoints;
        _maxPoints = maxPoints;
        this.ingredients = ingredients;
    }

    public int GetMaxPoints(){

        return  _maxPoints;

    }

    /// <summary>
    /// Add quantity to points. If points are 0 or less, return true.
    /// </summary>
    /// <param name="change"></param>
    /// <returns></returns>
    public bool ChangePoints(int change)
    {
        points += change;

        if (points <= 0)
        {
            points = 0;

            //No more points
            return true;
        }
        else
        {
            //There are still some points
            return false;
        }
    }

}
