using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin
{
    public Color color { get; private set; }
    public int value { get; private set; }
    public bool purchased;

    public Skin(Color color, int value)
    {
        this.color = color;
        this.value = value;
    }

}
