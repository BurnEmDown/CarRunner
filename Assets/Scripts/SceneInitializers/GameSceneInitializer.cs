using System;
using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;

namespace SceneInitializers
{
    public class GameSceneInitializer : MonoBehaviour
    {
        [SerializeField] private TopStats topStatsObject;
        
        [SerializeField] private List<CarSO> carSOList;

        private void Start()
        {
            GameManager.AssignTopStats(topStatsObject);
            
            // I don't like using FindObjectsOfType but I'm doing this here to save time
            foreach (var carSpawner in FindObjectsOfType<CarSpawner>())
            {
                carSpawner.AssignCarSOList(carSOList);
            }
        }
    }
}

