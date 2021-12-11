using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    Skin _currentSkin;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSkin(Skin skin) {
        _currentSkin = skin;


    }


}
