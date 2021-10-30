using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{

    private BoxCollider2D _collider;
    private Rigidbody2D _rb2d;
    private float _width;
    private float _scrollSpeed = -2f;


    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
        _rb2d = GetComponent<Rigidbody2D>();
        
        _width = _collider.size.x;
        _collider.enabled = false;

        _rb2d.velocity = new Vector2(_scrollSpeed, 0);

    }

    private void Update() {
        if(transform.position.x < -_width){
            
            Vector2 resetPosition = new Vector2(_width *2.5f ,0);
            transform.position = (Vector2)transform.position + resetPosition;

        }
    }

}
