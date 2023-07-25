using Managers.Events;
using UnityEngine;

namespace Managers
{
    public class MainManager : BaseManager
    {
        public static MainManager Instance;
    
        public FactoryManager factoryManager;
        public PoolManager poolManager;
        public EventsManager eventsManager;
    
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
            poolManager.InitPool<EnemyCar>(nameof(EnemyCar),10);
            eventsManager = new EventsManager();
        }
    }
}

