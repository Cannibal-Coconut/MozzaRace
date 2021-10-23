using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Spawnable Ingredients")]
    [SerializeField]
    List<WeightedSpawnable> _spawnables;

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

        foreach (var spawnable in _spawnables)
        {
            _totalWeight += spawnable.spawnWeight;
        }
    }

    public void SpawnRandom()
    {
        int token = Random.Range(0, _totalWeight);
        int sum = 0;

        foreach (var spawnable in _spawnables)
        {
            //Calculate ingredient's range
            sum += spawnable.spawnWeight;

            //Check if sum is within ingredient's range
            if (token < sum)
            {
                Spawn(spawnable.spawnablePrototype);

                return;
            }
        }
    }

    public void Spawn(Spawnable spawnable)
    {
        var newItem = Instantiate(spawnable);
        newItem.transform.position = transform.position;

        newItem.Go(-10f);

        //Debug.Log(item.name + " Created!");
    }

    //QUICK AND DIRTY, REMOVE WHEN DONE. REMOVE TO STOP SPAWNING!
    IEnumerator SpawnerCycle()
    {
        while (true)
        {
            SpawnRandom();
            yield return new WaitForSeconds(1.5f);
        }
    }
    //QUICK AND DIRTY



    [System.Serializable]
    class WeightedSpawnable
    {
        public Spawnable spawnablePrototype;

        [Tooltip("Probability for spawn of this Spawnable")]
        public int spawnWeight;
    }

}
