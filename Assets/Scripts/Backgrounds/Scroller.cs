using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{

    private BoxCollider2D _collider;
    private float _width;
    private float _scrollSpeed = 8f;


    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
        
        _width = _collider.size.x;

        

    }

    private void Update() {
        transform.position  = (Vector2) transform.position - new Vector2(_scrollSpeed * Time.deltaTime,0);
        if(transform.position.x < -_width){
            
            Vector2 resetPosition = new Vector2(_width*2f,0);
            transform.position = (Vector2)transform.position + resetPosition ;

        }
    }

}
