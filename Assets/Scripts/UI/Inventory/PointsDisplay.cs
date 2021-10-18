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

    public void SetPointsInDisplay(int points) {

        if (_textMesh != null) {
            _textMesh.text = "Points: " + points;
        }

    }

}
