using System;
using Managers;
using UI;
using UnityEngine;

namespace SceneInitializers
{
    public class GameSceneInitializer : MonoBehaviour
    {
        [SerializeField] private TopStats topStatsObject;

        private void Start()
        {
            GameManager.AssignTopStats(topStatsObject);
        }
    }
}

