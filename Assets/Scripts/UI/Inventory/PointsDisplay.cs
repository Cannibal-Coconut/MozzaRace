using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Just a display in screen of current points with cool effects
/// </summary>
public class PointsDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _textMesh;

    ProfileInventory _inventory;

    private void Start()
    {
        _inventory = FindObjectOfType<ProfileInventory>();

        _inventory.AddOnEconomyChangeListener(SetPointsInDisplay);
    }

    public void SetPointsInDisplay()
    {
        if (_textMesh != null)
        {
            _textMesh.text ="" +  _inventory.matchPoints;

        }

    }

}
