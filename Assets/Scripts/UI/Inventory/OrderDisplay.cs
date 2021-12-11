using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Adapter for all SingleOrderDisplay on scene.
/// </summary>
public class OrderDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] SingleOrderDisplay[] _displayers;

    /// <summary>
    /// Update points on every SingleOrderDisplay
    /// </summary>
    public void UpdatePoints() {

        foreach (var displayer in _displayers)
        {
            displayer.UpdatePoints();
        }
    }


    /// <summary>
    /// Display on SingleOrderDisplay's on scene given MealOrders. Unused SingleOrderDisplay's, will be hidden
    /// </summary>
    /// <param name="orders"></param>
    /// <param name="selectedOrder">Wich of these orders is selected</param>
    public void DisplayOrders(List<MealOrder> orders, int selectedOrder)
    {
        if (orders.Count > _displayers.Length)
        {
            Debug.LogWarning("Not enough Displayers!");

            return;
        }

        //Hide all displayers
        foreach (var display in _displayers)
        {
            display.Hide();
        }

        if (orders.Count == 0) return;

        //Show only used displayers and set mealOrder on them
        for (int i = 0; i < orders.Count; i++)
        {
            bool markAsSelected = false;

            if (i == selectedOrder)
                markAsSelected = true;

            _displayers[i].SetInMealOrder(orders[i], markAsSelected);
        }


    }

}
