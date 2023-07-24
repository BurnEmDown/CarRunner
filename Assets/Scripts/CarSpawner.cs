using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float spawnDelay = 3f;

    private float nextTimeToSpawn = 1f;

    private void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnCar();
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    private void SpawnCar()
    {
        EnemyCar car = MainManager.Instance.poolManager.GetFromPool<EnemyCar>(nameof(EnemyCar));
        car.transform.SetParent(transform);
        car.transform.localScale = Vector3.one;
        car.transform.localPosition = Vector3.zero;
    }
}
