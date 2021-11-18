using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skin
{
    public float r;
    public float g;
    public float b;
    public float a;

    public Color color
    {
        get
        {
            return new Color(r, g, b, a);
        }
    }

    public int value { get; private set; }
    public bool purchased;

    public Skin(Color color, int value)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;

        this.value = value;
    }

}
