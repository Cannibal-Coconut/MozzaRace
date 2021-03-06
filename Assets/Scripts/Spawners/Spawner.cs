using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ILiveListener
{
    [Tooltip("Spawnable Ingredients")]

    [SerializeField]
    WeightedSpawnable[] _easySpawnables;
    int _easyTotalWeight;

    [SerializeField]
    WeightedSpawnable[] _mediumSpawnables;
    int _mediumTotalWeight;

    [SerializeField]
    WeightedSpawnable[] _hardSpawnables;
    int _hardTotalWeight;


    SpawnSettings _spawnSettings;
    [SerializeField] SpawnSettings _defaultSpawnSettings;

    Coroutine _spawnCoroutine;
    ChangePizza _pizzaTime;


    private TipTime _tipTime;

    private void Awake()
    {
        _tipTime = FindObjectOfType<TipTime>();
        _pizzaTime = FindObjectOfType<ChangePizza>();

        UpdateTotalWeights();


        SetListeners();

        _spawnSettings = _defaultSpawnSettings;
    }

    public void StartSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }

        _spawnCoroutine = StartCoroutine(SpawnerCycle());
    }


    public void StopSpawn()
    {
        foreach (var removable in FindObjectsOfType<Removable>())
        {
            removable.Remove();
        }

        if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
    }

    public void SetListWeights(SpawnSettings settings)
    {
        _spawnSettings = settings;
    }

    void UpdateTotalWeights()
    {
        _easyTotalWeight = 0;
        foreach (var spawnable in _easySpawnables)
        {
            _easyTotalWeight += spawnable.spawnWeight;
        }

        _mediumTotalWeight = 0;
        foreach (var spawnable in _mediumSpawnables)
        {
            _mediumTotalWeight += spawnable.spawnWeight;
        }

        _hardTotalWeight = 0;
        foreach (var spawnable in _hardSpawnables)
        {
            _hardTotalWeight += spawnable.spawnWeight;
        }
    }

    public float SpawnRandom()
    {
        int listTotalWeight = _spawnSettings.easyObstacleWeight + _spawnSettings.mediumObstacleWeight + _spawnSettings.hardObstacleWeight;
        int listToken = Random.Range(0, listTotalWeight);

        int totalWeight;
        WeightedSpawnable[] selectedSpawnables;

        if (listToken < _spawnSettings.easyObstacleWeight)
        {
            //Debug.Log("Easy Obstacle Spawned");
            totalWeight = _easyTotalWeight;
            selectedSpawnables = _easySpawnables;
        }
        else if (listToken < _spawnSettings.easyObstacleWeight + _spawnSettings.mediumObstacleWeight)
        {
            //Debug.Log("Medium Obstacle Spawned");
            totalWeight = _mediumTotalWeight;
            selectedSpawnables = _mediumSpawnables;
        }
        else
        {
            //Debug.Log("Hard Obstacle Spawned");
            totalWeight = _hardTotalWeight;
            selectedSpawnables = _hardSpawnables;
        }

        int token = Random.Range(0, totalWeight);
        int sum = 0;

        foreach (var spawnable in selectedSpawnables)
        {
            //Calculate ingredient's range
            sum += spawnable.spawnWeight;

            //Check if sum is within ingredient's range
            if (token < sum)
            {

                return Spawn(spawnable.spawnablePrototype);
            }
        }

        return 0;
    }

    public float Spawn(Spawnable spawnable)
    {
        var newItem = Instantiate(spawnable);
        newItem.transform.position = transform.position;

        newItem.Go(-10f);

        return spawnable.timeForNextSpawn;

        //Debug.Log(item.name + " Created!");
    }

    //QUICK AND DIRTY, REMOVE WHEN DONE. REMOVE TO STOP SPAWNING!
    IEnumerator SpawnerCycle()
    {
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            //TIP TIME
            var time = SpawnRandom();
            if (_tipTime.GetTipTimeModeStatus()) time = _tipTime.GetTipTime();
            if (_pizzaTime.minigameState)
            {
                time = 4f;
            }
            yield return new WaitForSeconds(time);
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
