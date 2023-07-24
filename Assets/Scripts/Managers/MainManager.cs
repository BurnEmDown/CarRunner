﻿using UnityEngine;

namespace Managers
{
    public class MainManager : BaseManager
    {
        public static MainManager Instance;
    
        public FactoryManager factoryManager;
        public PoolManager poolManager;
    
        public MainManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Tried to create another Main Manager");
            }
        
            factoryManager = new FactoryManager();
            poolManager = new PoolManager();
            //poolManager = Object.FindObjectOfType<PoolManager>();
        }
    }
}
