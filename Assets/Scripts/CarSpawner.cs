using Managers;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 3f;
    private float nextTimeToSpawn = 1f;

    private float decreaseSpawnTimeDelay = 5f;
    private float nextTimeToDecreaseSpawnTime = 5f;
    private float decreaseSpawnTimeFactor = 0.9f;

    private void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnCar();
            nextTimeToSpawn = Time.time + spawnDelay;
        }

        if (nextTimeToDecreaseSpawnTime <= Time.time)
        {
            DecreaseSpawnTime();
            nextTimeToDecreaseSpawnTime = Time.time + decreaseSpawnTimeDelay;
        }
    }

    private void SpawnCar()
    {
        EnemyCar car = MainManager.Instance.poolManager.GetFromPool<EnemyCar>(nameof(EnemyCar));
        car.transform.SetParent(transform);
        car.transform.localScale = Vector3.one;
        car.transform.localPosition = Vector3.zero;
    }

    private void DecreaseSpawnTime()
    {
        spawnDelay *= decreaseSpawnTimeFactor;
    }
}
