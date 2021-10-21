using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] [Range(1, 5)] float _spawnTime;
    [Header("References")]
    [SerializeField] WeightedObstacle[] _obstacles;

    int _totalWeight;

    private void Start()
    {
        UpdateTotalWeight();

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {

            yield return new WaitForSeconds(_spawnTime);

            SpawnRandomItem();
        }
    }

    void UpdateTotalWeight()
    {
        _totalWeight = 0;

        foreach (WeightedObstacle obstacle in _obstacles)
        {
            _totalWeight += obstacle.spawnWeight;
        }
    }

    public void SpawnRandomItem()
    {
        int token = Random.Range(0, _totalWeight);
        int sum = 0;

        foreach (var obstacle in _obstacles)
        {
            //Calculate ingredient's range
            sum += obstacle.spawnWeight;

            //Check if sum is within ingredient's range
            if (token < sum)
            {
                SpawnObstacle(obstacle.obstaclePrototype);

                return;
            }
        }
    }


    public void SpawnObstacle(Obstacle obstacle)
    {
        var newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = transform.position;

        newObstacle.go = true;
    }


    [System.Serializable]
    class WeightedObstacle
    {
        public Obstacle obstaclePrototype;

        [Tooltip("Probability for spawn of this obstacle")]
        public int spawnWeight;
    }
}
