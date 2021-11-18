using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTimer : MonoBehaviour
{

    private Image _fill;
    private Color _fillColor;

    // Start is called before the first frame update
    void Start()
    {
        _fill = GetComponent<Image>();
        _fillColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
         SetFillColor(_fillColor);   
    }

    public void UpdateColor(){

      //  _fillColor = g

    }



    private void SetFillColor(Color color){

        _fill.color = color;

    }
}
