using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{

    private float _width;
    private float _scrollSpeed = 8f;
    [SerializeField] private float _parallaxEffect;

    private void Start() {  
              
        _width = GetComponent<SpriteRenderer>().bounds.size.x;

        _parallaxEffect = 1/_parallaxEffect;
        

    }

    private void Update() {
        transform.position  = (Vector2) transform.position - new Vector2(_scrollSpeed * _parallaxEffect* Time.deltaTime,0);
        if(transform.position.x < -_width){
            
            Vector2 resetPosition = new Vector2(_width*2f,0);
            transform.position = (Vector2)transform.position + resetPosition ;

        }
    }

}
