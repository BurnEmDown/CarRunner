using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class FactoryManager : BaseManager
    {
        public T[] GenerateObjects<T>(string originalName, int amount) where T : Component
        {
            var created = new List<T>();

            for (int i = 0; i < amount; i++)
            {
                var generatedObj = GenerateObject<T>(originalName);
                created.Add(generatedObj);
            }

            return created.ToArray();
        }

        public T GenerateObject<T>(string originalName) where T : Component
        {
            var original = Resources.Load<T>(originalName);
            return Object.Instantiate(original);
        }
    }
}

