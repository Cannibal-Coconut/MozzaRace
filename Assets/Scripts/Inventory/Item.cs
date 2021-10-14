using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [Header("Settings")]
    public ItemType itemType;

    [Tooltip("Picking up audio")]
    [SerializeField] AudioClip _audioClip;

    //QUICK AND DIRTY. LOOK AT FIXED UPDATE, IT IS JUST TO MAKE IT MOVE
    public bool go;
    //QUICK AND DIRTY

    public AudioClip audioClip
    {
        get
        {
            return _audioClip;
        }
    }

    private void Awake()
    {
        var collider = GetComponent<Collider2D>();

        //No need for collision, just trigger.
        collider.isTrigger = true;

    }

    public void Pick()
    {

        //Things to do when picked!
        Destroy(gameObject);

    }


    //QUICK AND DIRTY. REMOVE TO STOP GOING LIKE CRAZY
    private void FixedUpdate()
    {
        if (go)
            transform.position += new Vector3(-0.1f, 0, 0) * Time.fixedDeltaTime;
    }
    //QUICK AND DIRTY


}

public enum ItemType
{
    Bacon,
    Tomato,
    Pineapple,
    Ham,
    Onion,
    Pepper,
    Mozzarella,
    Mushroom
}