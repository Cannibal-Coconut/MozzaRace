using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Tooltip("Spawnable Ingredients")]
    [SerializeField]
    List<Ingredient> _ingredients;

    //Range of ingredients' weight
    int _totalWeight;

    private void Awake()
    {
        UpdateTotalWeight();
    }

    private void Start()
    {
        StartCoroutine(SpawnerCycle());
    }

    void UpdateTotalWeight()
    {
        _totalWeight = 0;

        foreach (Ingredient ingredient in _ingredients)
        {
            _totalWeight += ingredient.spawnWeight;
        }
    }

    public void SpawnRandomItem()
    {
        int token = Random.Range(0, _totalWeight);
        int sum = 0;

        foreach (var ingredient in _ingredients)
        {
            //Calculate ingredient's range
            sum += ingredient.spawnWeight;

            //Check if sum is within ingredient's range
            if (token < sum)
            {
                SpawnItem(ingredient.itemPrototype);

                return;
            }
        }
    }

    public void SpawnItem(Item item)
    {
        var newItem = Instantiate(item);

        newItem.go = true;

        Debug.Log(item.name + " Created!");
    }

    //QUICK AND DIRTY, REMOVE WHEN DONE
    IEnumerator SpawnerCycle()
    {
        while (true)
        {
            SpawnRandomItem();
            yield return new WaitForSeconds(1.5f);
        }
    }

    [System.Serializable]
    class Ingredient
    {
        public Item itemPrototype;

        [Tooltip("Probability for spawn of this ingredient")]
        public int spawnWeight;
    }

}
