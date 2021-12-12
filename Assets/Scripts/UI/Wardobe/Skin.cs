using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skin
{
    public int value;
    public SkinEnum skinID;
    public bool purchased;
   // public Sprite sprite;

    public Skin(SkinEnum s, int value)
    {
        this.skinID = s;
        this.value = value;
        SkinHandler sk = GameObject.FindObjectOfType<SkinHandler>();
       // if (sk.GetSprite(s) != null) this.sprite = sk.GetSprite(s);
        //else this.sprite = sk.GetSprite(s);
    }

}
