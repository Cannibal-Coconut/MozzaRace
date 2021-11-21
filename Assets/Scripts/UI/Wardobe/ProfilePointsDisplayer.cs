using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ProfilePointsDisplayer : MonoBehaviour
{
    TextMeshProUGUI _textMesh;
    ProfileInventory _inventory;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();

        _inventory = FindObjectOfType<ProfileInventory>();

        _inventory.AddOnEconomyChangeListener(UpdateText);

        UpdateText();
    }

    void UpdateText()
    {
        _textMesh.text = _inventory.points.ToString();
    }


}
