using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Inventory : MonoBehaviour
{
    public List<ItemType> desiredItems;

    AudioSource _audioSource;

    DesiredIngredientsDisplay _desiredDisplay;

    /// <summary>
    ///Current item in desiredItems to pick
    /// </summary>
    int _listIndex;


    private void Awake()
    {
        desiredItems = new List<ItemType>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;


        //QUICK AND DIRTY
        ItemType[] items = new ItemType[3] {
            ItemType.Bacon, ItemType.Tomato, ItemType.Pineapple
       };
        _desiredDisplay = FindObjectOfType<DesiredIngredientsDisplay>();
        SetDesiredItemsList(items);
        //QUICK AND DIRTY



    }

    public void SetDesiredItemsList(ItemType[] items)
    {
        desiredItems.Clear();
        _listIndex = 0;

        foreach (var item in items)
        {
            desiredItems.Add(item);
        }

        if (_desiredDisplay)
        {
            _desiredDisplay.DisplayDesiredIngredients(desiredItems.ToArray());
        }
    }

    public void RestartPickedItems()
    {
        _listIndex = 0;
    }

    void DesiredItemsAchieved()
    {
        Debug.Log("You've really made the grade!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        //Collided object is an item
        if (item)
        {
            //Is it the item we want?
            if (_listIndex < desiredItems.Count && item.itemType == desiredItems[_listIndex])
            {
                if (item.audioClip)
                {
                    _audioSource.clip = item.audioClip;
                    _audioSource.Play();
                }

                _listIndex++;

                //Check if it is done
                if (_listIndex == desiredItems.Count)
                {
                    DesiredItemsAchieved();
                }
            }
            else
            {
                //Wrong Item picked, start over.
                RestartPickedItems();
            }

            item.Pick();
        }
    }

}
