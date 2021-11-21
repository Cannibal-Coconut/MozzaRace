using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTimer : MonoBehaviour
{

    private Image _fill;
    private Color _fillColor;

    private float _currentPoints;

    private float _totalPoints;

    // Start is called before the first frame update
    void Start()
    {
        _fill = GetComponent<Image>();
        _fillColor = Color.green;
    }

    // Update is called once per frame

    public void UpdateTimer()
    {

        float currentPointPercentage = _currentPoints / _totalPoints;
        if (currentPointPercentage > 0.5)
        {
            SetFillColor(Color.green);
        }
        else if (currentPointPercentage > 0.25)
        {
            SetFillColor(Color.yellow);
        }
        else if (currentPointPercentage >= 0)
        {
            SetFillColor(Color.red);
        }

        if (_fill)
        {
            _fill.color = _fillColor;
            _fill.fillAmount = currentPointPercentage;

        }


    }

    public void UpdateCurrentPoints(int current) { _currentPoints = current; }

    public void SetTotalPoints(int total) { _totalPoints = total; }

    public void SetFillColor(Color color)
    {

        _fillColor = color;

    }
}
