using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pick up and Check ingredients. Care for PickingUpColliders in editor inspector! It doesnt include this object.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Theses colliders will catch up ingredients")]
    [SerializeField] Collider2D[] _pickingUpColliders;

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

        PreparepickingUpColliders();

        //QUICK AND DIRTY
        ItemType[] items = new ItemType[3] {
            ItemType.Bacon, ItemType.Tomato, ItemType.Pineapple
       };
        _desiredDisplay = FindObjectOfType<DesiredIngredientsDisplay>();
        SetDesiredItemsList(items);
        //QUICK AND DIRTY
    }

    void PreparepickingUpColliders()
    {
        if (_pickingUpColliders != null)
        {
            for (int i = 0; i < _pickingUpColliders.Length; i++)
            {
                var colliderDelegate = _pickingUpColliders[i].gameObject.AddComponent<ColliderDelegate>();

                colliderDelegate.onEnterTriggerAction = CheckTrigger;

            }
        }

    }

    /// <summary>
    /// Check if such Collider's object is an ingredient
    /// </summary>
    /// <param name="collider"></param>
    void CheckTrigger(Collider2D collider)
    {
        Item item = collider.GetComponent<Item>();

        //Collided object is an item
        if (item)
        {
            CheckIngredient(item);
        }

    }

    void CheckIngredient(Item item)
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

}
