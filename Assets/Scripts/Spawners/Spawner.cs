using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ILiveListener
{
    [Tooltip("Spawnable Ingredients")]
    [SerializeField]
    List<WeightedSpawnable> _spawnables;

    //Range of ingredients' weight
    int _totalWeight;

    Coroutine _spawnCoroutine;

    private void Awake()
    {
        UpdateTotalWeight();

        SetListeners();
    }

    void StartSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }

        _spawnCoroutine = StartCoroutine(SpawnerCycle());
    }

    void StopSpawn()
    {
        foreach (var removable in FindObjectsOfType<Removable>())
        {
            removable.Remove();
        }

        StopCoroutine(_spawnCoroutine);
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

    public void OnLive()
    {
        StartSpawn();
    }

    public void OnDead()
    {
        StopSpawn();
    }

    public void SetListeners()
    {
        var player = FindObjectOfType<Health>();
        if (player)
        {
            player.AddLiveListener(OnLive);
            player.AddDeadListener(OnDead);
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
