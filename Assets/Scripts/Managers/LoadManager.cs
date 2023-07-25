﻿using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LoadManager : MonoBehaviour
    {
        public static LoadManager Instance;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Tried to create another GameLoader");
            }
        
            new MainManager();
            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(1);
            GameManager.ResetDefaultValues();
            GameManager.SubscribeToGameEvents();
        }
        
        public void LoadSummaryScene()
        {
            GameManager.UnsubscribeFromGameEvents();
            SceneManager.LoadScene(2);
        }
    }
}
