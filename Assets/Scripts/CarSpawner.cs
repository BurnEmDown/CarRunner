using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private int laneIndex;
    [SerializeField] private List<CarSO> carSOList;
    [SerializeField] private float minSpawnDelay = 5f;
    [SerializeField] private float maxSpawnDelay = 15f;
    [SerializeField] private float nextTimeToSpawn;

    private float decreaseSpawnTimeDelay = 5f;
    private float nextTimeToDecreaseSpawnTime = 5f;
    private float decreaseSpawnTimeFactor = 0.9f;

    private void Start()
    {
        nextTimeToSpawn = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnCar();
            nextTimeToSpawn = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
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
        
        int randomIndex = Random.Range(0, carSOList.Count);
        CarSO carSO = carSOList[randomIndex];
        car.SetValues(carSO.size, carSO.color, carSO.speed, carSO.canMoveLanes, carSO.scoreGiven, laneIndex);
        car.AddAllLocationFromLanesParentObject(transform.parent.gameObject);
    }

    private void DecreaseSpawnTime()
    {
        minSpawnDelay *= decreaseSpawnTimeFactor;
        maxSpawnDelay *= decreaseSpawnTimeFactor;
    }

    public void AssignCarSOList(List<CarSO> carsSO)
    {
        carSOList = carsSO;
    }
}
