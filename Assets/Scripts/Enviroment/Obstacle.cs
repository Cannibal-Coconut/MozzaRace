using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0, 3)] int _damage;

    SpriteRenderer _renderer;
    BoxCollider2D _collider;

    public bool go;

    public int damage
    {
        get
        {
            return _damage;
        }
    }

    private void Awake()
    {

        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    public void Hide()
    {
        _renderer.enabled = false;
        _collider.enabled = false;

    }

    public void Show()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
    }


    //QUICK AND DIRTY. REMOVE TO STOP GOING LIKE CRAZY
    private void FixedUpdate()
    {
        if (go)
            transform.position += new Vector3(-10f, 0, 0) * Time.fixedDeltaTime;
    }
    //QUICK AND DIRTY


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Health>();

        if (player)
        {
            player.HurtPlayer(_damage);

            Hide();
        }
    }

}
