using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skin
{
    public int value { get; private set; }
    public SkinHandler.SkinEnum skin { get; private set; }
    public bool purchased;
    public Sprite sprite;
    public Skin(SkinHandler.SkinEnum s, int value)
    {
        this.skin = s;
        this.value = value;
        SkinHandler sk = GameObject.FindObjectOfType<SkinHandler>();
        if(sk.GetSprite(s) !=null) this.sprite =sk.GetSprite(s);
        else this.sprite = sk.GetSprite(this.skin);
    }

}
