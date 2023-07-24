using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class PoolManager : BaseManager
    {
        public Dictionary<string, PoolData> pools = new Dictionary<string, PoolData>();

        private GameObject poolsHolder;

        public PoolManager()
        {
            poolsHolder = new GameObject();
            Object.DontDestroyOnLoad(poolsHolder);
        }
        
        public void InitPool<T>(string originalName, int amount) where T : Component
        {
            var generatedObjects = MainManager.Instance.factoryManager.GenerateObjects<T>(originalName, amount);

            if (generatedObjects == null || !generatedObjects.Any())
            {
                Debug.LogError($"Failed to generate objects for pool of item types {originalName}");
                return;
            }
            
            
            var poolHolder = new GameObject($"Pool_{originalName}");
            poolHolder.transform.SetParent(poolsHolder.transform);

            pools[originalName] = new PoolData(generatedObjects, poolHolder);
        }

        public T GetFromPool<T>(string poolName) where T : Component
        {
            if (!pools.ContainsKey(poolName))
            {
                Debug.LogError($"No pool initialized found with the name {poolName}");
                return null;
            }

            // check if available, if not generate 1 and add to pool
            if (!pools[poolName].availableItems.Any())
            {
                Debug.Log($"No available items in the pool {poolName}");
                var generateObject = MainManager.Instance.factoryManager.GenerateObject<T>(poolName);
                pools[poolName].AddNewToPool(generateObject);
            }
            
            var availableItem = pools[poolName].availableItems[0];
            pools[poolName].availableItems.Remove(availableItem);
            pools[poolName].unAvailableItems.Add(availableItem);

            availableItem.gameObject.SetActive(true);
            return (T)availableItem;
        }
        
        public void ReturnToPool<T>(string poolName, T returnedObject) where T : Component
        {
            pools[poolName].availableItems.Add(returnedObject);
            pools[poolName].unAvailableItems.Remove(returnedObject);
            returnedObject.gameObject.SetActive(false);
        }
    }
    
    public class PoolData
    {
        public List<Component> totalItems;
        public List<Component> availableItems;
        public List<Component> unAvailableItems;

        public GameObject PoolHolder;
        
        public PoolData(Component[] generatedObjects, GameObject poolHolder)
        {
            totalItems = generatedObjects.ToList();
            availableItems = generatedObjects.ToList();
            unAvailableItems = new List<Component>();
            
            PoolHolder = poolHolder;

            foreach (var generatedObject in generatedObjects)
            {
                generatedObject.transform.SetParent(PoolHolder.transform);
            }
        }

        public void AddNewToPool<T>(T generatedObject) where T : Component
        {
            totalItems.Add(generatedObject);
            availableItems.Add(generatedObject);
            generatedObject.transform.SetParent(PoolHolder.transform);
            generatedObject.gameObject.SetActive(false);
        }
    }
}

